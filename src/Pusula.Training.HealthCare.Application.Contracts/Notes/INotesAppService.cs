using Pusula.Training.HealthCare.ProtocolTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Notes
{
    public interface INotesAppService : IApplicationService
    {
        Task<PagedResultDto<NoteDto>> GetListAsync(GetNotesInput input);
        Task<NoteDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<NoteDto> CreateAsync(NoteCreateDto input);
        Task<NoteDto> UpdateAsync(NoteUpdateDto input);
        Task DeleteByIdsAsync(List<Guid> noteIds);
        Task DeleteAllAsync(GetNotesInput input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
