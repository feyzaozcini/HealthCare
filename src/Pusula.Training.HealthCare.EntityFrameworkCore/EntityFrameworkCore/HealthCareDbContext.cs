﻿using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.DepartmentServices;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Titles;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class HealthCareDbContext :
    AbpDbContext<HealthCareDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Protocol> Protocols { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<PatientCompany> PatientCompanies { get; set; } = null!;
    public DbSet<Title> Titles { get; set; } = null!;
    public DbSet<DepartmentService> DepartmentServices { get; set; } = null!;

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
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

    public HealthCareDbContext(DbContextOptions<HealthCareDbContext> options)
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
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */
        if (builder.IsHostDatabase())
        {

            // Patient tablosu için sequence tanımlama
            builder.HasSequence<int>("PatientNoSequence").StartsAt(10000).IncrementsBy(1);

            // Migrationdan önce kontrol edilmesi gereken tablolar
            builder.Entity<Patient>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Patients", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.FirstName).HasColumnName(nameof(Patient.FirstName)).IsRequired().HasMaxLength(PatientConsts.FirstNameMaxLength);
                b.Property(x => x.LastName).HasColumnName(nameof(Patient.LastName)).IsRequired().HasMaxLength(PatientConsts.LastNameMaxLength);
                b.Property(x => x.BirthDate).HasColumnName(nameof(Patient.BirthDate));
                b.Property(x => x.IdentityNumber).HasColumnName(nameof(Patient.IdentityNumber)).HasMaxLength(PatientConsts.IdentityNumberMaxLength);
                b.Property(x => x.PassportNumber).HasColumnName(nameof(Patient.PassportNumber));
                b.Property(x => x.Email).HasColumnName(nameof(Patient.Email)).IsRequired().HasMaxLength(PatientConsts.EmailAddressMaxLength);
                b.Property(x => x.MobilePhoneNumber).HasColumnName(nameof(Patient.MobilePhoneNumber)).IsRequired().HasMaxLength(PatientConsts.MobilePhoneNumberMaxLength);
                b.Property(x => x.EmergencyPhoneNumber).HasColumnName(nameof(Patient.EmergencyPhoneNumber)).HasMaxLength(PatientConsts.MobilePhoneNumberMaxLength);
                b.Property(x => x.Gender).HasColumnName(nameof(Patient.Gender)).IsRequired();
                b.Property(x => x.No).HasColumnName(nameof(Patient.No)).IsRequired().HasDefaultValueSql("nextval('\"PatientNoSequence\"')");
                b.Property(x => x.MotherName).HasColumnName(nameof(Patient.MotherName));
                b.Property(x => x.FatherName).HasColumnName(nameof(Patient.FatherName));
                b.Property(x => x.BloodType).HasColumnName(nameof(Patient.BloodType));
                b.Property(x => x.Type).HasColumnName(nameof(Patient.Type)).IsRequired();
                b.HasOne<PatientCompany>().WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Country>().WithMany().IsRequired().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Department>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Departments", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Department.Name)).IsRequired().HasMaxLength(DepartmentConsts.NameMaxLength);
            });

            builder.Entity<Country>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Countries", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Country.Name)).IsRequired().HasMaxLength(CountryConsts.NameMaxLength);
                b.Property(x => x.Code).HasColumnName(nameof(Country.Code)).IsRequired().HasMaxLength(CountryConsts.CodeMaxLength);
            });

            builder.Entity<Protocol>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Protocols", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Type).HasColumnName(nameof(Protocol.Type)).IsRequired().HasMaxLength(ProtocolConsts.TypeMaxLength);
                b.Property(x => x.StartTime).HasColumnName(nameof(Protocol.StartTime));
                b.Property(x => x.EndTime).HasColumnName(nameof(Protocol.EndTime));
                b.HasOne<Patient>().WithMany().IsRequired().HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Department>().WithMany().IsRequired().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<PatientCompany>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "PatientCompanies", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(PatientCompany.Name)).IsRequired().HasMaxLength(PatientCompanyConsts.NameMaxLength);
            });

            builder.Entity<Title>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Titles", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Title.Name)).IsRequired().HasMaxLength(TitleConsts.NameMaxLength);
            });

            builder.Entity<DepartmentService>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "DepartmentServices", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(DepartmentService.Name)).IsRequired().HasMaxLength(DepartmentServiceConsts.NameMaxLength);
            });
        }

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(HealthCareConsts.DbTablePrefix + "YourEntities", HealthCareConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}
