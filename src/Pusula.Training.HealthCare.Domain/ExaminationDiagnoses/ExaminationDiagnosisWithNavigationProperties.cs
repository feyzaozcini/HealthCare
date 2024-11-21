using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public class ExaminationDiagnosisWithNavigationProperties
    {
        public ExaminationDiagnosis ExaminationDiagnosis { get; set; } = null!;

        public Protocol Protocol { get; set; } = null!;

        public Diagnosis Diagnosis { get; set; } = null!;


    }
}
