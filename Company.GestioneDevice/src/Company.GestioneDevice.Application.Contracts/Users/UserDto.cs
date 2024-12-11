using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Company.GestioneDevice.Users;

public class UserDto : EntityDto
{
    [Required]
    [StringLength(GestioneDeviceSharedConsts.MaxNameLength)]
    public string Username { get; set; }

    [Required]
    [StringLength(GestioneDeviceSharedConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    [StringLength(GestioneDeviceSharedConsts.MaxNameLength)]
    public string Surname { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(GestioneDeviceSharedConsts.MaxNameLength)]
    public string Email { get; set; }


    [StringLength(GestioneDeviceSharedConsts.MaxNameLength)]
    public string? Telephone { get; set; }

    [Required]
    public List<Guid> PolicyIds { get; set; } = new List<Guid>();


}
