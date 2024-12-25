using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.ExaminationDiagnoses
{
    public interface IExaminationDiagnosisBusinessRules : IBusinessRules
    {
        Task ExaminationDiagnosisDuplicatedAsync(Guid protocolId, Guid diagnosisId);
    }
}
