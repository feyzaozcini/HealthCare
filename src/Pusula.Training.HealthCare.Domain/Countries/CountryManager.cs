using JetBrains.Annotations;
using Pusula.Training.HealthCare.Departments;
using System.Threading.Tasks;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Countries;

public class CountryManager(ICountryRepository countryRepository) : DomainService
{
    public virtual async Task<Country> CreateAsync(
        string name, 
        string code
        )
    {

        var country = new Country(
         GuidGenerator.Create(),
         name,
         code
         );

        country.SetName(name);
        country.SetCode(code);

        return await countryRepository.InsertAsync(country);
    }

    public virtual async Task<Country> UpdateAsync(
        Guid id,
        string name,
        string code
    )
    {

        var country = await countryRepository.GetAsync(id);
        country.SetName(name);
        country.SetCode(code);

        return await countryRepository.UpdateAsync(country);
    }
}
