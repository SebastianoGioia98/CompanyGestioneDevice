using Volo.Abp.Modularity;

namespace Company.GestioneDevice;

public abstract class GestioneDeviceApplicationTestBase<TStartupModule> : GestioneDeviceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
