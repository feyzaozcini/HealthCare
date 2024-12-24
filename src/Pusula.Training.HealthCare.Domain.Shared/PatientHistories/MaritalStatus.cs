using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PatientHistories
{
    public enum MaritalStatus
    {
        [Display(Name = "Belirtilmemiş")]
        Unspecified = 1, // Belirtilmemiş

        [Display(Name = "Evli")]
        Married = 2, // Evli

        [Display(Name = "Bekar")]
        Single = 3, // Bekar
    }
}
