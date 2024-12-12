using Company.GestioneDevice.Features;
using Company.GestioneDevice.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Company.GestioneDevice.Devices;

public class DeviceManager: DomainService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IFeatureRepository _featureRepository;

    public DeviceManager(IDeviceRepository deviceRepository, IFeatureRepository featureRepository)
    {
        _deviceRepository = deviceRepository;
        _featureRepository = featureRepository;
    }

}
