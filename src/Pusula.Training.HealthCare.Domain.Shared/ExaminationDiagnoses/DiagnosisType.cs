using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public enum DiagnosisType
    {

        [Display(Name = "Kesin Tani")]
        Definitive = 1,//kesin tani

        [Display(Name = "Ön Tanı")]
        Preliminary = 2,//ön tanı


        [Display(Name = "Ayirici Tani")]
        Differential = 3 //ayirici
       
    }
}
