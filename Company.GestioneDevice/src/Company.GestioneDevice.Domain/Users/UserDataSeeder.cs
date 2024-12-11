using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Company.GestioneDevice.Users;

public class UserDataSeeder: ITransientDependency
{
    private readonly IUserRepository _userRepository;
    private readonly IGuidGenerator _guidGenerator;


    //Constructor
    public UserDataSeeder(IUserRepository userRepository, IGuidGenerator guidGenerator)
    {
        _userRepository = userRepository;
        _guidGenerator = guidGenerator;
    }


    //seeder Method
    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _userRepository.GetCountAsync() <= 0)
        {
            await _userRepository.InsertAsync(
                new User
               (
                    _guidGenerator.Create(),
                    "userMB",
                     "Marco",
                     "Bianchi",
                     "marcobianchi@company.com",
                    "0102855664"
                ),
                autoSave: true
            );

            await _userRepository.InsertAsync(
                new User
                (
                    _guidGenerator.Create(),
                    "userMR",
                    "Marco",
                    "Rossi",
                    "marcorossi@company.com",
                    "0102855934"
                ),
                autoSave: true
            );
        }
    }

}
