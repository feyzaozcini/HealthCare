using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Anamneses
{
    public interface IAnamnesisAppService : IApplicationService
    {
       
        Task<AnamnesisWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<AnamnesisDto> GetAsync(Guid id);

        Task<AnamnesisDto> GetWithProtocolIdAsync(Guid protocolId);

        Task DeleteAsync(Guid id);

        Task<AnamnesisDto> CreateAsync(AnamnesisCreateDto input);

        Task<AnamnesisDto> UpdateAsync(AnamnesisUpdateDto input);


    }
}
