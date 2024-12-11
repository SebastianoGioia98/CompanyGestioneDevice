using Company.GestioneDevice.Users.UserPolicies;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;

namespace Company.GestioneDevice.Users;

public class User : AuditedAggregateRoot<Guid>
{
    public string Username { get; private set; }
    public string Name { get;  set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string? Telephone { get; set; }

    //navigation properties
    public List<UserPolicy> UserPolicies { get; set; } = new List<UserPolicy>();




    //Constructors
    public User(Guid id, string username, string name, string surname, string email, string? telephone = null) : base(id)
    {
        SetUsername(username);
        Name=name;
        Surname = surname;
        Email = email;
        Telephone = telephone ?? null;
    }

    public User()
    {

    }


    //-------Property SET GET
    public void SetUsername(string username)
    {
        Username = Check.NotNullOrWhiteSpace(username, nameof(username), GestioneDeviceSharedConsts.MaxNameLength);
    }



    //--------POLICY Methods
    public void AssignPolicy(Guid policyId)
    {
        Check.NotNull(policyId, nameof(policyId));

        if (IsInPolicies(policyId))
        {
            return;
        }

        UserPolicies.Add(new UserPolicy(this.Id, policyId));
    }

    public void RemovePolicy(Guid policyId)
    {
        Check.NotNull(policyId, nameof(policyId));

        if (!IsInPolicies(policyId))
        {
            return;
        }

        UserPolicies.RemoveAll(x => x.PolicyId == policyId);
    }

    public void RemoveAllPoliciesExceptGivenIds(List<Guid> policyIds)
    {
        Check.NotNullOrEmpty(policyIds, nameof(policyIds));

        UserPolicies.RemoveAll(x => !policyIds.Contains(x.PolicyId));
    }

    public void RemoveAllPolicies()
    {
        UserPolicies.RemoveAll(x => x.UserId == this.Id);
    }

    private bool IsInPolicies(Guid policyId)
    {
        return UserPolicies.Any(x => x.PolicyId == policyId);
    }
}
