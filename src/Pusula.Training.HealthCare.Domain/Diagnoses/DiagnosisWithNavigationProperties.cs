using Pusula.Training.HealthCare.DiagnosisGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public class DiagnosisWithNavigationProperties
    {
        public Diagnosis Diagnosis { get; set; } = null!;

        public DiagnosisGroup DiagnosisGroup { get; set; } = null!;

    }
}
