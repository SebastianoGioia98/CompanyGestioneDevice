using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Company.GestioneDevice.Devices.Features;

public class Feature : AuditedEntity<Guid>
{
    public string Name { get; set; }


    //constructor
    public Feature() { }

    public Feature(Guid id, string name):base(id)
    {
        this.Name = name;
    }
}
