using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Company.GestioneDevice.Devices.SoftwareVersions;

public class SoftwareVersion : AuditedEntity<Guid>
{
    public Guid DeviceId { get; set; } // Foreign Key verso Device
    public string Name  { get; set; }
    public string Version { get; set; }
    

    //constructor
    public SoftwareVersion() 
    {

    }
    public SoftwareVersion(Guid id,Guid deviceId, string name, string version):base(id)
    {
        DeviceId = deviceId;
        Name = name;
        Version = version;
    }
}

