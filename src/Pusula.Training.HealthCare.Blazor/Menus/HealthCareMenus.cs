namespace Pusula.Training.HealthCare.Blazor.Menus;

public class HealthCareMenus
{
    private const string Prefix = "HealthCare";
    public const string Home = Prefix + ".Home";

    //Add your menu items here...
    public const string Patients = Prefix + ".Patients";
    public const string Protocols = Prefix + ".Protocols";
    public const string Departments = Prefix + ".Departments";

    public const string Laboratory = Prefix + ".Laboratory";
    public const string LaboratoryDefinitions = Laboratory + ".Definitions";
    public const string LaboratoryDefinitionsTests = LaboratoryDefinitions + ".Tests";
    public const string LaboratoryOperations = Laboratory + ".Operations";
    public const string LaboratoryOperationsProtocolList = LaboratoryOperations + ".ProtocolList";
    public const string LaboratoryOperationsTestRequests = LaboratoryOperations + ".TestRequests";
    public const string LaboratoryOperationsLaborerBacklogs = LaboratoryOperations + ".LaborerBacklogs";
    public const string LaboratoryReports = Laboratory + ".Reports";
    public const string LaboratoryReportsTestStatistics = LaboratoryReports + ".TestStatistics";
    public const string LaboratoryReportsTestResults = LaboratoryReports + ".TestResults";

    public const string Treatment = Prefix + ".Treatment";
    public const string Definitions = Treatment + ".Definitions";
    public const string Operations = Treatment + ".Operations";
    public const string Reports = Treatment + ".Reports";

    // Alt sayfalar
    public const string IcdList = Definitions + ".IcdList";
    public const string DoctorTaskList = Operations + ".DoctorTaskList";
    public const string DiagnosisReport = Reports + ".DiagnosisReport";

    public const string Patient = Prefix + ".Patient";
    public const string PatientOperations = Patient + ".Operations";
    public const string PatientOperationsPrm = PatientOperations + ".PRM";
    public const string PatientReports = Patient + ".Reports";
    public const string PatientReportsProtocolList = PatientReports + ".ProtocolLists";
}
