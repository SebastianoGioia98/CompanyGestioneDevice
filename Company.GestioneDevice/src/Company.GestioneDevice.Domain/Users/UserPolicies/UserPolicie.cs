using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Company.GestioneDevice.Users.UserPolicies;

public class UserPolicie : AuditedEntity<Guid>
{
    public Guid UserId { get; private set; }
    public Guid PolicieId { get; private set; }

    //constructor
    UserPolicie()
    {

    }
    UserPolicie(Guid id, Guid userId, Guid policieId):base(id)
    {
        UserId = userId;
        PolicieId = policieId;
    }
}
