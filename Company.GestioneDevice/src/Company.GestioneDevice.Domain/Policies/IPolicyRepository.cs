using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Company.GestioneDevice.Policies;

public interface IPolicyRepository : IRepository<Policy, Guid>
{

    // public Task<List<Policie.Policies>> GetUserPolicies(string userEmail);

    // public Task<List<User>> GetUsers(Guid projectId);
}

