using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;

namespace Pusula.Training.HealthCare.BlackLists
{
    public class BlackListWithNavigationProperties
    {
        public BlackList BlackList { get; set; } = null!;

        public Doctor Doctor { get; set; } = null!;

        public Patient Patient { get; set; } = null!;
    }
}
