using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.BlackLists
{
    public class BlackListWithNavigationProperties
    {
        public BlackList BlackList { get; set; } = null!;

        public Doctor Doctor { get; set; } = null!;

        public Patient Patient { get; set; } = null!;
    }
}
