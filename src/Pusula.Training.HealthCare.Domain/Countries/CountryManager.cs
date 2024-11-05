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
        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), CountryConsts.NameMaxLength, CountryConsts.NameMinLength);

        Check.NotNull(code, nameof(code));
        Check.Length(code, nameof(code), CountryConsts.CodeMaxLength, CountryConsts.CodeMinLength);

        var country = new Country(
         GuidGenerator.Create(),
         name,
         code
         );

        return await countryRepository.InsertAsync(country);
    }

    public virtual async Task<Country> UpdateAsync(
        Guid id,
        string name,
        string code,
        [CanBeNull] string? concurrencyStamp = null
    )
    {
        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), CountryConsts.NameMaxLength, CountryConsts.NameMinLength);

        Check.NotNull(code, nameof(code));
        Check.Length(code, nameof(code), CountryConsts.CodeMaxLength, CountryConsts.CodeMinLength);

        var country = await countryRepository.GetAsync(id);
        country.Name = name;
        country.Code = code;

        return await countryRepository.UpdateAsync(country);
    }
}
