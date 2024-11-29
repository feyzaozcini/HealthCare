using Pusula.Training.HealthCare.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Pusula.Training.HealthCare.Permissions;

public class HealthCarePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HealthCarePermissions.GroupName);

        myGroup.AddPermission(HealthCarePermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(HealthCarePermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(HealthCarePermissions.MyPermission1, L("Permission:MyPermission1"));

        var patientPermission = myGroup.AddPermission(HealthCarePermissions.Patients.Default, L("Permission:Patients"));
        patientPermission.AddChild(HealthCarePermissions.Patients.Create, L("Permission:Create"));
        patientPermission.AddChild(HealthCarePermissions.Patients.Edit, L("Permission:Edit"));
        patientPermission.AddChild(HealthCarePermissions.Patients.Delete, L("Permission:Delete"));

        var protocolPermission = myGroup.AddPermission(HealthCarePermissions.Protocols.Default, L("Permission:Protocols"));
        protocolPermission.AddChild(HealthCarePermissions.Protocols.Create, L("Permission:Create"));
        protocolPermission.AddChild(HealthCarePermissions.Protocols.Edit, L("Permission:Edit"));
        protocolPermission.AddChild(HealthCarePermissions.Protocols.Delete, L("Permission:Delete"));

        var departmentPermission = myGroup.AddPermission(HealthCarePermissions.Departments.Default, L("Permission:Departments"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Create, L("Permission:Create"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Edit, L("Permission:Edit"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Delete, L("Permission:Delete"));

        var patientCompanyPermission = myGroup.AddPermission(HealthCarePermissions.PatientCompanies.Default, L("Permission:PatientCompanies"));
        patientCompanyPermission.AddChild(HealthCarePermissions.PatientCompanies.Create, L("Permission:Create"));
        patientCompanyPermission.AddChild(HealthCarePermissions.PatientCompanies.Edit, L("Permission:Edit"));
        patientCompanyPermission.AddChild(HealthCarePermissions.PatientCompanies.Delete, L("Permission:Delete"));

        var titlePermission = myGroup.AddPermission(HealthCarePermissions.Titles.Default, L("Permission:Titles"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Create, L("Permission:Create"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Edit, L("Permission:Edit"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Delete, L("Permission:Delete"));

        var departmentServicePermission = myGroup.AddPermission(HealthCarePermissions.DepartmentServices.Default, L("Permission:DepartmentServices"));
        departmentServicePermission.AddChild(HealthCarePermissions.DepartmentServices.Create, L("Permission:Create"));
        departmentServicePermission.AddChild(HealthCarePermissions.DepartmentServices.Edit, L("Permission:Edit"));
        departmentServicePermission.AddChild(HealthCarePermissions.DepartmentServices.Delete, L("Permission:Delete"));

        var countryPermission = myGroup.AddPermission(HealthCarePermissions.Countries.Default, L("Permission:Countries"));
        countryPermission.AddChild(HealthCarePermissions.Countries.Create, L("Permission:Create"));
        countryPermission.AddChild(HealthCarePermissions.Countries.Edit, L("Permission:Edit"));
        countryPermission.AddChild(HealthCarePermissions.Countries.Delete, L("Permission:Delete"));

        var cityPermission = myGroup.AddPermission(HealthCarePermissions.Cities.Default, L("Permission:Cities"));
        cityPermission.AddChild(HealthCarePermissions.Cities.Create, L("Permission:Create"));
        cityPermission.AddChild(HealthCarePermissions.Cities.Edit, L("Permission:Edit"));
        cityPermission.AddChild(HealthCarePermissions.Cities.Delete, L("Permission:Delete"));

        var labRequestPermission = myGroup.AddPermission(HealthCarePermissions.LabRequests.Default, L("Permission:LabRequest"));
        labRequestPermission.AddChild(HealthCarePermissions.LabRequests.Create, L("Permission:Create"));
        labRequestPermission.AddChild(HealthCarePermissions.LabRequests.Edit, L("Permission:Edit"));
        labRequestPermission.AddChild(HealthCarePermissions.LabRequests.Delete, L("Permission:Delete"));

        var testGroupPermission = myGroup.AddPermission(HealthCarePermissions.TestGroups.Default, L("Permission:TestGroups"));
        testGroupPermission.AddChild(HealthCarePermissions.TestGroups.Create, L("Permission:Create"));
        testGroupPermission.AddChild(HealthCarePermissions.TestGroups.Edit, L("Permission:Edit"));
        testGroupPermission.AddChild(HealthCarePermissions.TestGroups.Delete, L("Permission:Delete"));

        var testGroupItemPermission = myGroup.AddPermission(HealthCarePermissions.TestGroupItems.Default, L("Permission:TestGroupItems"));
        testGroupItemPermission.AddChild(HealthCarePermissions.TestGroupItems.Create, L("Permission:Create"));
        testGroupItemPermission.AddChild(HealthCarePermissions.TestGroupItems.Edit, L("Permission:Edit"));
        testGroupItemPermission.AddChild(HealthCarePermissions.TestGroupItems.Delete, L("Permission:Delete"));

        var doctorPermission = myGroup.AddPermission(HealthCarePermissions.Doctors.Default, L("Permission:Doctors"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Create, L("Permission:Create"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Edit, L("Permission:Edit"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Delete, L("Permission:Delete"));

        var appointmentTypePermission = myGroup.AddPermission(HealthCarePermissions.AppointmentTypes.Default, L("Permission:AppointmentType"));
        appointmentTypePermission.AddChild(HealthCarePermissions.AppointmentTypes.Create, L("Permission:Create"));
        appointmentTypePermission.AddChild(HealthCarePermissions.AppointmentTypes.Edit, L("Permission:Edit"));
        appointmentTypePermission.AddChild(HealthCarePermissions.AppointmentTypes.Delete, L("Permission:Delete"));

        var districtPermission = myGroup.AddPermission(HealthCarePermissions.Districts.Default, L("Permission:Districts"));
        districtPermission.AddChild(HealthCarePermissions.Districts.Create, L("Permission:Create"));
        districtPermission.AddChild(HealthCarePermissions.Districts.Edit, L("Permission:Edit"));
        districtPermission.AddChild(HealthCarePermissions.Districts.Delete, L("Permission:Delete"));

        var villagePermission = myGroup.AddPermission(HealthCarePermissions.Villages.Default, L("Permission:Villages"));
        villagePermission.AddChild(HealthCarePermissions.Villages.Create, L("Permission:Create"));
        villagePermission.AddChild(HealthCarePermissions.Villages.Edit, L("Permission:Edit"));
        villagePermission.AddChild(HealthCarePermissions.Villages.Delete, L("Permission:Delete"));

        var appointmentPermission = myGroup.AddPermission(HealthCarePermissions.Appointments.Default, L("Permission:Appointments"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Create, L("Permission:Create"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Edit, L("Permission:Edit"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Delete, L("Permission:Delete"));


        var diagnosisGroupPermission = myGroup.AddPermission(HealthCarePermissions.DiagnosisGroups.Default, L("Permission:DiagnosisGroups"));
        diagnosisGroupPermission.AddChild(HealthCarePermissions.DiagnosisGroups.Create, L("Permission:Create"));
        diagnosisGroupPermission.AddChild(HealthCarePermissions.DiagnosisGroups.Edit, L("Permission:Edit"));
        diagnosisGroupPermission.AddChild(HealthCarePermissions.DiagnosisGroups.Delete, L("Permission:Delete"));

        var diagnosisPermission = myGroup.AddPermission(HealthCarePermissions.Diagnosis.Default, L("Permission:Diagnosis"));
        diagnosisPermission.AddChild(HealthCarePermissions.Diagnosis.Create, L("Permission:Create"));
        diagnosisPermission.AddChild(HealthCarePermissions.Diagnosis.Edit, L("Permission:Edit"));
        diagnosisPermission.AddChild(HealthCarePermissions.Diagnosis.Delete, L("Permission:Delete"));

        var examinationsPermission = myGroup.AddPermission(HealthCarePermissions.Examinations.Default, L("Permission:Examinations"));
        examinationsPermission.AddChild(HealthCarePermissions.Examinations.Create, L("Permission:Create"));
        examinationsPermission.AddChild(HealthCarePermissions.Examinations.Edit, L("Permission:Edit"));
        examinationsPermission.AddChild(HealthCarePermissions.Examinations.Delete, L("Permission:Delete"));

        var testProcessesPermission = myGroup.AddPermission(HealthCarePermissions.TestProcesses.Default, L("Permission:TestProcesses"));
        testProcessesPermission.AddChild(HealthCarePermissions.TestProcesses.Create, L("Permission:Create"));
        testProcessesPermission.AddChild(HealthCarePermissions.TestProcesses.Edit, L("Permission:Edit"));
        testProcessesPermission.AddChild(HealthCarePermissions.TestProcesses.Delete, L("Permission:Delete"));

        var testValueRangesPermission = myGroup.AddPermission(HealthCarePermissions.TestValueRanges.Default, L("Permission:TestValueRanges"));
        testValueRangesPermission.AddChild(HealthCarePermissions.TestValueRanges.Create, L("Permission:Create"));
        testValueRangesPermission.AddChild(HealthCarePermissions.TestValueRanges.Edit, L("Permission:Edit"));
        testValueRangesPermission.AddChild(HealthCarePermissions.TestValueRanges.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HealthCareResource>(name);
    }
}