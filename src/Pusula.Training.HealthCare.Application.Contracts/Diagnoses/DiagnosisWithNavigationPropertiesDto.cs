using Pusula.Training.HealthCare.DiagnosisGroups;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public class DiagnosisWithNavigationPropertiesDto
    {
        public DiagnosisDto Diagnosis { get; set; } = null!;

        public DiagnosisGroupDto DiagnosisGroup { get; set; } = null!;

    }
}
