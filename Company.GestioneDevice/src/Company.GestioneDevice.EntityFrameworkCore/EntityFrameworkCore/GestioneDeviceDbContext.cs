using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Company.GestioneDevice.Users;
using Company.GestioneDevice.Devices;
using Company.GestioneDevice.Devices.SoftwareVersions;
using Company.GestioneDevice.Devices.DeviceFeatures;
using Company.GestioneDevice.Policies;
using Company.GestioneDevice.Users.UserPolicies;
using Company.GestioneDevice.Features;

namespace Company.GestioneDevice.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class GestioneDeviceDbContext :
    AbpDbContext<GestioneDeviceDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    #region Domain Entities
    public DbSet<User> CompanyUsers { get; set; }
    public DbSet<Device> Devices { get; set; }

    public DbSet<Feature> Features { get; set; }
    public DbSet<DeviceFeature> DeviceFeatures { get; set; }

    public DbSet<Policy> Policies { get; set; }
    public DbSet<UserPolicy> UserPolicies { get; set; }

    #endregion


    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public GestioneDeviceDbContext(DbContextOptions<GestioneDeviceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(GestioneDeviceConsts.DbTablePrefix + "YourEntities", GestioneDeviceConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<User>(b =>
        {
            b.ToTable(GestioneDeviceConsts.DbTablePrefix + "Users", GestioneDeviceConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasKey(x => x.Id);

            b.HasMany(x => x.UserPolicies)
           .WithOne()
           .HasForeignKey(x => x.PolicyId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Device>(b =>
        {
            b.ToTable(GestioneDeviceConsts.DbTablePrefix + "Devices", GestioneDeviceConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(GestioneDeviceSharedConsts.MaxNameLength);

            b.HasMany(x => x.SoftwareVersions)
            .WithOne()
            .HasForeignKey(x => x.DeviceId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.DeviceFeatures)
            .WithOne()
            .HasForeignKey(x => x.DeviceId)
            .IsRequired();
            //.OnDelete(DeleteBehavior.Cascade);

            b.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .IsRequired();

        });

        builder.Entity<SoftwareVersion>(b =>
        {
            b.ToTable(GestioneDeviceConsts.DbTablePrefix + "SoftwareVersions", GestioneDeviceConsts.DbSchema);

            b.HasKey(sv => sv.Id);
        });

        builder.Entity<Feature>(b =>
        {
            b.ToTable(GestioneDeviceConsts.DbTablePrefix + "Features", GestioneDeviceConsts.DbSchema);
            b.HasKey(d => d.Id);
            b.Property(d => d.Name).IsRequired();

            b.HasOne<Policy>().WithMany().HasForeignKey(x => x.PolicyId).IsRequired();
        });


        builder.Entity<DeviceFeature>(b =>
        {
            b.ToTable(GestioneDeviceConsts.DbTablePrefix + "DeviceFeatures", GestioneDeviceConsts.DbSchema);
            b.HasKey(df => new { df.DeviceId, df.FeatureId });

            b.HasOne<Device>()
                  .WithMany(d => d.DeviceFeatures)
                  .HasForeignKey(df => df.DeviceId)
                  .IsRequired();

            b.HasOne<Feature>()
                  .WithMany()
                  .HasForeignKey(df => df.FeatureId)
                  .IsRequired();
        });


        builder.Entity<Policy>(b =>
        {
            b.ToTable(GestioneDeviceConsts.DbTablePrefix + "Policies", GestioneDeviceConsts.DbSchema);
            b.HasKey(d => d.Id);
            b.Property(d => d.Name).IsRequired();
        });


        builder.Entity<UserPolicy>(b =>
        {
            b.ToTable(GestioneDeviceConsts.DbTablePrefix + "UserPolicies", GestioneDeviceConsts.DbSchema);
            b.HasKey(df => new { df.UserId, df.PolicyId });

            b.HasOne<User>()
                  .WithMany(d => d.UserPolicies)
                  .HasForeignKey(df => df.UserId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);


            b.HasOne<Policy>()
                  .WithMany()
                  .HasForeignKey(df => df.PolicyId)
                  .IsRequired();



        });
    }
}
