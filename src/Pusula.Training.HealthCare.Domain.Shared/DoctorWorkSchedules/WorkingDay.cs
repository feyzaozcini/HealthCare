using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public enum WorkingDay
    {
        [Display(Name = "Pazartesi")]
        Monday = 1,

        [Display(Name = "Salı")]
        Tuesday = 2,

        [Display(Name = "Çarşamba")]
        Wednesday = 3,

        [Display(Name = "Perşembe")]
        Thursday = 4,

        [Display(Name = "Cuma")]
        Friday = 5,

        [Display(Name = "Cumartesi")]
        Saturday = 6,

        [Display(Name = "Pazar")]
        Sunday = 7
    }
}
