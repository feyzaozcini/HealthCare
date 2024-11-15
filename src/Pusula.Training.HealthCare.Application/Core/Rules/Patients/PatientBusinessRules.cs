using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.Patients
{
    public class PatientBusinessRules : IPatientBusinessRules
    {
        private readonly IPatientRepository _patientRepository;

        public PatientBusinessRules(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task IdentityNumberCannotBeDuplicatedWhenInserted(string identityNumber)
        {
            var existingPatient = await _patientRepository.FindByIdentityNumberAsync(identityNumber);
            if (existingPatient != null)
            {
                HealthCareException.Throw(HealthCareDomainErrorCodes.IdentityNumberAlreadyExists);
            }
        }

        public void IdentityNumberCannotBeEmpty(string identityNumber)
        {
            throw new NotImplementedException();
        }
    }
}

