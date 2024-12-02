using Company.GestioneDevice.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Company.GestioneDevice.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(GestioneDeviceEntityFrameworkCoreModule),
    typeof(GestioneDeviceApplicationContractsModule)
)]
public class GestioneDeviceDbMigratorModule : AbpModule
{
}
