using Pusula.Training.HealthCare.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.Doctors
{
    public class DoctorWithNavigationProperties
    {
        public Doctor Doctor { get; set; } = null!;

        public IdentityUser User { get; set; } = null!;

        public Title Title { get; set; } = null!;





    }
}
