using Company.GestioneDevice.Features;
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
    private readonly IGuidGenerator _guidGenerator;
    private readonly IPolicyRepository _policyRepository;
    private readonly IFeatureRepository _featureRepository;



    //Constructor
    public PolicyDataSeeder(IGuidGenerator guidGenerator, IPolicyRepository policyRepository, IFeatureRepository featureRepository)
    {
        _policyRepository = policyRepository;
        _featureRepository = featureRepository;
        _guidGenerator = guidGenerator;
    }


    //seeder Method
    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _policyRepository.GetCountAsync() <= 0)
        {
            //   === Comunication ===
            Guid policyId = _guidGenerator.Create();
            await _policyRepository.InsertAsync(
                new Policy
               (
                    policyId,
                    "comunication"
                ),
                autoSave: true
            );


            //ComunicationFeature
            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "send_sms",
                    policyId
                ),
                autoSave: true
            );

            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "internet_access",
                    policyId
                ),
                autoSave: true
            );

            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "call",
                    policyId
                ),
                autoSave: true
            );

            //   === Data Managment ===
            policyId = _guidGenerator.Create();
            await _policyRepository.InsertAsync(
                new Policy
               (
                    policyId,
                    "data_managment"
                ),
                autoSave: true
            );

            //data_managmentFeeature
            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "save_data",
                    policyId
                ),
                autoSave: true
            );

            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "usb_data_transfert",
                    policyId
                ),
                autoSave: true
            );





            //   === Hardware Managment ===
            policyId = _guidGenerator.Create();
            await _policyRepository.InsertAsync(
                 new Policy
               (
                    policyId,
                    "hardware_managment"
                ),
                autoSave: true
            );


            //Hardware Managment Feature
            await _featureRepository.InsertAsync(
               new Feature
              (
                   _guidGenerator.Create(),
                   "camera_access",
                   policyId
               ),
               autoSave: true
           );

            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "gps_access",
                    policyId
                ),
                autoSave: true
            );




            //   === Biometrics ===
            policyId = _guidGenerator.Create();
            await _policyRepository.InsertAsync(
                new Policy
               (
                    policyId,
                    "biometrics"
                ),
                autoSave: true
            );


            //Biometrics Feature
            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "heartbeat_sensor",
                    policyId
                ),
                autoSave: true
            );

            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "blood_pressure_sensor",
                    policyId
                ),
                autoSave: true
            );


            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "saturation_sensor",
                    policyId
                ),
                autoSave: true
            );




        }
    }

}

