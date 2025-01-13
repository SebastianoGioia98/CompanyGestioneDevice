using Company.GestioneDevice.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Company.GestioneDevice.Policies;

public interface IPolicyAppService : ICrudAppService<
    PolicyDto,   
    Guid,
    PagedAndSortedResultRequestDto
    >
{
    //public Task<DeviceDto> CreateDeviceWithFeatures(CreateDeviceDto input);
}