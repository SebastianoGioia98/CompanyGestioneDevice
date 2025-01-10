using Company.GestioneDevice.Policies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.GestioneDevice.Users;

public class UserDetailedDto : UserDto
{


    [Required]
    public List<PolicyDto> Policies { get; set; } = new List<PolicyDto>();

    public DateTime CreationTime { get; set; }
}
