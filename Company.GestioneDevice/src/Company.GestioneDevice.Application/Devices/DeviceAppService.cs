using Company.GestioneDevice.Devices.DeviceFeatures;
using Company.GestioneDevice.Devices.SoftwareVersions;
using Company.GestioneDevice.Features;
using Company.GestioneDevice.Policies;
using Company.GestioneDevice.Users;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using Volo.Abp.Validation;



namespace Company.GestioneDevice.Devices;


public class DeviceAppService : CrudAppService<
                                Device,
                                DeviceDto,
                                Guid,
                                PagedAndSortedResultRequestDto
                                >,
                                IDeviceAppService
{

    //dependency injection
    private readonly IGuidGenerator _guidGenerator;
    private readonly DeviceManager _deviceManager;
    private readonly IDeviceRepository _repository;


    //constructor
    public DeviceAppService(IDeviceRepository repository, IGuidGenerator guidGenerator, DeviceManager deviceManager) : base(repository)
    {
        _guidGenerator = guidGenerator;
        _deviceManager = deviceManager;
        _repository = repository;
    }


    //Methods

    public async Task<ActionResult<DeviceDetailedDto>> CreateDeviceWithDetails(CreateDeviceDto input)
    {

        //create device
        Device newDevice = new Device(
            _guidGenerator.Create(),
            input.Name,
            input.Type,
            input.Model
            );

        //create softwareVersion
        SoftwareVersion firstVersion = new SoftwareVersion(
             _guidGenerator.Create(),
             newDevice.Id,
            input.FirstSoftwareVersion.Name,
            input.FirstSoftwareVersion.Version
            );

        newDevice.UpdateSoftwareVersion(firstVersion);

        //set features
        List<Feature> deviceFeatures = await _deviceManager.SetFeaturesAsync(newDevice, input.DeviceFeaturesIds);

        //assign to user
        await _deviceManager.AssignDeviceUserAsync(newDevice, input.UserId);

        //save
        await Repository.InsertAsync(newDevice);

        //return confirm Dto
        var res = ObjectMapper.Map<Device, DeviceDetailedDto>(newDevice);
        res.Features = ObjectMapper.Map<List<Feature>, List<FeatureDto>>(deviceFeatures);
        res.LastSoftwareVersion = ObjectMapper.Map<SoftwareVersion, SoftwareVersionLookupDto>(newDevice.SoftwareVersions[0]);
        res.User = ObjectMapper.Map<User, UserLookupDto>(await _deviceManager.GetAssociatedUser(newDevice.UserId));
        return new OkObjectResult(res);

    }


    public async Task<ActionResult<(long totalCount, List<DeviceDto> items)>> GetDeviceList()
    {
        //Get the IQueryable<Device> from the repository
        var queryable = await _repository.GetQueryableAsync();

        //Prepare a query to join devices and users
        var query = from device in queryable
                    join user in await _deviceManager.GetQueryableUsers() on device.UserId equals user.Id
                    select new { device, user };

        //Paging
        //query = query
        //    .OrderBy(NormalizeSorting(input.Sorting))
        //    .Skip(input.SkipCount)
        //    .Take(input.MaxResultCount);

        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        //Convert the query result to a list of DeviceDto objects
        var deviceDtos = queryResult.Select(x =>
        {
            var deviceDto = ObjectMapper.Map<Device, DeviceDto>(x.device);
            deviceDto.User = ObjectMapper.Map<User, UserLookupDto>(x.user);
            return deviceDto;
        }).ToList();

        //Get the total count with another query
        // var totalCount = await _repository.GetCountAsync();

        return new OkObjectResult(
            (deviceDtos)
        );
    }


    [HttpGet("api/app/device/device-by-id/{id}")]
    public async Task<ActionResult<DeviceDetailedDto>> GetDeviceById(Guid id)
    {
        //Get the IQueryable<Device> from the repository
        var queryable = await _repository.CompleteJoin();


        //Prepare a query to join devices and users and Feature

        var featuresQuery = await _deviceManager.GetQueryablefeatures();
        var usersQuery = await _deviceManager.GetQueryableUsers();

        var query = from device in queryable
                    join user in usersQuery
                        on device.UserId equals user.Id
                    where device.Id == id
                    select new
                    {
                        Device = device,
                        User = user,
                        Features = device.DeviceFeatures
                                     .Join(featuresQuery,
                                           df => df.FeatureId,
                                           f => f.Id,
                                           (df, f) => f)
                                     .ToList()
                    };

        //Execute the query and check
        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Device), id);
        }

        //   --- prepare Dto ---
        var deviceDto = ObjectMapper.Map<Device, DeviceDetailedDto>(queryResult.Device);

        //user
        deviceDto.User = ObjectMapper.Map<User, UserLookupDto>(queryResult.User);

        //softwareVersion
        if (queryResult.Device.SoftwareVersions.Any())
        {
            deviceDto.LastSoftwareVersion = ObjectMapper.Map<SoftwareVersion, SoftwareVersionLookupDto>(queryResult.Device.SoftwareVersions[queryResult.Device.SoftwareVersions.Count-1]);
        }

        //DeviceFeature
        if (queryResult.Features.Any())
        {
            deviceDto.Features = ObjectMapper.Map<List<Feature>, List<FeatureDto>>(queryResult.Features);
            foreach (var feature in deviceDto.Features)
            {
                var featureMatch = queryResult.Features.FirstOrDefault(o => o.Name == feature.Name);
                feature.State = featureMatch != null && queryResult.User.UserPolicies
                    .Select(up => up.PolicyId)
                    .ToList()
                    .Contains(featureMatch.PolicyId);
            }
        }

        return deviceDto;
    }


    [AllowAnonymous]
    [HttpPut]
    public async Task<ActionResult<DeviceDetailedDto>> UpdateDeviceWithDetails(UpdateDeviceDto input)
    {

        //Get the IQueryable<Device> from the repository
        var queryable = await _repository.CompleteJoin();

        //find device
        var query = from dev in queryable
                    where dev.Id == input.Id
                    select dev;

        var device = await AsyncExecuter.FirstOrDefaultAsync(query);

        //check device null
        if (device == null)
        {
            return new NotFoundResult();
        }

        //update device
        var deviceFeatures = await _deviceManager.UpdateAsync(
         device,
         input.Name,
         input.Type,
         input.Model,
         input.DeviceFeaturesIds
        );

        //save
        await Repository.UpdateAsync(device);

        //return confirm Dto
        var res = ObjectMapper.Map<Device, DeviceDetailedDto>(device);


        res.Features = ObjectMapper.Map<List<Feature>, List<FeatureDto>>(deviceFeatures);
        res.LastSoftwareVersion = ObjectMapper.Map<SoftwareVersion, SoftwareVersionLookupDto>(device.SoftwareVersions[0]);
        res.User = ObjectMapper.Map<User, UserLookupDto>(await _deviceManager.GetAssociatedUser(device.UserId));
        return new OkObjectResult(res);
    }


    [AllowAnonymous]
    [HttpPut]
    public async Task<ActionResult<DeviceDto>> AssignUserDevice([FromBody] AssignUserDeviceDto input)
    {
        //find device
        var queryable = await _repository.CompleteJoin();

        var query = from dev in queryable
                    where dev.Id == input.DeviceId
                    select dev;

        var device = await AsyncExecuter.FirstOrDefaultAsync(query);

        //check device null
        if (device == null)
        {
            return new NotFoundResult();
        }

        //find user
        var userQueryable = await _deviceManager.GetQueryableUsers();

        var userQuery = from u in userQueryable
                        where u.Id == input.UserId
                        select u;

        var user = await AsyncExecuter.FirstOrDefaultAsync(userQuery);

        //check user null
        if (user == null)
        {
            return new NotFoundResult();
        }

        //Assign user to device
        device.AssignUser(input.UserId);


        //save
        await Repository.UpdateAsync(device);

        //return confirm DTO
        var res = ObjectMapper.Map<Device, DeviceDto>(device);
        res.User = ObjectMapper.Map<User, UserLookupDto>(user);
        return new OkObjectResult(res);
    }


    [HttpGet("api/app/device/software-versions/{id}")]
    public async Task<ActionResult<List<SoftwareVersionDto>>> GetSoftwareVersions(Guid id)
    {
        //Get the IQueryable<Device> from the repository
        var queryable = await _repository.CompleteJoin();

        //find device
        var query = from dev in queryable
                    where dev.Id == id
                    select dev;

        var device = await AsyncExecuter.FirstOrDefaultAsync(query);

        //check device null
        if (device == null)
        {
            return new NotFoundResult();
        }

        //return confirm Dto
        var res = ObjectMapper.Map<List<SoftwareVersion>, List<SoftwareVersionDto>>(device.SoftwareVersions);

        return res;
    }


    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<SoftwareVersionDto>> UpdateDeviceSoftware([FromBody][Required] UpdateDeviceSoftwareVersionDto input)
    {
        //Get the IQueryable<Device> from the repository
        var queryable = await _repository.CompleteJoin();

        //find device
        var query = from dev in queryable
                    where dev.Id == input.DeviceId
                    select dev;

        var device = await AsyncExecuter.FirstOrDefaultAsync(query);

        //check device null
        if (device == null)
        {
            return new NotFoundResult();
        }

        var newSoftwareVersion = ObjectMapper.Map<SoftwareVersionLookupDto, SoftwareVersion>(input.NewSoftwareVersion);
        device.UpdateSoftwareVersion(newSoftwareVersion);

        //save
        await Repository.UpdateAsync(device);

        //return confirm Dto
        var res = ObjectMapper.Map<SoftwareVersion, SoftwareVersionDto>(newSoftwareVersion);

        return res;

    }


    [HttpGet("api/app/device/geolocalization/{id}")]
    public async Task<ActionResult<DeviceGeolocalizationDto>> GetDeviceGeolocalization(Guid id)
    {
        //Get the IQueryable<Device> from the repository
        var queryable = await _repository.CompleteJoin();

        //find device
        var query = from dev in queryable
                    where dev.Id == id
                    select dev;

        var device = await AsyncExecuter.FirstOrDefaultAsync(query);

        //check device null
        if (device == null)
        {
            return new NotFoundResult();
        }


        var res = new DeviceGeolocalizationDto(44.412998, 8.945222);

        return res;
    }
}