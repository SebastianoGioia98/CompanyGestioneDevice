using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.GestioneDevice.Devices.SoftwareVersions;

public class UpdateDeviceSoftwareVersionDto
{
     public Guid DeviceId {  get; set; }

    public SoftwareVersionLookupDto NewSoftwareVersion { get; set; } 


}
