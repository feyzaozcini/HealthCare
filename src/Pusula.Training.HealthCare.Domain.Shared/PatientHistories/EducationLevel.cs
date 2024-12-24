using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PatientHistories
{
    public enum EducationLevel
    {
        [Display(Name = "Belirtilmemiş")]
        Unspecified = 1, // Belirtilmemiş

        [Display(Name = "İlköğretim")]
        PrimarySchool = 2, // İlköğretim

        [Display(Name = "Lise")]
        HighSchool = 3, // Lise

        [Display(Name = "Ön Lisans")]
        AssociateDegree = 4, // Ön Lisans

        [Display(Name = "Lisans")]
        BachelorDegree = 5, // Lisans

        [Display(Name = "Lisansüstü")]
        Postgraduate = 6, // Lisansüstü
    }
}
