
namespace Pusula.Training.HealthCare.AppointmentRules
{
    public class AppointmentRuleConst
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName) => string.Format(DefaultSorting, withEntityName ? "AppointmentRule." : string.Empty);
    }
}
