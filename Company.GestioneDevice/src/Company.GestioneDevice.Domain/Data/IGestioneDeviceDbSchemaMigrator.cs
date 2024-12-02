using System.Threading.Tasks;

namespace Company.GestioneDevice.Data;

public interface IGestioneDeviceDbSchemaMigrator
{
    Task MigrateAsync();
}
