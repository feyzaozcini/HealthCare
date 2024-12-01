using JetBrains.Annotations;
using Pusula.Training.HealthCare.DepartmentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Pusula.Training.HealthCare.ProtocolTypes
{
    public class ProtocolTypeManager(IProtocolTypeRepository protocolTypeRepository) : DomainService
    {
        public virtual async Task<ProtocolType> CreateAsync(
        string name)
        {

            var protocolType = new ProtocolType(
             GuidGenerator.Create(),
             name
             );

            protocolType.SetName(name);

            return await protocolTypeRepository.InsertAsync(protocolType);
        }

        public virtual async Task<ProtocolType> UpdateAsync(
            Guid id,
            string name, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var protocolType = await protocolTypeRepository.GetAsync(id);
            protocolType.SetName(name);
            protocolType.SetConcurrencyStampIfNotNull(concurrencyStamp);

            return await protocolTypeRepository.UpdateAsync(protocolType);
        }

    }
}
