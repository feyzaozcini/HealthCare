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

        //context.Menu.AddItem(
        //    new ApplicationMenuItem(
        //        HealthCareMenus.Protocols,
        //        l["Menu:Protocols"],
        //        url: "/protocols",
        //        icon: "fa fa-file-alt",
        //        requiredPermissionName: HealthCarePermissions.Protocols.Default)
        //);

        //context.Menu.AddItem(
        //    new ApplicationMenuItem(
        //        HealthCareMenus.Departments,
        //        l["Menu:Departments"],
        //        url: "/departments",
        //        icon: "fa fa-file-alt",
        //        requiredPermissionName: HealthCarePermissions.Departments.Default)
        //);

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
        context.Menu.AddItem(treatmentMenu);
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
