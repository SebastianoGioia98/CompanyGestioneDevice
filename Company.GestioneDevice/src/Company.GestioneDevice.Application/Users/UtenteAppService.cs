using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Company.GestioneDevice.Users;

public class UtenteAppService : CrudAppService<
                                User,
                                UserDto,
                                Guid,
                                PagedAndSortedResultRequestDto
                                >
                                //IUtenteAppService
{

    //dependency injection
    private readonly IGuidGenerator _guidGenerator;
    private readonly UserManager _userManager;


    //constructor
    public UtenteAppService(IUserRepository repository, IGuidGenerator guidGenerator, UserManager userManager) : base(repository)
    {
        _guidGenerator = guidGenerator;
        _userManager = userManager;
    }


    //Methods
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> CreateUserWithPolicies(UserDto input)
    {
        var user = await _userManager.CreateAsync(
            _guidGenerator.Create(),
            "Username",
            "Name",
            "Surname",
            "Email",
            "Telephone",
            []
            );



        //var user = await _userManager.CreateAsync(
        //    _guidGenerator.Create(),
        //    input.Username,
        //    input.Name,
        //    input.Surname,
        //    input.Email,
        //    input.Telephone,
        //    input.PolicyIds
        //    );

        //set policies
        //if (input.PolicyIds != null && input.PolicyIds.Count() > 0)
        //{
        //   await _userManager.SetPoliciesAsync(user, input.PolicyIds);
        //}

        //save
        await Repository.InsertAsync(user);

        //return confirm DTO
        return new OkResult();
    }
}
