using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Doctors
{
    public class DoctorDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string SurName { get; set; } = null!;

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        public Guid UserId { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public List<string> DoctorDepartments { get; set; } = new();
    }
}
