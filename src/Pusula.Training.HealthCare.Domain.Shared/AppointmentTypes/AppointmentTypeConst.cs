

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeConst
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "AppointmentType." : string.Empty);
        }

        public const int NameMinLength = 2;
        public const int NameMaxLength = 100;
        
    }
}
