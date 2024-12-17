using Company.GestioneDevice.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using Volo.Abp.Validation;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using static Volo.Abp.UI.Navigation.DefaultMenuNames.Application;


namespace Company.GestioneDevice.Users;

public class UtenteAppService : CrudAppService<
                                User,
                                UserDto,
                                Guid,
                                PagedAndSortedResultRequestDto
                                >,
                                IUtenteAppService
{

    //dependency injection
    private readonly IGuidGenerator _guidGenerator;
    private readonly UserManager _userManager;
    private readonly IUserRepository _userRepository;

    //constructor
    public UtenteAppService(IUserRepository repository, IGuidGenerator guidGenerator, UserManager userManager) : base(repository)
    {
        _guidGenerator = guidGenerator;
        _userRepository = repository;
        _userManager = userManager;
    }


    //Methods
    [AllowAnonymous]
    [HttpPost]
    [DisableValidation]
    public async Task<UserDto> CreateUserWithPolicies([FromBody]CreateUserDto input)
    {
        //create User
        var user = await _userManager.CreateAsync(
            _guidGenerator.Create(),
            input.Username,
            input.Name,
            input.Surname,
            input.Email,
            input.Telephone,
            input.PolicyIds
            );

        //set policies
        List<Policy> settedPolicies = new List<Policy>();
        if (input.PolicyIds != null && input.PolicyIds.Count() > 0)
        {
           settedPolicies = await _userManager.SetPoliciesAsync(user, input.PolicyIds);
        }

        //save
        await Repository.InsertAsync(user);

        //return confirm DTO
        var res = ObjectMapper.Map< User, UserDto>(user);
        res.Policies = ObjectMapper.Map<List<Policy>, List<PolicyDto>>(settedPolicies);
        return res;
    }


    [HttpGet("api/app/utente/user-by-id/{id}")]
    public async Task<ActionResult<UserDto>> getUserById(Guid id)
    {
        try
        {   
            //find user
            var user = await _userRepository.GetWithDetails(id);

            if (user == null)
            {
                return new NotFoundResult();
            }

            //find UserPolicy
            var userPolicies = await _userManager.GetUserPoliciesAsync(user);

            //return confirm DTO
            var res = ObjectMapper.Map<User, UserDto>(user);
            res.Policies = ObjectMapper.Map<List<Policy>, List<PolicyDto>>(userPolicies);
            return new OkObjectResult(res);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult("An error occured: " + ex.Message);
        }
    }


    [AllowAnonymous]
    [HttpPut]
    [DisableValidation]
    public async Task<ActionResult<UserDto>> UpdateUserWithPolicies([FromBody]UpdateUserDto input)
    {
        //find user
        var user = await _userRepository.GetWithDetails(input.Id);

        if (user == null)
        {
            return new NotFoundResult();
        }

        //updateUser
        user = await _userManager.UpdateAsync(
         user,
         input.Username,
         input.Name,
         input.Surname,
         input.Email,
         input.Telephone,
         input.PolicyIds
        );

        //save
        await Repository.UpdateAsync(user);

        //return confirm DTO
        var res = ObjectMapper.Map<User, UserDto>(user);
        var userPolicies = await _userManager.GetUserPoliciesAsync(user);
        res.Policies = ObjectMapper.Map<List<Policy>, List<PolicyDto>>(userPolicies);
        return new OkObjectResult(res);
    }


    [AllowAnonymous]
    [HttpPut]
    [DisableValidation]
    public async Task<ActionResult<UserDto>> updateUserPolicies([FromBody] UpdateUserPolicyDto input)
    {
        //find user
        var user = await _userRepository.GetWithDetails(input.Id);

        if (user == null)
        {
            return new NotFoundResult();
        }

        //updatePolicy
        await _userManager.SetPoliciesAsync(user, input.PolicyIds);

        //save
        await Repository.UpdateAsync(user);

        //return confirm DTO
        var res = ObjectMapper.Map<User, UserDto>(user);
        var userPolicies = await _userManager.GetUserPoliciesAsync(user);
        res.Policies = ObjectMapper.Map<List<Policy>, List<PolicyDto>>(userPolicies);
        return new OkObjectResult(res);
    }
}
