using Pusula.Training.HealthCare.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.PainTypes
{
    public interface IPainTypesAppService : IApplicationService
    {
        Task<PagedResultDto<PainTypeDto>> GetListAsync(GetPainTypesInput input);
        Task<PainTypeDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<PainTypeDto> CreateAsync(PainTypeCreateDto input);
        Task<PainTypeDto> UpdateAsync(PainTypeUpdateDto input);
        Task DeleteByIdsAsync(List<Guid> painTypeIds);
        Task DeleteAllAsync(GetPainTypesInput input);
    }
}
