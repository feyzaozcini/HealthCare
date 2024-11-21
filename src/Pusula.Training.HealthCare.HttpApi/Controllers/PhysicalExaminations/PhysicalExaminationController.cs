using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.PhysicalExaminations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Controllers.PhysicalExaminations
{
    [RemoteService]
    [Area("app")]
    [ControllerName("PhysicalExamiantion")]
    [Route("api/app/physical-examination")]
    public class PhysicalExaminationController(IPhysicalExaminationsAppService physicalExaminationsAppService)
        : HealthCareController, IPhysicalExaminationsAppService
    {

        [HttpPost]
        public Task<PhysicalExaminationDto> CreateAsync(PhysicalExaminationCreateDto input) =>
            physicalExaminationsAppService.CreateAsync(input);


        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => physicalExaminationsAppService.DeleteAsync(id);


        [HttpGet]
        [Route("{id}")]
        public Task<PhysicalExaminationDto> GetAsync(Guid id) => physicalExaminationsAppService.GetAsync(id);

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<PhysicalExaminationWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id) =>
            physicalExaminationsAppService.GetWithNavigationPropertiesAsync(id);


        [HttpGet]
        [Route("with-protocolId/{protocolId}")]
        public Task<PhysicalExaminationDto> GetWithProtocolIdAsync(Guid protocolId) =>
            physicalExaminationsAppService.GetWithProtocolIdAsync(protocolId);


        [HttpPut]
        public Task<PhysicalExaminationDto> UpdateAsync(PhysicalExaminationUpdateDto input) =>
            physicalExaminationsAppService.UpdateAsync(input);
       
    }
}
