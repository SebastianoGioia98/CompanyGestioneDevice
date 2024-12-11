using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Company.GestioneDevice.Policies;

public class Policy : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }

    //constructor
    public Policy() { }
    public Policy(Guid id, string name) : base(id)
    {
        Name = name;
    }
}
