using Pusula.Training.HealthCare.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Doctors
{
    public class DoctorGetListDto
    {
        public DoctorDto Doctor { get; set; } = null!;

        public TitleDto Title { get; set; } = null!;
    }
}
