using Company.GestioneDevice.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
namespace Company.GestioneDevice.Users;

public class EfCoreUserRepository : EfCoreRepository<GestioneDeviceDbContext, User, Guid>, IUserRepository
{
    public EfCoreUserRepository(IDbContextProvider<GestioneDeviceDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }



}