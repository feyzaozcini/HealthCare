namespace Pusula.Training.HealthCare.Blazor.Menus;

public class HealthCareMenus
{
    private const string Prefix = "HealthCare";
    public const string Home = Prefix + ".Home";

    //Add your menu items here...
    public const string Patients = Prefix + ".Patients";
    public const string Protocols = Prefix + ".Protocols";
    public const string Departments = Prefix + ".Departments";
    public const string TestGroups = Prefix + ".TestGroups";
    public const string TestRequests = Prefix + ".TestRequests";
   
    public const string Treatment = Prefix + ".Treatment";
    public const string Definitions = Treatment + ".Definitions";
    public const string Operations = Treatment + ".Operations";
    public const string Reports = Treatment + ".Reports";

    // Alt sayfalar
    public const string IcdList = Definitions + ".IcdList";
    public const string DoctorTaskList = Operations + ".DoctorTaskList";
    public const string DiagnosisReport = Reports + ".DiagnosisReport";
}
