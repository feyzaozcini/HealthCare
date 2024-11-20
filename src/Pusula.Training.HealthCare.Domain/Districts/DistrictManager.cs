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
            Check.NotNull(cityId, nameof(cityId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DistrictConsts.NameMaxLength);

            var district = new District(
             GuidGenerator.Create(),
             cityId,
             name
             );

            return await districtRepository.InsertAsync(district);
        }

        public virtual async Task<District> UpdateAsync(
            Guid id,
            string name
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DistrictConsts.NameMaxLength);

            var district = await districtRepository.GetAsync(id);

            district.Name = name;

            return await districtRepository.UpdateAsync(district);
        }
    }
}
