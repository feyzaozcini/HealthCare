using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.Doctors
{
    public class DoctorWithNavigationPropertiesDto
    {
        public DoctorDto Doctor { get; set; } = null!;

        public TitleDto Title { get; set; } = null!;

        public IdentityUserDto User { get; set; } = null!;

        //public List<Guid> DoctorDepartments { get; set; }

    }
}
