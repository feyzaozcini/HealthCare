
namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public class DoctorWorkScheduleConsts
    {
        private const string DefaultSorting = "{0}StartHour asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "DoctorWorkSchedule." : string.Empty);
        }
    }
}
