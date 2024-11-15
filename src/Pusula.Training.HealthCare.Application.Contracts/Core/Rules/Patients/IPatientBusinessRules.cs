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

        void IdentityNumberCannotBeEmpty(string identityNumber);
    }
}
