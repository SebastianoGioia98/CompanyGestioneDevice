﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Company.GestioneDevice.Features;


public interface IFeatureRepository : IRepository<Feature, Guid>
{

}
