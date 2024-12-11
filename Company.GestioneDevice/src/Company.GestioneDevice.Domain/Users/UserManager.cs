using Company.GestioneDevice.Policies;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Company.GestioneDevice.Users;

public class UserManager: DomainService
{
    private readonly IUserRepository _userRepository;
    private readonly IPolicyRepository _policyRepository;

    public UserManager(IUserRepository userRepository, IPolicyRepository policyRepository)
    {
        _userRepository = userRepository;
        _policyRepository = policyRepository;
    }

    // ------ USER Methods
    public async Task<User> CreateAsync(Guid id, string username, string name, string surname, string email, string? telephone, [CanBeNull] List<Guid> policyIds)
    {
        var user = new User(GuidGenerator.Create(), username, name, surname, email, telephone);

        await this.SetPoliciesAsync(user, policyIds);

        return user;
    }

    public async Task<User> UpdateAsync(
        User user,
        string username,
        string name,
        string surname,
        string email,
        string? telephone,
        [CanBeNull] List<Guid> policyIds
    )
    {
        user.SetUsername(username);
        user.Name=name;
        user.Surname = surname;
        user.Email = email;
        user.Telephone = telephone;


        await this.SetPoliciesAsync(user, policyIds);

        return user;
    }



    // ------ POLICY Methods
    public async Task SetPoliciesAsync(User user, [CanBeNull] List<Guid> policyIds)
    {
        if (policyIds == null || !policyIds.Any())
        {
            user.RemoveAllPolicies();
            return;
        }

        var query = (await _policyRepository.GetQueryableAsync())
            .Where(x => policyIds.Contains(x.Id))
            .Select(x => x.Id)
            .Distinct();

        var filteredpolicyIds = await AsyncExecuter.ToListAsync(query);
        if (!filteredpolicyIds.Any())
        {
            return;
        }

        user.RemoveAllPoliciesExceptGivenIds(filteredpolicyIds);

        foreach (var policyId in filteredpolicyIds)
        {
            user.AssignPolicy(policyId);
        }
    }
}
