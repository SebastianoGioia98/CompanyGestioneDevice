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
using System.Linq.Dynamic.Core;

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


    [HttpGet("api/app/utente/user-list")]
    public async Task<ActionResult<shared.PagedResultDto<UserDto>>> GetDeviceList(
     [FromQuery] int pageNumber = 1, // Numero di pagina richiesto
     [FromQuery] int itemsPerPage = 10, // Numero di elementi per pagina
     [FromQuery] string sortByKey = "Id",  // Valore predefinito per Key
     [FromQuery] string sortByOrder = "asc", // Valore predefinito per Order
     [FromQuery] string userName = null // Valore predefinito per Order
   )
    {




        // Get the IQueryable<Device> from the repository
        var query = await _repository.GetQueryableAsync();

       

        // Apply filters
        if (userName != null)
        {
            query = query.Where(x => x.Username.Contains(userName));
        }

        // Applica ordinamento dinamico
        var allowedKeys = new[] { "username", "name", "surname", "email" }; // Campi accettabili

        if (sortByKey != null && !string.IsNullOrWhiteSpace(sortByKey) && !string.IsNullOrWhiteSpace(sortByOrder) &&
            allowedKeys.Contains(sortByKey.ToLower()))
        {
            var key = ToPascalCase(sortByKey); // Converte "type" in "Type"
            var order = sortByOrder.ToLower();


           
                // Usa "device.<key>" per specificare esplicitamente il percorso
                query = query.AsQueryable().OrderBy($"{key} {order}");

            


        }
        else
        {
            // Default sorting (se il parametro sortBy non è presente o non è valido)
            query = query.OrderBy(x => x.Id);
        }

        // **Calculate total count**
        var totalCount = await AsyncExecuter.CountAsync(query);

        // Calcola il numero totale di pagine
        var totalPages = totalCount > 0
            ? (int)Math.Ceiling((double)totalCount / itemsPerPage)
            : 1; // Almeno una pagina anche se vuota

        // Ensure pageNumber is within bounds
        if (pageNumber < 1)
        {
            pageNumber = 1; // Default to the first page
        }
        else if (pageNumber > totalPages)
        {
            pageNumber = totalPages; // Default to the last page
        }

        // **Calculate skipCount**
        var skipCount = Math.Max(0, (pageNumber - 1) * itemsPerPage);

        // Apply paging to the query
        query = query
            .Skip(skipCount)
            .Take(itemsPerPage);

        // Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        // Convert the query result to a list of DeviceDto objects
        var userDtos = queryResult.Select(x =>
        {
            var userDto = ObjectMapper.Map<User, UserDto>(x);
            
            return userDto;
        }).ToList();

        // **Prepare the response DTO**
        var result = new shared.PagedResultDto<UserDto>
        {
            TotalItems = totalCount,
            TotalPages = totalPages,
            Items = userDtos
        };

        return new OkObjectResult(result);
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




    private string ToPascalCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        return char.ToUpper(input[0]) + input.Substring(1);
    }
}
