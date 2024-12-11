using Company.GestioneDevice.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Company.GestioneDevice.Features;


public class EfCoreFeatureRepository : EfCoreRepository<GestioneDeviceDbContext, Feature, Guid>, IFeatureRepository
{
    public EfCoreFeatureRepository(IDbContextProvider<GestioneDeviceDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }

}