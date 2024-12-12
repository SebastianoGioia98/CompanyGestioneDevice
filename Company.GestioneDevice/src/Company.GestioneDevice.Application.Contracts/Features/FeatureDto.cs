using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Company.GestioneDevice.Features;

public class FeatureDto : EntityDto
{
    public string Name { get; set; }

    public string State { get; set; }
}
