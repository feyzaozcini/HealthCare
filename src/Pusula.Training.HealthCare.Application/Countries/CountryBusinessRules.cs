using System.Threading.Tasks;
using System.Threading;
using Volo.Abp;
using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Countries;

public class CountryBusinessRules
{
    private readonly ICountryRepository _countryRepository;

    public CountryBusinessRules(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task CountryNameCannotBeDuplicatedWhenInserted(string name)
    {
        var existingCountry = await _countryRepository.FirstOrDefaultAsync(c => c.Name == name);
        if (existingCountry != null)
        {
            throw new BusinessException(CountryBusinessMessages.CountryNameExists);
        }
    }

    public Task CountryShouldExistWhenSelected(Country? country)
    {
        if (country == null)
            throw new BusinessException(CountryBusinessMessages.CountryNotExists);
        return Task.CompletedTask;
    }

    public async Task CountryIdShouldExistWhenSelected(Guid id)
    {
        Country? country = await _countryRepository.GetAsync(
            predicate: l => l.Id == id
        );
        await CountryShouldExistWhenSelected(country);
    }
}
