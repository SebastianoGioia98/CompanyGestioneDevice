using Company.GestioneDevice.EntityFrameworkCore;
using Company.GestioneDevice.Policies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Company.GestioneDevice.Devices;



public class EfCoreDeviceRepository : EfCoreRepository<GestioneDeviceDbContext, Device, Guid>, IDeviceRepository
{
public EfCoreDeviceRepository(IDbContextProvider<GestioneDeviceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }



    public async Task<IQueryable<Device>> CompleteJoin()
    {
        var queryable = await GetQueryableAsync();
        return queryable
            .Include(x => x.SoftwareVersions)
            .Include(x => x.DeviceFeatures)
            .AsQueryable();
    }



}