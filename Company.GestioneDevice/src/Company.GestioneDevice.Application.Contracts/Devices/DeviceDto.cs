using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.GestioneDevice.Devices;
using Company.GestioneDevice.Devices.SoftwareVersions;
using Company.GestioneDevice.Features;

namespace Company.GestioneDevice.Devices;

public class DeviceDto
{
    [Required]
    [StringLength(GestioneDeviceSharedConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    public DeviceType Type { get; set; }

    [Required]
    [StringLength(GestioneDeviceSharedConsts.MaxNameLength)]
    public string Model { get; set; }

    public Guid UserId { get; set; }

    [Required]
    public SoftwareVersionDto LastSoftwareVersion { get; set; }

    [Required]
    public List<FeatureDto> DeviceFeatures { get; set; } = new List<FeatureDto>();

   
}
