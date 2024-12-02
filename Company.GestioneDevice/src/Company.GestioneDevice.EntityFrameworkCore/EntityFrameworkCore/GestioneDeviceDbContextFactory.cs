using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Company.GestioneDevice.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class GestioneDeviceDbContextFactory : IDesignTimeDbContextFactory<GestioneDeviceDbContext>
{
    public GestioneDeviceDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        GestioneDeviceEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<GestioneDeviceDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new GestioneDeviceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Company.GestioneDevice.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
