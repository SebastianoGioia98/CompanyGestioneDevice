﻿using Company.GestioneDevice.Features;
using Company.GestioneDevice.Policies;
using Company.GestioneDevice.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Company.GestioneDevice;

public class SolutionDataSeederContributor : IDataSeedContributor, ITransientDependency
{

    protected readonly UserDataSeeder _userDataSeeder;
    protected readonly PolicyDataSeeder _policyDataSeeder;
    protected readonly FeatureDataSeeder _featureDataSeeder;

    public SolutionDataSeederContributor(
        UserDataSeeder userDataSeeder,
        PolicyDataSeeder policyDataSeeder,
        FeatureDataSeeder featureDataSeeder
        )
    {
        _userDataSeeder = userDataSeeder;
        _policyDataSeeder = policyDataSeeder;
        _featureDataSeeder = featureDataSeeder;
    }
    public async Task SeedAsync(DataSeedContext context)
    {
#if DEBUG
        await _userDataSeeder.SeedAsync(context); 
        await _policyDataSeeder.SeedAsync(context);
        await _featureDataSeeder.SeedAsync(context);
#endif
    }
}
