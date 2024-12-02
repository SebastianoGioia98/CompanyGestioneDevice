using Volo.Abp.Modularity;

namespace Company.GestioneDevice;

[DependsOn(
    typeof(GestioneDeviceApplicationModule),
    typeof(GestioneDeviceDomainTestModule)
)]
public class GestioneDeviceApplicationTestModule : AbpModule
{

}
