using Company.GestioneDevice.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Company.GestioneDevice.Devices;


public interface IDeviceAppService : ICrudAppService<
    DeviceDto,
    Guid,
    PagedAndSortedResultRequestDto
    >
{
    //public Task<DeviceDto> CreateDeviceWithFeatures(CreateDeviceDto input);
}