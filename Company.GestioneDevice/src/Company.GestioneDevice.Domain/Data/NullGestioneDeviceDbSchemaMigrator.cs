using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Company.GestioneDevice.Data;

/* This is used if database provider does't define
 * IGestioneDeviceDbSchemaMigrator implementation.
 */
public class NullGestioneDeviceDbSchemaMigrator : IGestioneDeviceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
