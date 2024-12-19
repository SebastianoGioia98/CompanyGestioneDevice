using Company.GestioneDevice.Devices.SoftwareVersions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.GestioneDevice.Devices;

public class UpdateDeviceDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [StringLength(GestioneDeviceSharedConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    public DeviceType Type { get; set; }

    [Required]
    [StringLength(GestioneDeviceSharedConsts.MaxNameLength)]
    public string Model { get; set; }


    [Required]
    public List<Guid> DeviceFeaturesIds { get; set; } = new List<Guid>();
}
