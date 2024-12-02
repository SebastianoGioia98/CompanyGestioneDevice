using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Company.GestioneDevice.Data;
using Volo.Abp.DependencyInjection;

namespace Company.GestioneDevice.EntityFrameworkCore;

public class EntityFrameworkCoreGestioneDeviceDbSchemaMigrator
    : IGestioneDeviceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreGestioneDeviceDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the GestioneDeviceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<GestioneDeviceDbContext>()
            .Database
            .MigrateAsync();
    }
}
