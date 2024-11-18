using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Doctors
{
    public class DoctorExcelDto
    {
        //public string FirstName { get; set; } = null!;
        //public string LastName { get; set; } = null!;
        //public string PhoneNumber { get; set; } = null!;
        public string IdentityNumber { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Title { get; set; } = null!;
    }
}
