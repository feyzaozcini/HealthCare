using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.PatientCompanies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.PatientCompanies
{
    public class PatientCompanyBusinessRules(IPatientCompanyRepository patientCompanyRepository) : IPatientCompanyBusinessRules
    {
        public async Task DuplicatedPatientCompanyName(string name)
        {
            HealthCareException.ThrowIf(HealthCareDomainErrorCodes.PatientCompanyNameExist, await patientCompanyRepository.FirstOrDefaultAsync(c => c.Name == name) is not null);
        }

        public async Task<PatientCompany?> PatientCompanyNotFound(Guid id)
        {
            var patientCompany = await patientCompanyRepository.FirstOrDefaultAsync(predicate: c => c.Id == id);
            HealthCareException.ThrowIf(HealthCareDomainErrorCodes.PatientCompanyNameNotFound, patientCompany is null);
            return patientCompany;
        }
    }
}
