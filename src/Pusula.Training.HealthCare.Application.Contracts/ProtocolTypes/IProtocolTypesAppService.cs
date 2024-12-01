using Pusula.Training.HealthCare.DepartmentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.ProtocolTypes
{
    public interface IProtocolTypesAppService : IApplicationService
    {
        Task<PagedResultDto<ProtocolTypeDto>> GetListAsync(GetProtocolTypesInput input);
        Task<ProtocolTypeDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<ProtocolTypeDto> CreateAsync(ProtocolTypeCreateDto input);
        Task<ProtocolTypeDto> UpdateAsync(ProtocolTypeUpdateDto input);
        Task DeleteByIdsAsync(List<Guid> protocolTypeIds);
        Task DeleteAllAsync(GetProtocolTypesInput input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
