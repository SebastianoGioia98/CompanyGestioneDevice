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


    public SolutionDataSeederContributor(
        UserDataSeeder userDataSeeder
        )
    {
        _userDataSeeder = userDataSeeder;
    }
    public async Task SeedAsync(DataSeedContext context)
    {
#if DEBUG
        await _userDataSeeder.SeedAsync(context);
      
#endif
    }
}
