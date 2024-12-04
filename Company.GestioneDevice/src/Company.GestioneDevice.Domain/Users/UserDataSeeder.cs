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
    private readonly IRepository<User, Guid> _userRepository;
    private readonly IGuidGenerator _GuidGenerator;


    //Constructor
    public UserDataSeeder(IRepository<User, Guid> userRepository, IGuidGenerator guidGenerator)
    {
        _userRepository = userRepository;
        _GuidGenerator = guidGenerator;
    }


    //seeder Method
    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _userRepository.GetCountAsync() <= 0)
        {
            await _userRepository.InsertAsync(
                new User
                {
                    Username = "userMB",
                    Name = "Marco",
                    Surname = "Bianchi",
                    Email = "marcobianchi@company.com",
                    Telephone = "0102855664"
                },
                autoSave: true
            );

            await _userRepository.InsertAsync(
                new User
                {
                    Username = "userMR",
                    Name = "Marco",
                    Surname = "Rossi",
                    Email = "marcorossi@company.com",
                    Telephone = "0102855934"
                },
                autoSave: true
            );
        }
    }

}
