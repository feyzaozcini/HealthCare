﻿using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.DepartmentServices;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Titles;
using Pusula.Training.HealthCare.TestGroupItems;
using Pusula.Training.HealthCare.TestGroups;
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
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.DoctorDepartments;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Villages;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.DiagnosisGroups;
using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.PhysicalExaminations;
using Pusula.Training.HealthCare.PshychologicalStates;
using Pusula.Training.HealthCare.FallRisks;
using System;
using Pusula.Training.HealthCare.TestProcesses;
using Pusula.Training.HealthCare.TestValueRanges;
using Pusula.Training.HealthCare.DoctorAppoinmentTypes;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Notes;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.AppointmentRules;
using Pusula.Training.HealthCare.PainTypes;
using Pusula.Training.HealthCare.PainDetails;
using Pusula.Training.HealthCare.DoctorWorkSchedules;
using Pusula.Training.HealthCare.BlackLists;
using Pusula.Training.HealthCare.SystemChecks;
using Pusula.Training.HealthCare.FollowUpPlans;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.FamilyHistories;
using Pusula.Training.HealthCare.ControlNotes;
using Pusula.Training.HealthCare.PatientHistories;


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
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<PatientCompany> PatientCompanies { get; set; } = null!;
    public DbSet<Title> Titles { get; set; } = null!;
    public DbSet<DepartmentService> DepartmentServices { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<TestGroup> TestGroups { get; set; } = null!;
    public DbSet<TestGroupItem> TestGroupItems { get; set; } = null!;
    public DbSet<LabRequest> LabRequests { get; set; } = null!;
    public DbSet<TestValueRange> TestValueRanges { get; set; } = null!;
    public DbSet<TestProcess> TestProcesses { get; set; } = null!;
    public DbSet<AppointmentType> AppointmentTypes { get; set; } = null!;
    public DbSet<District> Districts { get; set; } = null!;
    public DbSet<Village> Villages { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<DoctorDepartment> DoctorDepartments { get; set; } = null!;
    public DbSet<DoctorAppoinmentType> DoctorAppoinmentTypes { get; set; } = null!;
    public DbSet<ProtocolType> ProtocolTypes { get; set; } = null!;
    public DbSet<DiagnosisGroup> DiagnosisGroups { get; set; } = null!;
    public DbSet<Diagnosis> Diagnoses { get; set; } = null!;
    public DbSet<Anamnesis> Anamneses { get; set; } = null!;
    public DbSet<ExaminationDiagnosis> ExaminationDiagnoses { get; set; } = null!;
    public DbSet<PhysicalExamination> PhysicalExaminations { get; set; } = null!;
    public DbSet<PshychologicalState> PshychologicalStates { get; set; } = null!;
    public DbSet<FallRisk> FallRisks { get; set; } = null!;
    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<Insurance> Insurances { get; set; } = null!;
    public DbSet<AppointmentRule> AppointmentRules { get; set; }
    public DbSet<PainType> PainTypes { get; set; } = null!;
    public DbSet<PainDetail> PainDetails { get; set; } = null!;
    public DbSet<SystemCheck> SystemChecks { get; set; } = null!;
    public DbSet<FollowUpPlan> FollowUpPlans { get; set; } = null!;
    public DbSet<DoctorWorkSchedule> DoctorWorkSchedules { get; set; } = null!;
    public DbSet<BlackList> BlackLists { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;

    public DbSet<FamilyHistory> FamilyHistories { get; set; } = null!;
    public DbSet<ControlNote> ControlNotes { get; set; } = null!;
    public DbSet<PatientHistory> PatientHistories { get; set; } = null!;




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

            builder.HasSequence<int>("ProtocolNoSequence").StartsAt(20000).IncrementsBy(1);


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
                b.Property(x => x.Email).HasColumnName(nameof(Patient.Email)).IsRequired(false).HasMaxLength(PatientConsts.EmailAddressMaxLength);
                b.Property(x => x.MobilePhoneNumber).HasColumnName(nameof(Patient.MobilePhoneNumber)).IsRequired().HasMaxLength(PatientConsts.PhoneNumberMaxLength);
                b.Property(x => x.EmergencyPhoneNumber).HasColumnName(nameof(Patient.EmergencyPhoneNumber)).HasMaxLength(PatientConsts.PhoneNumberMaxLength);
                b.Property(x => x.Gender).HasColumnName(nameof(Patient.Gender)).IsRequired();
                b.Property(x => x.No).HasColumnName(nameof(Patient.No)).IsRequired().HasDefaultValueSql("nextval('\"PatientNoSequence\"')");
                b.Property(x => x.MotherName).HasColumnName(nameof(Patient.MotherName));
                b.Property(x => x.FatherName).HasColumnName(nameof(Patient.FatherName));
                b.Property(x => x.BloodType).HasColumnName(nameof(Patient.BloodType));
                b.Property(x => x.Type).HasColumnName(nameof(Patient.Type)).IsRequired(false);
                b.HasOne<PatientCompany>().WithMany().IsRequired(false).HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
                b.HasMany(p => p.Addresses).WithOne(a => a.Patient).HasForeignKey(a => a.PatientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Address>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Addresses", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne(a => a.Country).WithMany().IsRequired(false).HasForeignKey(a => a.CountryId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(a => a.City).WithMany().IsRequired(false).HasForeignKey(a => a.CityId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(a => a.District).WithMany().IsRequired(false).HasForeignKey(a => a.DistrictId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(a => a.Village).WithMany().IsRequired(false).HasForeignKey(a => a.VillageId).OnDelete(DeleteBehavior.NoAction);
                b.Property(x => x.AddressDescription).IsRequired(false).HasColumnName(nameof(Address.AddressDescription));
                b.Property(a => a.IsPrimary).HasDefaultValue(false);
                b.HasOne(a => a.Patient).WithMany(p => p.Addresses).HasForeignKey(a => a.PatientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });


            builder.Entity<Protocol>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Protocols", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.StartTime).HasColumnName(nameof(Protocol.StartTime));
                b.Property(x => x.EndTime).HasColumnName(nameof(Protocol.EndTime));
                b.Property(x => x.ProtocolStatus).HasColumnName(nameof(Protocol.ProtocolStatus));
                b.Property(x => x.ProtocolTypeId).HasColumnName(nameof(Protocol.ProtocolType));
                b.Property(x => x.ProtocolNoteId).HasColumnName(nameof(Protocol.Note));
                b.Property(x => x.ProtocolInsuranceId).HasColumnName(nameof(Protocol.Insurance));
                b.Property(x => x.PatientId).HasColumnName(nameof(Protocol.Patient));
                b.Property(x => x.DepartmentId).HasColumnName(nameof(Protocol.Department));
                b.Property(x => x.DoctorId).HasColumnName(nameof(Protocol.Doctor));
                b.Property(x => x.No).HasColumnName(nameof(Protocol.No)).IsRequired().HasDefaultValueSql("nextval('\"ProtocolNoSequence\"')");
                b.HasOne(x => x.ProtocolType).WithMany().IsRequired().HasForeignKey(x => x.ProtocolTypeId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(x => x.Note).WithOne().HasForeignKey<Protocol>(x => x.ProtocolNoteId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(x => x.Insurance).WithMany().IsRequired().HasForeignKey(x => x.ProtocolInsuranceId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(x => x.Patient).WithMany().IsRequired().HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(x => x.Department).WithMany().IsRequired().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(x => x.Doctor).WithMany().IsRequired(false).HasForeignKey(x => x.DoctorId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Department>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Departments", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Department.Name)).IsRequired().HasMaxLength(DepartmentConsts.NameMaxLength);
                b.HasMany(x => x.Doctors).WithOne().HasForeignKey(x => x.DepartmentId).IsRequired();
            });



            builder.Entity<ProtocolType>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "ProtocolTypes", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(ProtocolType.Name)).IsRequired().HasMaxLength(ProtocolTypeConsts.NameMaxLength);
            });

            builder.Entity<Note>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Notes", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Text).HasColumnName(nameof(Note.Text)).IsRequired(false).HasMaxLength(NoteConsts.TextMaxLength);
            });

            builder.Entity<Insurance>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Insurances", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Insurance.Name)).IsRequired().HasMaxLength(InsuranceConsts.NameMaxLength);
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

            builder.Entity<Doctor>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Doctors", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.BirthDate).HasColumnName(nameof(Doctor.BirthDate)).IsRequired();
                b.Property(x => x.Gender).HasColumnName(nameof(Doctor.Gender)).IsRequired();
                b.Property(x => x.IdentityNumber).HasColumnName(nameof(Doctor.IdentityNumber)).IsRequired();
                b.Property(x => x.UserId).HasColumnName(nameof(Doctor.UserId)).IsRequired();
                b.Property(x => x.TitleId).HasColumnName(nameof(Doctor.TitleId)).IsRequired();
                //b.HasOne<IdentityUser>().WithOne().HasForeignKey<Doctor>(x => x.UserId).IsRequired();
                //b.HasOne<Title>().WithMany().HasForeignKey(x => x.TitleId).OnDelete(DeleteBehavior.NoAction);
                b.HasMany(e => e.DoctorDepartments).WithOne().HasForeignKey(x => x.DoctorId).IsRequired();
                b.HasOne(x => x.User) // Navigasyon özelliği bağlanıyor
        .WithOne()
        .HasForeignKey<Doctor>(x => x.UserId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(d => d.Title)
     .WithMany(t => t.Doctors)
     .HasForeignKey(d => d.TitleId)
     .IsRequired();

            });

            builder.Entity<TestGroup>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "TestGroups", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .HasColumnName(nameof(TestGroup.Name))
                    .IsRequired()
                    .HasMaxLength(TestGroupConsts.NameMaxLength);
            });

            builder.Entity<TestGroupItem>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "TestGroupItems", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .HasColumnName(nameof(TestGroupItem.Name))
                    .IsRequired()
                    .HasMaxLength(TestGroupItemConsts.NameMaxLength);

                b.Property(x => x.Code)
                    .HasColumnName(nameof(TestGroupItem.Code))
                    .IsRequired()
                    .HasMaxLength(TestGroupItemConsts.CodeMaxLength);

                b.Property(x => x.TestType)
                    .HasColumnName(nameof(TestGroupItem.TestType))
                    .IsRequired()
                    .HasMaxLength(TestGroupItemConsts.TestTypeMaxLength);

                b.Property(x => x.Description)
                    .HasColumnName(nameof(TestGroupItem.Description))
                    .HasMaxLength(TestGroupItemConsts.DescriptionMaxLength)
                    .IsRequired(false);


                b.Property(x => x.TurnaroundTime)
                    .HasColumnName(nameof(TestGroupItem.TurnaroundTime))
                    .IsRequired();

            });

            builder.Entity<LabRequest>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "LabRequests", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.Date)
                    .HasColumnName(nameof(LabRequest.Date))
                    .IsRequired();

                b.Property(x => x.Status)
                    .HasColumnName(nameof(LabRequest.Status))
                    .IsRequired();
                b.Property(x => x.ProtocolId)
                    .HasColumnName(nameof(LabRequest.ProtocolId))
                    .IsRequired();

                b.Property(x => x.DoctorId)
                    .HasColumnName(nameof(LabRequest.DoctorId))
                    .IsRequired();

                b.Property(x => x.Description)
                    .HasColumnName(nameof(LabRequest.Description))
                    .IsRequired(false);

                b.HasOne(x => x.Doctor)
                    .WithMany()
                    .HasForeignKey(x => x.DoctorId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Protocol)
                    .WithMany()
                    .HasForeignKey(x => x.ProtocolId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<TestProcess>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "TestProcesses", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.LabRequestId)
                    .HasColumnName(nameof(TestProcess.LabRequestId))
                    .IsRequired();

                b.Property(x => x.TestGroupItemId)
                    .HasColumnName(nameof(TestProcess.TestGroupItemId))
                    .IsRequired();

                b.Property(x => x.Status)
                    .HasColumnName(nameof(TestProcess.Status))
                    .HasConversion<int>()
                    .IsRequired();

                b.Property(x => x.Result)
                    .HasColumnName(nameof(TestProcess.Result))
                    .IsRequired(false);

                b.Property(x => x.ResultDate)
                    .HasColumnName(nameof(TestProcess.ResultDate))
                    .IsRequired(false);

                b.HasOne(x => x.LabRequest)
                    .WithMany()
                    .HasForeignKey(x => x.LabRequestId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(x => x.TestGroupItem)
                    .WithMany()
                    .HasForeignKey(x => x.TestGroupItemId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<TestValueRange>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "TestValueRanges", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.TestGroupItemId)
                    .HasColumnName(nameof(TestValueRange.TestGroupItemId))
                    .IsRequired();

                b.Property(x => x.MinValue)
                    .HasColumnName(nameof(TestValueRange.MinValue))
                    .IsRequired();

                b.Property(x => x.MaxValue)
                    .HasColumnName(nameof(TestValueRange.MaxValue))
                    .IsRequired();

                b.Property(x => x.Unit)
                    .HasColumnName(nameof(TestValueRange.Unit))
                    .IsRequired();

                b.HasOne(tvr => tvr.TestGroupItem)
                    .WithOne(tgi => tgi.TestValueRange)
                    .HasForeignKey<TestValueRange>(tvr => tvr.TestGroupItemId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            builder.Entity<AppointmentType>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "AppointmentTypes", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .HasColumnName(nameof(AppointmentType.Name))
                    .IsRequired()
                    .HasMaxLength(AppointmentTypeConst.NameMaxLength);

                b.Property(x => x.DurationInMinutes).HasColumnName(nameof(AppointmentType.DurationInMinutes)).IsRequired();
                b.HasMany(x => x.DoctorAppointmentTypes).WithOne().HasForeignKey(x => x.AppoinmentTypeId).IsRequired();
            });

            builder.Entity<DoctorDepartment>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "DoctorDepartments", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();

                // Composite Key
                b.HasKey(dd => new { dd.DoctorId, dd.DepartmentId });

                // Relationships
                b.HasOne(dd => dd.Doctor)
                    .WithMany(d => d.DoctorDepartments)
                    .HasForeignKey(dd => dd.DoctorId)
                    .IsRequired();

                b.HasOne(dd => dd.Department)
                    .WithMany(d => d.Doctors)
                    .HasForeignKey(dd => dd.DepartmentId)
                    .IsRequired();
            });
            builder.Entity<Country>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Countries", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Country.Name)).IsRequired().HasMaxLength(CountryConsts.NameMaxLength);
                b.Property(x => x.Code).HasColumnName(nameof(Country.Code)).IsRequired().HasMaxLength(CountryConsts.CodeMaxLength);
            });

            builder.Entity<City>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Cities", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(City.Name)).IsRequired().HasMaxLength(CityConsts.NameMaxLength);
                b.HasOne<Country>().WithMany().IsRequired().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<District>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Districts", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(District.Name)).IsRequired().HasMaxLength(DistrictConsts.NameMaxLength);
                b.HasOne<City>().WithMany().IsRequired().HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<DoctorAppoinmentType>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "DoctorAppoinmentTypes", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();

                // Composite Key
                b.HasKey(dd => new { dd.DoctorId, dd.AppoinmentTypeId });

                // İşlemler appointment type ile yapılacak

                b.HasOne(dd => dd.Doctor)
                    .WithMany()
                    .HasForeignKey(dd => dd.DoctorId)
                    .IsRequired();

                b.HasOne(dd => dd.AppoinmentType)
                    .WithMany(d => d.DoctorAppointmentTypes)
                    .HasForeignKey(dd => dd.AppoinmentTypeId)
                    .IsRequired();
            });

            builder.Entity<DiagnosisGroup>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "DiagnosisGroups", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(DiagnosisGroup.Name)).IsRequired().HasMaxLength(DiagnosisGroupConsts.NameMaxLength);
                b.Property(x => x.Code).HasColumnName(nameof(DiagnosisGroup.Code)).IsRequired().HasMaxLength(DiagnosisGroupConsts.CodeMaxLength);

            });


            builder.Entity<Diagnosis>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Diagnoses", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Diagnosis.Name)).IsRequired().HasMaxLength(DiagnosisConsts.NameMaxLength);
                b.Property(x => x.Code).HasColumnName(nameof(Diagnosis.Code)).IsRequired().HasMaxLength(DiagnosisConsts.CodeMaxLength);
                b.HasOne<DiagnosisGroup>().WithMany().HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Anamnesis>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Anamneses", HealthCareConsts.DbSchema);
                b.ConfigureByConvention(); 
                b.Property(x => x.Complaint).HasColumnName(nameof(Anamnesis.Complaint)).IsRequired();
                b.Property(x => x.Story).HasColumnName(nameof(Anamnesis.Story)).IsRequired();
                b.Property(x => x.StartDate).HasColumnName(nameof(Anamnesis.StartDate)).IsRequired();
                b.HasOne<Protocol>().WithOne().HasForeignKey<Anamnesis>(x => x.ProtocolId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<ExaminationDiagnosis>(b =>
            {

                b.ToTable(HealthCareConsts.DbTablePrefix + "ExaminationDiagnoses", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.DiagnosisType).HasColumnName(nameof(ExaminationDiagnosis.DiagnosisType)).IsRequired();
                b.Property(x => x.InitialDate).HasColumnName(nameof(ExaminationDiagnosis.InitialDate)).IsRequired();
                b.Property(x => x.Note).HasColumnName(nameof(ExaminationDiagnosis.Note)).HasMaxLength(ExaminationDiagnosisConsts.NoteMaxLength);
                b.HasOne<Protocol>().WithMany().HasForeignKey(x => x.ProtocolId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Diagnosis>().WithMany().HasForeignKey(x => x.DiagnosisId).OnDelete(DeleteBehavior.Cascade);

            });

            builder.Entity<PhysicalExamination>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "PhysicalExaminations", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Weight).HasColumnName(nameof(PhysicalExamination.Weight)).HasPrecision(5, 2);
                b.Property(x => x.Height).HasColumnName(nameof(PhysicalExamination.Height)).HasPrecision(5, 2);
                b.Property(x => x.BMI).HasColumnName(nameof(PhysicalExamination.BMI)).HasPrecision(5, 2);
                b.Property(x => x.VYA).HasColumnName(nameof(PhysicalExamination.VYA)).HasPrecision(5, 2);
                b.Property(x => x.Temperature).HasColumnName(nameof(PhysicalExamination.Temperature)).HasPrecision(5, 2);
                b.Property(x => x.Pulse).HasColumnName(nameof(PhysicalExamination.Pulse));
                b.Property(x => x.SystolicBP).HasColumnName(nameof(PhysicalExamination.SystolicBP));
                b.Property(x => x.DiastolicBP).HasColumnName(nameof(PhysicalExamination.DiastolicBP));
                b.Property(x => x.SPO2).HasColumnName(nameof(PhysicalExamination.SPO2));
                b.Property(x => x.Note).HasColumnName(nameof(PhysicalExamination.Note)).HasMaxLength(PhysicalExaminationConsts.NoteMaxLength);
                b.HasOne<Protocol>().WithOne().HasForeignKey<PhysicalExamination>(x => x.ProtocolId).OnDelete(DeleteBehavior.NoAction);

            });

            builder.Entity<PshychologicalState>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "PshychologicalStates", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Description).HasColumnName(nameof(PshychologicalState.Description)).IsRequired(false).HasMaxLength(PshychologicalStateConsts.DescriptionMaxLength);
                b.Property(x => x.MentalState).HasColumnName(nameof(PshychologicalState.MentalState));
                b.HasOne<Protocol>().WithOne().HasForeignKey<PshychologicalState>(x => x.ProtocolId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Village>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Villages", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Village.Name)).IsRequired().HasMaxLength(VillageConsts.NameMaxLength);
                b.HasOne<District>().WithMany().IsRequired().HasForeignKey(x => x.DistrictId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Appointment>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Appointments", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.StartDate).HasColumnName(nameof(Appointment.StartDate)).IsRequired();
                b.Property(x => x.EndDate).HasColumnName(nameof(Appointment.EndDate)).IsRequired();
                b.Property(x => x.AppointmentStatus).HasColumnName(nameof(Appointment.AppointmentStatus)).IsRequired();
                b.Property(x => x.IsBlock).HasColumnName(nameof(Appointment.IsBlock)).IsRequired();
                b.Property(x => x.Note).HasColumnName(nameof(Appointment.Note)).HasMaxLength(AppointmentConst.NoteMaxLength);
                b.HasOne(x=>x.Doctor).WithMany().HasForeignKey(x => x.DoctorId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(x=>x.Patient).WithMany().HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(x => x.Department).WithMany().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(x=>x.AppointmentType).WithMany().HasForeignKey(x => x.AppointmentTypeId).OnDelete(DeleteBehavior.NoAction);
            });


            builder.Entity<FallRisk>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "FallRisks", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Score).HasColumnName(nameof(FallRisk.Score));
                b.Property(x => x.Description).HasColumnName(nameof(FallRisk.Description));
                b.Property(x => x.HasFallHistory).HasColumnName(nameof(FallRisk.HasFallHistory));
                b.Property(x => x.UsesMedications).HasColumnName(nameof(FallRisk.UsesMedications));
                b.Property(x => x.HasAddiction).HasColumnName(nameof(FallRisk.HasAddiction));
                b.Property(x => x.HasBalanceDisorder).HasColumnName(nameof(FallRisk.HasBalanceDisorder));
                b.Property(x => x.HasVisionImpairment).HasColumnName(nameof(FallRisk.HasVisionImpairment));
                b.Property(x => x.MentalState).HasColumnName(nameof(FallRisk.MentalState));
                b.Property(x => x.GeneralHealthState).HasColumnName(nameof(FallRisk.GeneralHealthState));
                b.HasOne<Protocol>().WithOne().HasForeignKey<FallRisk>(x => x.ProtocolId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<AppointmentRule>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "AppointmentRules", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Gender).HasColumnName(nameof(AppointmentRule.Gender));
                b.Property(x => x.Age).HasColumnName(nameof(AppointmentRule.Age));
                b.Property(x => x.MinAge).HasColumnName(nameof(AppointmentRule.MinAge));
                b.Property(x => x.MaxAge).HasColumnName(nameof(AppointmentRule.MaxAge));
                b.Property(x => x.Description).HasColumnName(nameof(AppointmentRule.Description));
                b.HasOne(x => x.Doctor).WithMany().IsRequired(false).HasForeignKey(x => x.DoctorId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(x => x.Department).WithMany().IsRequired(false).HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<PainType>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "PainTypes", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(PainType.Name)).IsRequired().HasMaxLength(PainTypeConsts.NameMaxLength);
            });

            builder.Entity<PainDetail>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "PainDetails", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Area).HasColumnName(nameof(PainDetail.Area)).IsRequired().HasMaxLength(PainDetailConsts.AreaMaxLength);
                b.Property(x => x.ProtocolId).HasColumnName(nameof(PainDetail.ProtocolId)) .IsRequired();
                b.Property(x => x.Score).HasColumnName(nameof(PainDetail.Score)).IsRequired();
                b.Property(x => x.PainTypeId).HasColumnName(nameof(PainDetail.PainTypeId)).IsRequired();
                b.Property(x => x.Description) .HasColumnName(nameof(PainDetail.Description)).HasMaxLength(PainDetailConsts.DescriptionMaxLength).IsRequired(false);
                b.Property(x => x.PainRhythm).HasColumnName(nameof(PainDetail.PainRhythm)).IsRequired();
                b.Property(x => x.OtherPain).HasColumnName(nameof(PainDetail.OtherPain)).HasMaxLength(PainDetailConsts.OtherPainMaxLength).IsRequired(false);
                b.Property(x => x.StartDate).HasColumnName(nameof(PainDetail.StartDate)).IsRequired();
                b.HasOne(x => x.Protocol).WithOne().HasForeignKey<PainDetail>(x => x.ProtocolId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                b.HasOne(x => x.PainType).WithMany().HasForeignKey(x => x.PainTypeId).IsRequired().OnDelete(DeleteBehavior.NoAction);


            });

            builder.Entity<DoctorWorkSchedule>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "DoctorWorkSchedules", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.WorkingDays).HasColumnName(nameof(DoctorWorkSchedule.WorkingDays)).IsRequired();
                b.Property(x => x.StartHour).HasColumnName(nameof(DoctorWorkSchedule.StartHour)).IsRequired();
                b.Property(x => x.EndHour).HasColumnName(nameof(DoctorWorkSchedule.EndHour)).IsRequired();
                b.HasOne(x=>x.Doctor).WithOne().HasForeignKey<DoctorWorkSchedule>(x => x.DoctorId).OnDelete(DeleteBehavior.NoAction);
            }
             );

            builder.Entity<BlackList>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "BlackLists", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.BlackListStatus).HasColumnName(nameof(BlackList.BlackListStatus)).IsRequired();
                b.Property(x => x.Note).HasColumnName(nameof(BlackList.Note));
                b.HasOne(x=>x.Patient).WithMany().IsRequired(false).HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne(x=>x.Doctor).WithMany().IsRequired(false).HasForeignKey(x => x.DoctorId).OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<SystemCheck>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "SystemChecks", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.ProtocolId).HasColumnName(nameof(SystemCheck.ProtocolId)).IsRequired();
                b.Property(x => x.GeneralSystemCheck).HasColumnName(nameof(SystemCheck.GeneralSystemCheck)).IsRequired();
                b.Property(x => x.GenitoUrinary).HasColumnName(nameof(SystemCheck.GenitoUrinary)).IsRequired(false);
                b.Property(x => x.Skin).HasColumnName(nameof(SystemCheck.Skin)).IsRequired(false);
                b.Property(x => x.Respiratory).HasColumnName(nameof(SystemCheck.Respiratory)).IsRequired(false);
                b.Property(x => x.Nervous).HasColumnName(nameof(SystemCheck.Nervous)).IsRequired(false);
                b.Property(x => x.MusculoSkeletal).HasColumnName(nameof(SystemCheck.MusculoSkeletal)).IsRequired(false);
                b.Property(x => x.Circulatory).HasColumnName(nameof(SystemCheck.Circulatory)).IsRequired(false);
                b.Property(x => x.GastroIntestinal).HasColumnName(nameof(SystemCheck.GastroIntestinal)).IsRequired(false);
                b.Property(x => x.Description).HasColumnName(nameof(SystemCheck.Description)).IsRequired(false);
                b.HasOne(x => x.Protocol).WithOne().HasForeignKey<SystemCheck>(x => x.ProtocolId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            });

            builder.Entity<FollowUpPlan>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "FollowUpPlans", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.ProtocolId).HasColumnName(nameof(FollowUpPlan.ProtocolId)).IsRequired();
                b.Property(x => x.Note).HasColumnName(nameof(FollowUpPlan.Note)).HasMaxLength(FollowUpPlanConsts.NoteMaxLength).IsRequired(false);
                b.Property(x => x.FollowUpType).HasColumnName(nameof(FollowUpPlan.FollowUpType)).IsRequired();
                b.Property(x => x.IsSurgeryScheduled).HasColumnName(nameof(FollowUpPlan.IsSurgeryScheduled)).IsRequired();
                b.HasOne(x => x.Protocol).WithOne().HasForeignKey<FollowUpPlan>(x => x.ProtocolId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            });

            builder.Entity<FamilyHistory>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "FamilyHistories", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.PatientId).HasColumnName(nameof(FamilyHistory.PatientId)).IsRequired();
                b.Property(x => x.Mother).HasColumnName(nameof(FamilyHistory.Mother)).IsRequired(false);
                b.Property(x => x.Father).HasColumnName(nameof(FamilyHistory.Father)).IsRequired(false);
                b.Property(x => x.Sister).HasColumnName(nameof(FamilyHistory.Sister)).IsRequired(false);
                b.Property(x => x.Brother).HasColumnName(nameof(FamilyHistory.Brother)).IsRequired(false);
                b.Property(x => x.Other).HasColumnName(nameof(FamilyHistory.Other)).IsRequired(false);
                b.Property(x => x.IsParentsRelative).HasColumnName(nameof(FamilyHistory.IsParentsRelative)).IsRequired();
                b.HasOne(x => x.Patient).WithOne().HasForeignKey<FamilyHistory>(x => x.PatientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ControlNote>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "ControlNotes", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.ProtocolId).HasColumnName(nameof(ControlNote.ProtocolId)).IsRequired();
                b.Property(x => x.NoteDate).HasColumnName(nameof(ControlNote.NoteDate)).IsRequired();
                b.Property(x => x.Note).HasColumnName(nameof(ControlNote.Note)).HasMaxLength(ControlNoteConsts.NoteMaxLength).IsRequired();
                b.HasOne(x => x.Protocol).WithMany().IsRequired().HasForeignKey(x => x.ProtocolId).OnDelete(DeleteBehavior.Cascade);
                b.HasOne(x => x.User).WithMany().IsRequired().HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<PatientHistory>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "PatientHistories", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.PatientId).HasColumnName(nameof(PatientHistory.PatientId)).IsRequired();
                b.Property(x => x.Habit).HasColumnName(nameof(PatientHistory.Habit)).IsRequired(false);
                b.Property(x => x.Disease).HasColumnName(nameof(PatientHistory.Disease)).IsRequired();
                b.Property(x => x.Medicine).HasColumnName(nameof(PatientHistory.Medicine)).IsRequired(false);
                b.Property(x => x.Operation).HasColumnName(nameof(PatientHistory.Operation)).IsRequired();
                b.Property(x => x.Vaccination).HasColumnName(nameof(PatientHistory.Vaccination)).IsRequired(false);
                b.Property(x => x.Allergy).HasColumnName(nameof(PatientHistory.Allergy)).IsRequired();
                b.Property(x => x.SpecialCondition).HasColumnName(nameof(PatientHistory.SpecialCondition)).IsRequired(false);
                b.Property(x => x.Device).HasColumnName(nameof(PatientHistory.Device)).IsRequired(false);
                b.Property(x => x.Therapy).HasColumnName(nameof(PatientHistory.Therapy)).IsRequired(false);
                b.Property(x => x.Job).HasColumnName(nameof(PatientHistory.Job)).IsRequired(false);
                b.Property(x => x.EducationLevel).HasColumnName(nameof(PatientHistory.EducationLevel)).IsRequired();
                b.Property(x => x.MaritalStatus).HasColumnName(nameof(PatientHistory.MaritalStatus)).IsRequired();


                b.HasOne(x => x.Patient).WithOne().HasForeignKey<PatientHistory>(x => x.PatientId).IsRequired().OnDelete(DeleteBehavior.Cascade);

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
