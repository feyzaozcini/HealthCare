using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Patients
{
    public class PatientWithNavigationProperties
    {
        public Patient Patient { get; set; } = null!;

        //public Country Country { get; set; } = null!;
        //public Company Company { get; set; } = null!;

    }
}
