using Company.GestioneDevice.Devices;
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
using Volo.Abp.Domain.Entities;
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
    private readonly IUserRepository _repository;

    //constructor
    public UtenteAppService(IUserRepository repository, IGuidGenerator guidGenerator, UserManager userManager) : base(repository)
    {
        _guidGenerator = guidGenerator;
        _repository = repository;
        _userManager = userManager;
    }


    //Methods
    [AllowAnonymous]
    [HttpPost]
    public async Task<UserDetailedDto> CreateUserWithDetails([FromBody]CreateUserDto input)
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
        List<Policy> settedPolicies = await _userManager.SetPoliciesAsync(user, input.PolicyIds);
        

        //save
        await _repository.InsertAsync(user);

        //return confirm DTO
        var res = ObjectMapper.Map< User, UserDetailedDto>(user);
        res.Policies = ObjectMapper.Map<List<Policy>, List<PolicyDto>>(settedPolicies);
        return res;
    }


    [HttpGet("api/app/utente/user-by-id/{id}")]
    public async Task<ActionResult<UserDetailedDto>> getUserById(Guid id)
    {
        try
        {

            //Get the IQueryable<User> from the repository
            var queryable = await _repository.CompleteJoin();


            //Prepare a query to join user and policy
            var policyQueryable = await _userManager.GetQueryablePolicy();

            var query = from user in queryable
                        where user.Id == id
                        select new
                        {
                            User = user,
                            Policies = user.UserPolicies
                                        .Join(policyQueryable,
                                              up => up.PolicyId,
                                              p => p.Id,
                                              (df, f) => f)
                                        .ToList()
                        };

            //Execute the query and check
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }


            //   --- prepare Dto ---
            var res = ObjectMapper.Map<User, UserDetailedDto>(queryResult.User);
            res.Policies = ObjectMapper.Map<List<Policy>, List<PolicyDto>>(queryResult.Policies);
            return new OkObjectResult(res);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult("An error occured: " + ex.Message);
        }
    }


    [AllowAnonymous]
    [HttpPut]
    public async Task<ActionResult<UserDto>> UpdateUserWithDetails([FromBody] UpdateUserDto input)
    {
        //Get the IQueryable<Device> from the repository
        var queryable = await _repository.CompleteJoin();

        //find device
        var query = from u in queryable
                    where u.Id == input.Id
                    select u;

        var user = await AsyncExecuter.FirstOrDefaultAsync(query);

        //check device null
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
         input.Telephone
        );

        //save
        await Repository.UpdateAsync(user);

        //return confirm DTO
        var res = ObjectMapper.Map<User, UserDto>(user);
    
        return new OkObjectResult(res);
    }


    [AllowAnonymous]
    [HttpPut]
    public async Task<ActionResult<UserDetailedDto>> AssignUserPolicies([FromBody] UpdateUserPolicyDto input)
    {
        //Get the IQueryable<Device> from the repository
        var queryable = await _repository.CompleteJoin();

        //find device
        var query = from u in queryable
                    where u.Id == input.Id
                    select u;

        var user = await AsyncExecuter.FirstOrDefaultAsync(query);

        //check device null
        if (user == null)
        {
            return new NotFoundResult();
        }


        //set policies
        List<Policy> settedPolicies = await _userManager.SetPoliciesAsync(user, input.PolicyIds);


        //save
        await Repository.UpdateAsync(user);

        //return confirm DTO
        var res = ObjectMapper.Map<User, UserDetailedDto>(user);
        res.Policies = ObjectMapper.Map<List<Policy>, List<PolicyDto>>(settedPolicies);
        return new OkObjectResult(res);
    }
}
