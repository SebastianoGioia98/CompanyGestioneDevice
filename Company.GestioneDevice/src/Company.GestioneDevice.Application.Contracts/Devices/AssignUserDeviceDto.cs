using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.GestioneDevice.Devices;

public class AssignUserDeviceDto
{
    public Guid DeviceId { get; set; }

    public Guid UserId { get; set; }
}
