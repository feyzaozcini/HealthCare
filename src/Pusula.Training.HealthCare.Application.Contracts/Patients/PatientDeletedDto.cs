using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Patients
{
    public class PatientDeletedDto
    {
        public Guid Id { get; set; }
        public String? DeleteMessage { get; set; }
    }
}
