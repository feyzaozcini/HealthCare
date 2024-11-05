using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Patients
{
    public class PatientWithNavigationPropertiesDto
    {
        public PatientDto Patient { get; set; } = null!;
        //public TitleDto Title { get; set; } = null!;
        //public DepartmentDto Department { get; set; } = null!;
    }
}
