using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.PatientCompanies
{
    public class PatientCompanyBusinessRules(IPatientCompanyRepository patientCompanyRepository)
    {
        public async Task DuplicatedPatientCompanyName(string name)
        {
            var existingPatientCompany = await patientCompanyRepository.FirstOrDefaultAsync(c => c.Name == name);
            if(existingPatientCompany != null)
            {
                throw new BusinessException(PatientCompanyBusinessMessages.PatientCompanyNameExist);
            }
        }

        public async Task<PatientCompany> PatientCompanyNotFound(Guid id)
        {
            var patientCompany = await patientCompanyRepository.FirstOrDefaultAsync(predicate: c => c.Id == id);

            if (patientCompany == null)
            {
                throw new BusinessException(PatientCompanyBusinessMessages.PatientCompanyDoesNotExist);
            }

            return patientCompany;
        }
    }
}
