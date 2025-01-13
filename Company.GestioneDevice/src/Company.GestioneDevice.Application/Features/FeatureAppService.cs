using Company.GestioneDevice.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Guids;

namespace Company.GestioneDevice.Features;

public class FeatureAppService : CrudAppService<
                                Feature,
                                FeatureDtoOther,
                                Guid,
                                PagedAndSortedResultRequestDto
                                >,
                                IFeatureAppService
{

    //dependency injection
    // private readonly IGuidGenerator _guidGenerator;


    //constructor
    public FeatureAppService(IFeatureRepository repository /*IGuidGenerator guidGenerator*/) : base(repository)
    {
        // _guidGenerator = guidGenerator;

    }


    //Methods



}