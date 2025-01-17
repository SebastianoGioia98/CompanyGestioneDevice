using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.GestioneDevice.shared;

public class SortByDto
{
    [BindRequired]
    public string Key { get; set; } // Es. 'type', 'id', 'name', ecc.

    [BindRequired]
    public string Order { get; set; } // Es. 'asc' o 'desc'
}
