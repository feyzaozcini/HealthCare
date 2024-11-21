using Microsoft.EntityFrameworkCore;
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
    public DbSet<AppointmentType> AppointmentTypes { get; set; } = null!;
    public DbSet<District> Districts { get; set; } = null!;
    public DbSet<Village> Villages { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;




    public DbSet<DiagnosisGroup> DiagnosisGroups { get; set; } = null!;
    public DbSet<Diagnosis> Diagnoses { get; set; } = null!;
    public DbSet<Anamnesis> Anamneses { get; set; } = null!;
    public DbSet<ExaminationDiagnosis> ExaminationDiagnoses { get; set; } = null!;
    public DbSet<PhysicalExamination> PhysicalExaminations { get; set; } = null!;
    public DbSet<PshychologicalState> PshychologicalStates { get; set; } = null!;
    public DbSet<FallRisk> FallRisks { get; set; } = null!;



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

            builder.Entity<Doctor>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Doctors", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.BirthDate).HasColumnName(nameof(Doctor.BirthDate)).IsRequired();
                b.Property(x => x.Gender).HasColumnName(nameof(Doctor.Gender)).IsRequired();
                b.Property(x => x.IdentityNumber).HasColumnName(nameof(Doctor.IdentityNumber)).IsRequired();
                b.Property(x => x.UserId).HasColumnName(nameof(Doctor.UserId)).IsRequired();
                b.Property(x => x.TitleId).HasColumnName(nameof(Doctor.TitleId)).IsRequired();
                b.HasOne<IdentityUser>().WithOne().HasForeignKey<Doctor>(x => x.UserId).IsRequired();
                b.HasOne<Title>().WithMany().HasForeignKey(x=>x.TitleId).OnDelete(DeleteBehavior.NoAction);

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
                    .HasMaxLength(TestGroupItemConsts.DescriptionMaxLength);

                b.Property(x => x.TurnaroundTime)
                    .HasColumnName(nameof(TestGroupItem.TurnaroundTime))
                    .IsRequired();

                b.HasOne<TestGroup>()
                    .WithMany()
                    .HasForeignKey(x => x.TestGroupId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<LabRequest>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "LabRequests", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .HasColumnName(nameof(LabRequest.Name))
                    .IsRequired()
                    .HasMaxLength(LabRequestConsts.NameMaxLength);

                b.Property(x => x.Date)
                    .HasColumnName(nameof(LabRequest.Date))
                    .IsRequired();

                b.Property(x => x.Status)
                    .HasColumnName(nameof(LabRequest.Status))
                    .IsRequired();

                b.HasOne<TestGroupItem>()
                    .WithMany()
                    .HasForeignKey(x => x.TestGroupItemId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                b.Property(x => x.ProtocolId)
                    .HasColumnName(nameof(LabRequest.ProtocolId))
                    .IsRequired();

                b.Property(x => x.DoctorId)
                    .HasColumnName(nameof(LabRequest.DoctorId))
                    .IsRequired();
            });

            builder.Entity<AppointmentType>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "AppointmentTypes", HealthCareConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .HasColumnName(nameof(AppointmentType.Name))
                    .IsRequired()
                    .HasMaxLength(AppointmentTypeConst.NameMaxLength);
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
                    .WithMany(d => d.DoctorDepartments)
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
                b.ConfigureByConvention(); // ABP'nin varsayılan kuralları
                b.Property(x => x.Name).HasColumnName(nameof(Diagnosis.Name)).IsRequired().HasMaxLength(DiagnosisConsts.NameMaxLength);
                b.Property(x => x.Code).HasColumnName(nameof(Diagnosis.Code)).IsRequired().HasMaxLength(DiagnosisConsts.CodeMaxLength);
                b.HasOne<DiagnosisGroup>().WithMany().HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Anamnesis>(b =>
            {
                b.ToTable(HealthCareConsts.DbTablePrefix + "Anamneses", HealthCareConsts.DbSchema);
                b.ConfigureByConvention(); // ABP'nin varsayılan kuralları
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
                b.HasOne<Diagnosis>().WithMany().HasForeignKey(x => x.DiagnosisId).OnDelete(DeleteBehavior.NoAction);

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
                b.Property(x => x.Description).HasColumnName(nameof(PshychologicalState.Description)).HasMaxLength(PshychologicalStateConsts.DescriptionMaxLength);
                b.Property(x => x.State).HasColumnName(nameof(PshychologicalState.State));
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
                b.Property(x => x.Note).HasColumnName(nameof(Appointment.Note)).HasMaxLength(AppointmentConst.NoteMaxLength);
                b.HasOne<Doctor>().WithMany().HasForeignKey(x => x.DoctorId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Patient>().WithMany().HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<AppointmentType>().WithMany().HasForeignKey(x => x.AppointmentTypeId).OnDelete(DeleteBehavior.NoAction);
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



        }

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(HealthCareConsts.DbTablePrefix + "YourEntities", HealthCareConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}
