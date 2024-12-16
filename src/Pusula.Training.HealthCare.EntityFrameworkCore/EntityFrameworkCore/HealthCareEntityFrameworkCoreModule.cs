using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Titles;
using Pusula.Training.HealthCare.DepartmentServices;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Villages;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.DiagnosisGroups;
using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.FallRisks;
using Pusula.Training.HealthCare.PhysicalExaminations;
using Pusula.Training.HealthCare.PshychologicalStates;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Notes;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.AppointmentRules;
using Pusula.Training.HealthCare.PainTypes;
using Pusula.Training.HealthCare.PainDetails;
using Pusula.Training.HealthCare.DoctorWorkSchedules;
using Pusula.Training.HealthCare.BlackLists;

namespace Pusula.Training.HealthCare.EntityFrameworkCore;

[DependsOn(
    typeof(HealthCareDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
public class HealthCareEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        HealthCareEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<HealthCareDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);

            options.AddRepository<Patient, EfCorePatientRepository>();
            options.AddRepository<Protocol, EfCoreProtocolRepository>();
            options.AddRepository<Department, EfCoreDepartmentRepository>();
            options.AddRepository<PatientCompany, EfCorePatientCompanyRepository>();
            options.AddRepository<Title, EfCoreTitleRepository>();
            options.AddRepository<DepartmentService, EfCoreDepartmentServiceRepository>();
            options.AddRepository<Country, EfCoreCountryRepository>();
            options.AddRepository<City, EfCoreCityRepository>();
            options.AddRepository<AppointmentType, EfCoreAppointmentTypeRepository>();
            options.AddRepository<District, EfCoreDistrictRepository>();
            options.AddRepository<Village, EfCoreVillageRepository>();
            options.AddRepository<Appointment, EfCoreAppointmentTypeRepository>();
            options.AddRepository<ProtocolType, EfCoreProtocolTypeRepository>();
            options.AddRepository<Note, EfCoreNoteRepository>();
            options.AddRepository<Doctor, EfCoreDoctorRepository>();
            options.AddRepository<DiagnosisGroup, EfCoreDiagnosisGroupRepository>();
            options.AddRepository<Diagnosis, EfCoreDiagnosisRepository>();
            options.AddRepository<Anamnesis, EfCoreAnamnesisRepository>();
            options.AddRepository<Doctor, EfCoreDoctorRepository>();
            options.AddRepository<ExaminationDiagnosis, EfCoreExaminationDiagnosisRepository>();
            options.AddRepository<FallRisk, EfCoreFallRiskRepository>();
            options.AddRepository<PhysicalExamination, EfCorePhysicalExaminationRepository>();
            options.AddRepository<PshychologicalState, EfCorePshychologicalStateRepository>();
            options.AddRepository<Insurance, EfCoreInsuranceRepository>();
            options.AddRepository<AppointmentRule, EfCoreAppointmentRuleRepository>();
            options.AddRepository<PainType, EfCorePainTypeRepository>();
            options.AddRepository<PainDetail, EfCorePainDetailRepository>();
            options.AddRepository<DoctorWorkSchedule, EfCoreDoctorWorkScheduleRepository>();
            options.AddRepository<BlackList, EfCoreBlackListRepository>();



        });

        Configure<AbpDbContextOptions>(options =>
        {
                /* The main point to change your DBMS.
                 * See also HealthCareMigrationsDbContextFactory for EF Core tooling. */
            options.UseNpgsql();
        });

    }
}
