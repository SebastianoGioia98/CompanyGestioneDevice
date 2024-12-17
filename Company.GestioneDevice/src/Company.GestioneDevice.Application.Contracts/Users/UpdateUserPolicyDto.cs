using Company.GestioneDevice.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.GestioneDevice.Users;

public class UpdateUserPolicyDto
{
    public Guid Id {  get; set; }

    public List<Guid> PolicyIds { get; set; } = new List<Guid>();
}
