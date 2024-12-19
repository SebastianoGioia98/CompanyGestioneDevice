using Company.GestioneDevice.Features;
using Company.GestioneDevice.Policies;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Company.GestioneDevice.Users;

public class UserManager: DomainService
{

    //dependency injection
    private readonly IUserRepository _userRepository;
    private readonly IPolicyRepository _policyRepository;


    //constructor
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

    public async Task<User> UpdateAsync(User user, string username, string name, string surname, string email, string? telephone)
    {
        user.SetUsername(username);
        user.Name=name;
        user.Surname = surname;
        user.Email = email;
        user.Telephone = telephone;

        return user;
    }



    // ------ POLICY Methods

    public async Task<IQueryable<Policy>> GetQueryablePolicy()
    {
        return await _policyRepository.GetQueryableAsync();
    }






    public async Task<List<Policy>> GetUserPoliciesAsync(User user)
    {
        if (!user.UserPolicies.Any())
        {
            return [];
        }

        List<Policy> res = new List<Policy>();
        foreach (var UserPolicies in user.UserPolicies)
        {
            res.Add(await _policyRepository.GetAsync(UserPolicies.PolicyId));
        }
        return res;
    }

    public async Task<List<Policy>> SetPoliciesAsync(User user, [CanBeNull] List<Guid> policyIds)
    {
        if (policyIds == null || !policyIds.Any())
        {
            user.RemoveAllPolicies();
            return [];
        }

        var filteredPolicies = (await _policyRepository.GetQueryableAsync())
          .Where(x => policyIds.Contains(x.Id))
          .Select(x => new Policy(x.Id, x.Name))
          .Distinct()
          .ToList();

        var filteredPolicyIds = filteredPolicies.Select(x => x.Id).ToList();

        if (!filteredPolicyIds.Any())
        {
            return [];
        }

        user.RemoveAllPoliciesExceptGivenIds(filteredPolicyIds);

        foreach (var policyId in filteredPolicyIds)
        {
            user.AssignPolicy(policyId);
        }

        return filteredPolicies;

    }
}
