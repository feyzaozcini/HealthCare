using Pusula.Training.HealthCare.PatientCompanies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.PatientCompanies
{
    public interface IPatientCompanyBusinessRules : IBusinessRules
    {
        Task DuplicatedPatientCompanyName(string name);

    }
}
