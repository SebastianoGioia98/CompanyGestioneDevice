using Company.GestioneDevice.EntityFrameworkCore;
using Company.GestioneDevice.Policies;
using System;
using System.Collections.Generic;
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



}