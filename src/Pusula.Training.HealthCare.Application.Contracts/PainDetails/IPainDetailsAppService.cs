using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.PainDetails
{
    public interface IPainDetailsAppService : IApplicationService
    {
        Task<PainDetailDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<PainDetailDto> GetWithNavigationPropertiesByProtocolIdAsync(Guid protocolId);

        Task<PainDetailDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<PainDetailDto> CreateAsync(PainDetailCreateDto input);
        Task<PainDetailDto> UpdateAsync(PainDetailUpdateDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetPainTypeLookupAsync(LookupRequestDto input);


    }
}
