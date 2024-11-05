using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.PatientCompanies;
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

        public Country Country { get; set; } = null!;

        public PatientCompany PatientCompany { get; set; } = null!;

    }
}
