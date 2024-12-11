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

public class UserAppService : CrudAppService<
                                User,
                                UserDto,
                                Guid,
                                PagedAndSortedResultRequestDto
                                >,
                                IUserAppService
{

    //dependency injection
    private readonly IGuidGenerator _guidGenerator;
    private readonly UserManager _userManager;
    private readonly IUserRepository _userRepository;

    //constructor
    public UserAppService(IUserRepository repository, IGuidGenerator guidGenerator, UserManager userManager, IUserRepository userRepository) : base(repository)
    {
        _guidGenerator = guidGenerator;
        _userManager = userManager;
        _userRepository = userRepository;
    }


    //Methods
    [HttpPost("user-with-policies")]
    public async Task<UserDto> CreateUserWithPolicies([FromBody] UserDto input)
    {
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
        if (input.PolicyIds != null && input.PolicyIds.Count() > 0)
        {
           await _userManager.SetPoliciesAsync(user, input.PolicyIds);
        }

        //save
        await Repository.InsertAsync(user);

        //return confirm DTO
        return ObjectMapper.Map<User, UserDto>(user);
    }
}
