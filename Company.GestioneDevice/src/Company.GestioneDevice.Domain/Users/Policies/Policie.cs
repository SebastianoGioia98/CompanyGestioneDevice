using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Company.GestioneDevice.Users.Policies;

public class Policie : AuditedEntity<Guid>
{
    public string Name { get; set; }

    //constructor
    Policie() { }
    Policie(Guid id, string name):base(id)
    {
        Name = name;
    }
}
