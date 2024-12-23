using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Addresses
{
    public class AddressWithNavigationPropertiesDto
    {
        public PatientDto Patient { get; set; } = null!;
        public AddressDto Address { get; set; } = null!;
    }
}
