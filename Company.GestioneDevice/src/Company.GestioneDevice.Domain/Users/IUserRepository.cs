using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Company.GestioneDevice.Users;

public interface IUserRepository : IRepository<User, Guid>
{
    
   // public Task<List<Policie.Policies>> GetUserPolicies(string userEmail);

   // public Task<List<User>> GetUsers(Guid projectId);
}
