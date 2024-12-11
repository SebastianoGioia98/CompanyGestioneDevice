using Company.GestioneDevice.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Company.GestioneDevice.Policies;




public class EfCorePolicyRepository : EfCoreRepository<GestioneDeviceDbContext, Policy, Guid>, IPolicyRepository
{
    public EfCorePolicyRepository(IDbContextProvider<GestioneDeviceDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }



}