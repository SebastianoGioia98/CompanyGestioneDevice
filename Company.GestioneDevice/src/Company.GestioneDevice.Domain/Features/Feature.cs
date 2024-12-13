using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Company.GestioneDevice.Features;

public class Feature : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }

    //navigation
    public Guid PolicyId { get; set; }




    //constructor
    public Feature() { }

    public Feature(Guid id, string name, Guid policyId) : base(id)
    {
        this.Name = name;
        PolicyId = policyId;
    }
}
