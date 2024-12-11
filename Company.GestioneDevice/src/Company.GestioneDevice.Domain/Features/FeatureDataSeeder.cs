using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace Company.GestioneDevice.Features;

public class FeatureDataSeeder : ITransientDependency
{
    private readonly IFeatureRepository _featureRepository;
    private readonly IGuidGenerator _guidGenerator;


    //Constructor
    public FeatureDataSeeder(IFeatureRepository featureRepository, IGuidGenerator guidGenerator)
    {
        _featureRepository = featureRepository;
        _guidGenerator = guidGenerator;
    }


    //seeder Method
    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _featureRepository.GetCountAsync() <= 0)
        {
            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "send_sms"
                ),
                autoSave: true
            );
            
            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "internet_access"
                ),
                autoSave: true
            );
            
            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "call"
                ),
                autoSave: true
            );
            
            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "save_data"
                ),
                autoSave: true
            );

            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "usb_data_transfert"
                ),
                autoSave: true
            );

            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "camera_access"
                ),
                autoSave: true
            );

            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "gps_access"
                ),
                autoSave: true
            );

            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "heartbeat_sensor"
                ),
                autoSave: true
            );
            
            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "blood_pressure_sensor"
                ),
                autoSave: true
            );   
            
            
            await _featureRepository.InsertAsync(
                new Feature
               (
                    _guidGenerator.Create(),
                    "saturation_sensor"
                ),
                autoSave: true
            );






        }
    }

}
