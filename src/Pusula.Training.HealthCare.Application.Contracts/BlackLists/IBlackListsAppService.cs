using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.BlackLists
{
    public interface IBlackListsAppService : IApplicationService
    {
        Task<PagedResultDto<BlackListWithNavigationPropertiesDto>> GetListAsync(GetBlackListInput input);

        Task<BlackListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<BlackListDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetPatientLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<BlackListDto> CreateAsync(BlackListCreateDto input);

        Task<BlackListDto> UpdateAsync(BlackListUpdateDto input);

        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
