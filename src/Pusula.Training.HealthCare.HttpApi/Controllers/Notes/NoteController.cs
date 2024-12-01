using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Pusula.Training.HealthCare.Notes;

namespace Pusula.Training.HealthCare.Controllers.Notes
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Note")]
    [Route("api/app/notes")]
    public class NoteController(INotesAppService notesAppService)
    : HealthCareController, INotesAppService
    {
        [HttpGet]
        public virtual Task<PagedResultDto<NoteDto>> GetListAsync(GetNotesInput input) => notesAppService.GetListAsync(input);


        [HttpGet]
        [Route("{id}")]
        public virtual Task<NoteDto> GetAsync(Guid id) => notesAppService.GetAsync(id);


        [HttpPost]
        public virtual Task<NoteDto> CreateAsync(NoteCreateDto input) => notesAppService.CreateAsync(input);


        [HttpPut]
        [Route("{id}")]
        public virtual Task<NoteDto> UpdateAsync(NoteUpdateDto input) => notesAppService.UpdateAsync(input);


        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id) => notesAppService.DeleteAsync(id);


        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => notesAppService.GetDownloadTokenAsync();


        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> noteIds) => notesAppService.DeleteByIdsAsync(noteIds);


        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetNotesInput input) => notesAppService.DeleteAllAsync(input);
    }
}
