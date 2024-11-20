using JetBrains.Annotations;
using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp;
using Volo.Abp.Data;

namespace Pusula.Training.HealthCare.Cities
{
    public class CityManager(ICityRepository cityRepository) : DomainService
    {
        public virtual async Task<City> CreateAsync(
        Guid countryId,
        string name)
        {
            Check.NotNull(countryId, nameof(countryId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), CityConsts.NameMaxLength);

            var city = new City(
             GuidGenerator.Create(),
             countryId,
             name
             );

            return await cityRepository.InsertAsync(city);
        }

        public virtual async Task<City> UpdateAsync(
            Guid id,
            string name
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), CityConsts.NameMaxLength);

            var city = await cityRepository.GetAsync(id);

            city.Name = name;

            return await cityRepository.UpdateAsync(city);
        }
    }
}
