using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.ControlNotes
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Examinations.Default)]
    public class ControlNotesAppService(IControlNoteRepository controlNoteRepository, ControlNoteManager controlNoteManager)
    : HealthCareAppService, IControlNotesAppService
    {

        [Authorize(HealthCarePermissions.Examinations.Create)]
        public async Task<ControlNoteDto> CreateAsync(ControlNoteCreateDto input)
        {
            var controlNote = await controlNoteManager.CreateAsync(input.ProtocolId,input.NoteDate,input.Note);

            return ObjectMapper.Map<ControlNote, ControlNoteDto>(controlNote);
        }

        [Authorize(HealthCarePermissions.Examinations.Delete)]
        public async Task DeleteAsync(Guid id) => await controlNoteRepository.DeleteAsync(id);

        public async Task<ControlNoteDto> GetAsync(Guid id) => ObjectMapper.Map<ControlNote, ControlNoteDto>(
                await controlNoteRepository.GetAsync(id));

        public async Task<PagedResultDto<ControlNoteDto>> GetListAsync(GetControlsInput input)
        {
            var totalCount = await controlNoteRepository.GetCountAsync(input.FilterText, input.ProtocolId, input.NoteDate,
                input.Note);

            //var items = await controlNoteRepository.GetListAsync(input.FilterText,input.ProtocolId,input.NoteDate,input.Note,
            //    input.Sorting, input.MaxResultCount,input.SkipCount);
            var items = await controlNoteRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.ProtocolId, input.NoteDate, input.Note,
                input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ControlNoteDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ControlNote>, List<ControlNoteDto>>(items)
            };
        }

        public async Task<ControlNoteDto> GetWithNavigationPropertiesByProtocolIdAsync(Guid protocolId)
        {
            var controlNote = await controlNoteRepository.GetWithNavigationPropertiesByProtocolIdAsync(protocolId);
            return ObjectMapper.Map<ControlNote, ControlNoteDto>(controlNote);
        }

        [Authorize(HealthCarePermissions.Examinations.Edit)]

        public async Task<ControlNoteDto> UpdateAsync(ControlNoteUpdateDto input)
        {
            var controlNote = await controlNoteManager.UpdateAsync(input.Id,input.ProtocolId, input.NoteDate, input.Note);

            return ObjectMapper.Map<ControlNote, ControlNoteDto>(controlNote);
        }
    }
}
