using Company.GestioneDevice.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
namespace Company.GestioneDevice.Users;

public class EfCoreUserRepository : EfCoreRepository<GestioneDeviceDbContext, User, Guid>, IUserRepository
{
    public EfCoreUserRepository(IDbContextProvider<GestioneDeviceDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }



    public async Task<User?> GetWithDetails(Guid id)
    {
        return await (await GetQueryableAsync())
            .Include(u => u.UserPolicies)
            .FirstOrDefaultAsync(u => u.Id == id);

    }


}