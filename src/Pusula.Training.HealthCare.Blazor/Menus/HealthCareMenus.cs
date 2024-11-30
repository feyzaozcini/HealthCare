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
    public const string LaboratoryReports = Laboratory + ".Reports";
    public const string LaboratoryReportsTestStatistics = LaboratoryReports + ".TestStatistics";
    public const string LaboratoryReportsTestResults = LaboratoryReports + ".TestResults";
}
