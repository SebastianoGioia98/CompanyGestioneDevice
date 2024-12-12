using Company.GestioneDevice.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Guids;

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


    //constructor
    public DeviceAppService(IDeviceRepository repository, IGuidGenerator guidGenerator, DeviceManager deviceManager) : base(repository)
    {
        _guidGenerator = guidGenerator;
        _deviceManager = deviceManager;
    }


    //Methods
   // public Task<DeviceDto> CreateDeviceWithFeatures(CreateDeviceDto input)
   // {

    //}



}