using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.Patients
{
    public class PatientBusinessRules(IPatientRepository patientRepository) : IPatientBusinessRules
    {
       
        public async Task IdentityNumberCannotBeDuplicatedWhenInserted(string identityNumber)
        {
            HealthCareException.ThrowIf(HealthCareDomainErrorCodes.IdentityNumberAlreadyExists, await patientRepository.FirstOrDefaultAsync(c => c.IdentityNumber == identityNumber) is not null);
        }

        public async Task<Patient?> PatientNotFount(Guid id)
        {
            var patient = await patientRepository.FirstOrDefaultAsync(predicate: c => c.Id == id);
            HealthCareException.ThrowIf(HealthCareDomainErrorCodes.PatientNotFound, patient is null);
            return patient;
        }
    }
}

