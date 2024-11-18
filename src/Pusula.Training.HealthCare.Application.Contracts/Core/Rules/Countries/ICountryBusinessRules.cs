using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.PatientCompanies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.Countries
{
    public interface ICountryBusinessRules : IBusinessRules
    {
        Task CountryNameCannotBeDuplicatedWhenInserted(string name);

        
    }
}
