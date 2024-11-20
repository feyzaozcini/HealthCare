using Pusula.Training.HealthCare.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Villages
{
    public class VillageManager(IVillageRepository villageRepository) : DomainService
    {
        public virtual async Task<Village> CreateAsync(
        Guid districtId,
        string name)
        {
            Check.NotNull(districtId, nameof(districtId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), VillageConsts.NameMaxLength);

            var village = new Village(
             GuidGenerator.Create(),
             districtId,
             name
             );

            return await villageRepository.InsertAsync(village);
        }

        public virtual async Task<Village> UpdateAsync(
            Guid id,
            string name
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), VillageConsts.NameMaxLength);

            var village = await villageRepository.GetAsync(id);

            village.Name = name;

            return await villageRepository.UpdateAsync(village);
        }
    }
}
