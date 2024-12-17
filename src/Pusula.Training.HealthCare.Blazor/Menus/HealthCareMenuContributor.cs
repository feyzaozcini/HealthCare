using System.Threading.Tasks;
using Pusula.Training.HealthCare.Localization;
using Pusula.Training.HealthCare.MultiTenancy;
using Pusula.Training.HealthCare.Permissions;
using System.Threading.Tasks;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace Pusula.Training.HealthCare.Blazor.Menus;

public class HealthCareMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }
    

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<HealthCareResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                HealthCareMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 0
            )
        );

        ConfigureTenantMenu(administration, MultiTenancyConsts.IsEnabled);

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        #region Patient Tabs

        var patientMenu = new ApplicationMenuItem(
            HealthCareMenus.Patient,
            l["Menu:Patient"],
            icon: "fa fa-user-injured"
        );

        patientMenu.AddItem(
        new ApplicationMenuItem(
            HealthCareMenus.PatientDefinitions,
            l["Menu:Definitions"],
            icon: "fa fa-cogs"
            ).AddItem(
                new ApplicationMenuItem(
                    HealthCareMenus.PatientDefinitionsInsurances,
                    l["Menu:Insurances"],
                    url: "/insurances",
                    icon: "fa fa-shield-alt"
                )
            ).AddItem(
                new ApplicationMenuItem(
                    HealthCareMenus.PatientDefinitionsProtocolTypes,
                    l["Menu:ProtocolTypes"],
                    url: "/protocol-types",
                    icon: "fa fa-id-card"
                )
        ));

        patientMenu.AddItem(
        new ApplicationMenuItem(
            HealthCareMenus.PatientOperations,
            l["Menu:Operations"],
            icon: "fa fa-tools"
            ).AddItem(
                new ApplicationMenuItem(
                    HealthCareMenus.PatientOperationsPrm,
                    l["Menu:PRM"],
                    url: "/patients",
                    icon: "fa fa-users"
                )
            ));

        patientMenu.AddItem(
        new ApplicationMenuItem(
                HealthCareMenus.PatientReports,
                l["Menu:Reports"],
                icon: "fa fa-chart-bar"
        ).AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.PatientReportsProtocolList,
                l["Menu:ProtocolLists"],
                url: "/patient-protocol-lists",
                icon: "fa fa-file-alt"
                )
        )
    );

        #endregion

        #region Lab Tabs

        var labMenu = new ApplicationMenuItem(
            HealthCareMenus.Laboratory,
            l["Menu:Laboratory"],
            icon: "fa fa-vials"
        );

        labMenu.AddItem(
        new ApplicationMenuItem(
            HealthCareMenus.LaboratoryDefinitions,
            l["Menu:Definitions"],
            icon: "fa fa-cogs"
            ).AddItem(
            new ApplicationMenuItem(
            HealthCareMenus.LaboratoryDefinitionsTests,
            l["Menu:Tests"],
            url: "/test-groups",
            icon: "fa fa-flask"
            )
        ));

        labMenu.AddItem(
        new ApplicationMenuItem(
            HealthCareMenus.LaboratoryOperations,
            l["Menu:Operations"],
            icon: "fa fa-tools"
            ).AddItem(
                new ApplicationMenuItem(
                    HealthCareMenus.LaboratoryOperationsProtocolList,
                    l["Menu:ProtocolList"],
                    url: "/lab-protocols",
                    icon: "fa fa-list"
                )
            ).AddItem(
                new ApplicationMenuItem(
                    HealthCareMenus.LaboratoryOperationsTestRequests,
                    l["Menu:TestRequests"],
                    url: "/lab-request",
                    icon: "fa fa-receipt"
                )
            ).AddItem(
                new ApplicationMenuItem(
                    HealthCareMenus.LaboratoryOperationsLaborerBacklogs,
                    l["Menu:LaborerBacklogs"],
                    url: "/laborer-backlog",
                    icon: "fa fa-vials"
                )
            ));

        labMenu.AddItem(
        new ApplicationMenuItem(
                HealthCareMenus.LaboratoryReports,
                l["Menu:Reports"],
                icon: "fa fa-chart-bar"
        ).AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.LaboratoryReportsTestStatistics,
                l["Menu:TestStatistics"],
                url: "/test-statistics-report",
                icon: "fa fa-chart-pie"
                )
        ).AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.LaboratoryReportsTestResults,
                l["Menu:TestResults"],
                url: "/test-results-report",
                icon: "fa fa-file-alt"
               )
        )
    );

        #endregion

        #region Examination Tabs
        // Tedavi başlığı ve alt menüleri
        var treatmentMenu = new ApplicationMenuItem(
            HealthCareMenus.Treatment,
            l["Menu:Treatment"],
            icon: "fa fa-heartbeat"
        );

        treatmentMenu.AddItem(new ApplicationMenuItem(
            HealthCareMenus.Definitions,
            l["Menu:Definitions"],
            icon: "fa fa-cogs"
        ).AddItem(new ApplicationMenuItem(
            HealthCareMenus.IcdList,
            l["Menu:IcdList"],
            "/icd-list",
            icon: "fa fa-list"
        )));

        treatmentMenu.AddItem(new ApplicationMenuItem(
            HealthCareMenus.Operations,
            l["Menu:Operations"],
            icon: "fa fa-tasks"
        ).AddItem(new ApplicationMenuItem(
            HealthCareMenus.DoctorTaskList,
            l["Menu:DoctorTaskList"],
            "/doctors",//"/doctor-task-list"
            icon: "fa fa-user-md"
        )));

        treatmentMenu.AddItem(new ApplicationMenuItem(
            HealthCareMenus.Reports,
            l["Menu:Reports"],
            icon: "fa fa-file-alt"
        ).AddItem(new ApplicationMenuItem(
            HealthCareMenus.DiagnosisReport,
            l["Menu:DiagnosisReport"],
            "/diagnosis-report",
            icon: "fa fa-file"
        )));
        #endregion

        #region Appointments Tabs
        // Randevu başlığı ve alt menüleri
        var appoinmentMenu = new ApplicationMenuItem(
            HealthCareMenus.Appointment,
            l["Menu:Appointment"],
            icon: "fa fa-calendar-check"
        );

        appoinmentMenu.AddItem(new ApplicationMenuItem(
            HealthCareMenus.AppointmentDefinitions,
            l["Menu:Definitions"],
            icon: "fa fa-sliders-h"
        ).AddItem(new ApplicationMenuItem(
            HealthCareMenus.DoctorWorkSchedule,
            l["Menu:DoctorWorkSchedule"],
            "/doctorWorkSchedule",
            icon: "fa fa-clock-o"
        )).AddItem(new ApplicationMenuItem(
            HealthCareMenus.AppointmentType,
            l["Menu:AppointmentType"],
            "/appointment-types",
            icon: "fa fa-list-ul"
        ))
        .AddItem(new ApplicationMenuItem(
            HealthCareMenus.AppointmentRule,
            l["Menu:AppointmentRule"],
            "/appointmentRule",
            icon: "fa fa-tasks"
        )).AddItem(new ApplicationMenuItem(
            HealthCareMenus.Blacklist,
            l["Menu:Blacklist"],
            "/blacklist",
            icon: "fa fa-ban"
        )));

        appoinmentMenu.AddItem(new ApplicationMenuItem(
            HealthCareMenus.AppointmentOperations,
            l["Menu:Operations"],
            icon: "fa fa-clipboard-list"
        ).AddItem(new ApplicationMenuItem(
            HealthCareMenus.AppoinmentSchedule,
            l["Menu:AppointmentSchedule"],
            url: "/appointment",
            icon: "fa fa-calendar-alt"
        )));

        appoinmentMenu.AddItem(
        new ApplicationMenuItem(
                HealthCareMenus.AppointmentReports,
                l["Menu:Reports"],
                icon: "fa fa-chart-line"
        ).AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Appointments,
                l["Menu:AppointmentList"],
                url: "/appointmentslist",
                icon: "fa fa-list-alt"
                )
        ).AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.AppointmentReport,
                l["Menu:AppointmentReport"],
                url: "/appointmentReport",
                icon: "fa fa-file-alt"
               )));

        #endregion

        context.Menu.AddItem(patientMenu);
        context.Menu.AddItem(treatmentMenu);
        context.Menu.AddItem(appoinmentMenu);
        context.Menu.AddItem(labMenu);

        return Task.CompletedTask;
    }

    private static void ConfigureTenantMenu(ApplicationMenuItem? item, bool isMultiTenancyEnabled)
    {
        if (isMultiTenancyEnabled)
        {
            item?.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            item?.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }
    }
}
