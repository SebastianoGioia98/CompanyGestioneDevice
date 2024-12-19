using Company.GestioneDevice.Devices.SoftwareVersions;
using Company.GestioneDevice.Features;
using Company.GestioneDevice.Policies;
using Company.GestioneDevice.Users;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Company.GestioneDevice.Devices;

public class DeviceManager : DomainService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IFeatureRepository _featureRepository;
    private readonly IUserRepository _userRepository;

    public DeviceManager(IDeviceRepository deviceRepository, IFeatureRepository featureRepository, IUserRepository userRepository)
    {
        _deviceRepository = deviceRepository;
        _featureRepository = featureRepository;
        _userRepository = userRepository;   
    }



    // ------ DEVICE Methods
    public async Task<List<Feature>> UpdateAsync(
    Device device,
    string name,
    DeviceType type,
    string model,
    [CanBeNull] List<Guid> featureIds
)
    {

        device.SetName(name);
        device.Type = type;
        device.Model = model;


        List<Feature> deviceFeatures = await this.SetFeaturesAsync(device, featureIds);

        return deviceFeatures;
    }


    // ------ FEATURE Methods
    public async Task<List<Feature>> SetFeaturesAsync(Device device, [CanBeNull] List<Guid> featureIds)
    {

        //check featureIds null
        if (featureIds == null || !featureIds.Any())
        {
            device.RemoveAllFeatures();
            return [];
        }

        //recupero features dal DB
        var filteredFeatures = (await _featureRepository.GetQueryableAsync())
          .Where(x => featureIds.Contains(x.Id))
          .Select(x => new Feature(x.Id, x.Name, x.PolicyId))
          .Distinct()
          .ToList();

        var filteredFeaturesIds = filteredFeatures.Select(x => x.Id).ToList();

        //check recovered features empty
        if (!filteredFeaturesIds.Any())
        {
            return [];
        }

        //rimuovi tutte le feature che non sono nella filterdList 
        device.RemoveAllFeaturesExceptGivenIds(filteredFeaturesIds);

        //assegna le feature rimanenti
        foreach (var featureId in filteredFeaturesIds)
        {
            device.AssignFeature(featureId);
        }

        return filteredFeatures;

    }

    public async Task<IQueryable<Feature>> GetQueryablefeatures()
    {
        return await _featureRepository.GetQueryableAsync();
    }



    // ------ USER Methods
    public async Task<User> GetAssociatedUser(Guid userId)
    {
        return await _userRepository.GetAsync(userId);
    }


    public async Task<IQueryable<User>> GetQueryableUsers()
    {
        return await _userRepository.CompleteJoin();
    }


    public async Task<bool> AssignDeviceUserAsync(Device device,  Guid userId)
    {
        var user = await _userRepository.GetAsync(userId);

        //check dello user
        if (user == null)
        {
            return false;
        }

        //assegnazione dello user
        device.AssignUser(userId);

        return true;
    }

}