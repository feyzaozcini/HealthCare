using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.BlackLists
{
    public class BlackListWithNavigationPropertiesDto
    {
        public BlackListDto BlackList { get; set; } = null!;
        public PatientDto Patient { get; set; } = null!;
        public DoctorDto Doctor { get; set; } = null!;
    }
}
