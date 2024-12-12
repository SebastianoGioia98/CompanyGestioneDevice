using Company.GestioneDevice.Devices.SoftwareVersions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Company.GestioneDevice.Devices;

public class CreateDeviceDto: EntityDto
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
    public SoftwareVersionDto firstSoftwareVersion { get; set; }

    [Required]
    public List<Guid> DeviceFeatures { get; set; } = new List<Guid>();
}
