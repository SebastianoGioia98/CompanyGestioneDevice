using Company.GestioneDevice.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Company.GestioneDevice.Policies;

public class PolicyAppService : CrudAppService<
                                Policy,
                                PolicyDto,
                                Guid,
                                PagedAndSortedResultRequestDto
                                >,
                                IPolicyAppService
{

    //dependency injection
    // private readonly IGuidGenerator _guidGenerator;


    //constructor
    public PolicyAppService(IPolicyRepository repository /*IGuidGenerator guidGenerator*/) : base(repository)
    {
        // _guidGenerator = guidGenerator;

    }


    //Methods



}