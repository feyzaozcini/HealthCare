using Pusula.Training.HealthCare.Core.Rules.Diagnoses;
using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.ExaminationDiagnoses
{
    public class ExaminationDiagnosisBusinessRules(IExaminationDiagnosisRepository examinationDiagnosisRepository) : IExaminationDiagnosisBusinessRules
    {
        public async Task ExaminationDiagnosisDuplicatedAsync(Guid protocolId, Guid diagnosisId)
        {
            HealthCareException.ThrowIf(
           HealthCareDomainErrorCodes.ExaminationDiagnosisAlreadyExists,
           await examinationDiagnosisRepository.AnyAsync(x => x.ProtocolId == protocolId && x.DiagnosisId == diagnosisId)
             );
        }
    }
}
