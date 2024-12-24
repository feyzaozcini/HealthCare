using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.ControlNotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.ControlNotes
{
    [RemoteService]
    [Area("app")]
    [ControllerName("ControlNote")]
    [Route("api/app/control-notes")]

    public class ControlNoteController(IControlNotesAppService controlNotesAppService)
    : HealthCareController, IControlNotesAppService
    {
        [HttpPost]
        public Task<ControlNoteDto> CreateAsync(ControlNoteCreateDto input) => controlNotesAppService.CreateAsync(input);

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => controlNotesAppService.DeleteAsync(id);

        [HttpGet]
        [Route("{id}")]
        public Task<ControlNoteDto> GetAsync(Guid id) => controlNotesAppService.GetAsync(id);

        [HttpGet]
        public Task<PagedResultDto<ControlNoteDto>> GetListAsync(GetControlsInput input) => controlNotesAppService.GetListAsync(input);


        [HttpGet]
        [Route("with-navigation-properties/{protocolId}")]
        public Task<ControlNoteDto> GetWithNavigationPropertiesByProtocolIdAsync(Guid protocolId) => 
            controlNotesAppService.GetWithNavigationPropertiesByProtocolIdAsync(protocolId);


        [HttpPut]
        public Task<ControlNoteDto> UpdateAsync(ControlNoteUpdateDto input) => controlNotesAppService.UpdateAsync(input);
    }
}
