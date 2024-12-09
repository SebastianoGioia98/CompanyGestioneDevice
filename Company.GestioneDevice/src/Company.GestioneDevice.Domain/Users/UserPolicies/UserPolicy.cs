using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Company.GestioneDevice.Users.UserPolicies;

public class UserPolicy : AuditedEntity<Guid>
{
    public Guid UserId { get; private set; }
    public Guid PolicieId { get; private set; }

    //constructor
    UserPolicy()
    {

    }
    UserPolicy(Guid id, Guid userId, Guid policieId):base(id)
    {
        UserId = userId;
        PolicieId = policieId;
    }
}
