using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace Company.GestioneDevice.Users;

public interface IUserAppService: ICrudAppService <
    UserDto,
    Guid,
    PagedAndSortedResultRequestDto
    >
{ 
    public Task<UserDto> CreateUserWithPolicies(UserDto input);
}
