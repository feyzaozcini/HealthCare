using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;

namespace Pusula.Training.HealthCare.BlackLists
{
    public class BlackListWithNavigationPropertiesDto
    {
        public BlackListDto BlackList { get; set; } = null!;
        public PatientDto Patient { get; set; } = null!;
        public DoctorDto Doctor { get; set; } = null!;
    }
}
