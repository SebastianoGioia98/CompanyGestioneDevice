using Company.GestioneDevice.Users.UserPolicies;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Company.GestioneDevice.Users;

public class User : AuditedAggregateRoot<Guid>
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string? Telephone { get; set; }

    //navigation properties
    public List<UserPolicie> UserPolicies { get; set; } = new List<UserPolicie>();




    //Constructors
    public User(Guid id, string username, string name, string surname, string email, string? telephone = null) : base(id)
    {
        Username = username;
        Name = name;
        Surname = surname;
        Email = email;
        Telephone = telephone ?? null;
    }

    public User()
    {

    }
}
