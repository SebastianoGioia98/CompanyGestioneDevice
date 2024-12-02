using Volo.Abp.Modularity;

namespace Company.GestioneDevice;

[DependsOn(
    typeof(GestioneDeviceDomainModule),
    typeof(GestioneDeviceTestBaseModule)
)]
public class GestioneDeviceDomainTestModule : AbpModule
{

}
