using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.GestioneDevice.Devices;

public class AssignDeviceUserDto
{
    [Required]
    public Guid DeviceId { get; set; }

    [Required]
    public Guid UserId { get; set; }
}
