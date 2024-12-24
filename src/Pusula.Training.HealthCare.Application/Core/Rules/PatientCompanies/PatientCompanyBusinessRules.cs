using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.PatientCompanies;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.PatientCompanies
{
    public class PatientCompanyBusinessRules(IPatientCompanyRepository patientCompanyRepository) : IPatientCompanyBusinessRules
    {
        public async Task DuplicatedPatientCompanyName(string name) => HealthCareException.ThrowIf(HealthCareDomainErrorCodes.PatientCompanyNameExist, await patientCompanyRepository.FirstOrDefaultAsync(c => c.Name == name) is not null);
    }
}

