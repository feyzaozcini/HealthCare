using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Patients
{
    public class PatientBusinessRules
    {
        private readonly IPatientRepository _patientRepository;

        public PatientBusinessRules(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task IdentityNumberCannotBeDuplicatedWhenInserted(string identityNumber)
        {
            var existingPatient = await _patientRepository.FirstOrDefaultAsync(c => c.IdentityNumber == identityNumber);
            if (existingPatient != null)
            {
                throw new BusinessException(PatientBusinessMessages.IdentityNumberAlreadyExists);
            }
        }
    }
}
