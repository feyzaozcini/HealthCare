using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.PainTypes
{
    public class PainTypeManager(IPainTypeRepository painTypeRepository) : DomainService
    {
        public virtual async Task<PainType> CreateAsync(string name)
        {
            var painType = new PainType(GuidGenerator.Create(), name);

            return await painTypeRepository.InsertAsync(painType);
        }


        public virtual async Task<PainType> UpdateAsync(Guid id,string name)
        {
            var painType = await painTypeRepository.GetAsync(id);
            painType.SetName(name);

            return await painTypeRepository.UpdateAsync(painType);

        }
    }
}
