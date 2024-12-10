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
        [Display(Name = "DiagnosisType:Definitive")]
        Definitive = 1,//kesin tani

        [Display(Name = "DiagnosisType:Preliminary")]
        Preliminary = 2,//ön tanı

        [Display(Name = "DiagnosisType:Differential")]
        Differential = 3 //ayirici
       
    }
}
