using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Company.GestioneDevice.Users.UserPolicies;

public class UserPolicy : AuditedEntity<Guid>
{
    public Guid UserId { get; private set; }
    public Guid PolicyId { get; private set; }

    //constructor
    public UserPolicy()
    {

    }

    public UserPolicy(Guid id, Guid userId, Guid policyId) :base(id)
    {
        UserId = userId;
        PolicyId = policyId;
    }
    
    public UserPolicy(Guid userId, Guid policyId)
    {
        UserId = userId;
        PolicyId = policyId;
    }


    public override object[] GetKeys()
    {
        return new object[] { UserId, PolicyId };
    }
}
