using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace Company.GestioneDevice.Policies;


public class PolicyDataSeeder : ITransientDependency
{
	private readonly IPolicyRepository _policyRepository;
	private readonly IGuidGenerator _guidGenerator;


	//Constructor
	public PolicyDataSeeder(IPolicyRepository policyRepository, IGuidGenerator guidGenerator)
	{
		_policyRepository = policyRepository;
		_guidGenerator = guidGenerator;
	}


	//seeder Method
	public async Task SeedAsync(DataSeedContext context)
	{
		if (await _policyRepository.GetCountAsync() <= 0)
		{
			await _policyRepository.InsertAsync(
				new Policy
			   (
					_guidGenerator.Create(),
					"comunication"
				),
				autoSave: true
			);

			await _policyRepository.InsertAsync(
				 new Policy
			   (
					_guidGenerator.Create(),
					"data_managment"
				),
				autoSave: true
			);

			await _policyRepository.InsertAsync(
				 new Policy
			   (
					_guidGenerator.Create(),
					"hardware_managment"
				),
			    autoSave: true
			);

            await _policyRepository.InsertAsync(
				new Policy
			   (
					_guidGenerator.Create(),
                    "biometrics"
                ),
				autoSave: true
			);




        }
	}

}

