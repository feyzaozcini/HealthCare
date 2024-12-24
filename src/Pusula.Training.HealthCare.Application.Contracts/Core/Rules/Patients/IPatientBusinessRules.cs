using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.Patients
{
    public interface IPatientBusinessRules : IBusinessRules
    {
        Task IdentityNumberCannotBeDuplicatedWhenInserted(string identityNumber);

        Task ValidateMernisAsync(IdentityValidationDto identityValidationDto);
    }
}
