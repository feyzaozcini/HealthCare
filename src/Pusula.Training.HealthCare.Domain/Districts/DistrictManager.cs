using Pusula.Training.HealthCare.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Districts
{
    public class DistrictManager(IDistrictRepository districtRepository) : DomainService
    {
        public virtual async Task<District> CreateAsync(
        Guid cityId,
        string name)
        {

            var district = new District(
             GuidGenerator.Create(),
             cityId,
             name
             );

            district.SetName(name);
            district.SetCityId(cityId);

            return await districtRepository.InsertAsync(district);
        }

        public virtual async Task<District> UpdateAsync(
            Guid id,
            string name
        )
        {

            var district = await districtRepository.GetAsync(id);
            district.SetName(name);

            return await districtRepository.UpdateAsync(district);
        }
    }
}
