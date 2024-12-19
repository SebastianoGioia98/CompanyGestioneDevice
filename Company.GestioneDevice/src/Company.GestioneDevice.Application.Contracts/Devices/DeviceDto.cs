using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.GestioneDevice.Devices;
using Company.GestioneDevice.Devices.SoftwareVersions;
using Company.GestioneDevice.Features;
using Company.GestioneDevice.Users;

namespace Company.GestioneDevice.Devices;

public class DeviceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public DeviceType Type { get; set; }

    public string Model { get; set; }

    public UserLookupDto User { get; set; }

    public DateTime CreationTime { get; set; }
}
