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
            
            var city = new City(
             GuidGenerator.Create(),
             countryId,
             name
             );

            city.SetName(name);
            city.SetCountryId(countryId);

            return await cityRepository.InsertAsync(city);
        }

        public virtual async Task<City> UpdateAsync(
            Guid id,
            string name
        )
        {
            var city = await cityRepository.GetAsync(id);
            city.SetName(name);

            return await cityRepository.UpdateAsync(city);
        }
    }
}
