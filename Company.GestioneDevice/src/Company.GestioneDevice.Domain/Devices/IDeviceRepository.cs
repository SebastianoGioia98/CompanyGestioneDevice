using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Company.GestioneDevice.Devices;


public interface IDeviceRepository : IRepository<Device, Guid>
{

   
}
