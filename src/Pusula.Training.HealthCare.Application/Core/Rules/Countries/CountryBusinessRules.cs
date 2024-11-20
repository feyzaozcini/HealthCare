using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.Countries
{
    public class CountryBusinessRules(ICountryRepository countryRepository) : ICountryBusinessRules
    {
       
        public async Task CountryNameCannotBeDuplicatedWhenInserted(string name)
        {
            HealthCareException.ThrowIf(HealthCareDomainErrorCodes.CountryNameExists, await countryRepository.FirstOrDefaultAsync(c => c.Name == name) is not null);
        }

        
    }
}
