using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Services;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.Patients
{
    public class PatientBusinessRules(IPatientRepository patientRepository, IMernisValidationService mernisValidationService) : IPatientBusinessRules
    {
        public async Task ValidateMernisAsync(IdentityValidationDto identityValidationDto)
        {
            var isIdentityValid = await mernisValidationService.ValidateIdentityAsync(identityValidationDto);
            HealthCareException.ThrowIf(
                HealthCareDomainErrorCodes.MernisVerificationFailed,
                !isIdentityValid
            );
        }
        public async Task IdentityNumberCannotBeDuplicatedWhenInserted(string identityNumber) => HealthCareException.ThrowIf(HealthCareDomainErrorCodes.IdentityNumberAlreadyExists, await patientRepository.FirstOrDefaultAsync(c => c.IdentityNumber == identityNumber) is not null);
    }
}

