using Company.GestioneDevice.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Company.GestioneDevice.Features;

public interface IFeatureAppService : ICrudAppService<
    FeatureDtoOther,
    Guid,
    PagedAndSortedResultRequestDto
    >
{
    //public Task<DeviceDto> CreateDeviceWithFeatures(CreateDeviceDto input);
}