using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.ControlNotes
{
    public interface IControlNotesAppService : IApplicationService
    {
        Task<ControlNoteDto> GetWithNavigationPropertiesByProtocolIdAsync(Guid protocolId);
        Task<PagedResultDto<ControlNoteDto>> GetListAsync(GetControlsInput input);
        Task<ControlNoteDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<ControlNoteDto> CreateAsync(ControlNoteCreateDto input);
        Task<ControlNoteDto> UpdateAsync(ControlNoteUpdateDto input);
    }
}
