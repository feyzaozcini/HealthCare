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

            var village = new Village(
             GuidGenerator.Create(),
             districtId,
             name
             );

            village.SetName(name);
            village.SetDistrictId(districtId);

            return await villageRepository.InsertAsync(village);
        }

        public virtual async Task<Village> UpdateAsync(
            Guid id,
            string name
        )
        {
            var village = await villageRepository.GetAsync(id);
            village.SetName(name);

            return await villageRepository.UpdateAsync(village);
        }
    }
}
