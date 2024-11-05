using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.PatientCompanies;
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

        public CountryDto Country { get; set; } = null!;

        public PatientCompanyDto PatientCompany { get; set; } = null!;
    }
}
