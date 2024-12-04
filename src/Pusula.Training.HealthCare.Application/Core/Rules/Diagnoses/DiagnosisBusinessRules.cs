using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.Exceptions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.Diagnoses
{
    public class DiagnosisBusinessRules(IDiagnosisRepository diagnosisRepository) : IDiagnosisBusinessRules
    {
        public async Task DiagnosisCodeDuplicatedAsync(string diagnosisCode)
        {
            HealthCareException.ThrowIf(
             HealthCareDomainErrorCodes.DiagnosisAlreadyExist,
             await diagnosisRepository.AnyAsync(e => e.Code == diagnosisCode)
         );
        }
    }
}
