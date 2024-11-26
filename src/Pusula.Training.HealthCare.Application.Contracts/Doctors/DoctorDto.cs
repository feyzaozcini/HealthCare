using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain.Entities;
namespace Pusula.Training.HealthCare.Doctors
{
    public class DoctorDto : Entity<Guid>
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

        public Guid TitleId { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public List<Guid> DoctorDepartments { get; set; } 
    }
}
