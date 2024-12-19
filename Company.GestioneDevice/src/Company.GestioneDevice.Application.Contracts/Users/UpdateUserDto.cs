﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.GestioneDevice.Users;

public class UpdateUserDto
{
    [Required]
    public Guid Id { get; set; }

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

}
