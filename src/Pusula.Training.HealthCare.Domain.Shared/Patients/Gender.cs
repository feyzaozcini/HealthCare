using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Patients
{
    public enum Gender
    {
        [Display(Name = "Belirtilmemiş")]
        Unspecified = 1, // Belirtilmemiş

        [Display(Name = "Erkek")]
        Male = 2,        // Erkek   

        [Display(Name = "Kadın")]
        Female = 3,      // Kadın

        [Display(Name = "Diğer")]
        Other = 4,       // Diğer
    }
}
