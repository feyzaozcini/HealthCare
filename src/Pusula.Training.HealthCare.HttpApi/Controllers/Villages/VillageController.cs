using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp;
using Pusula.Training.HealthCare.Villages;

namespace Pusula.Training.HealthCare.Controllers.Villages
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Village")]
    [Route("api/app/villages")]
    public class VillageController(IVillagesAppService villagesAppService)
            : HealthCareController, IVillagesAppService
    {
        [HttpGet]
        public Task<PagedResultDto<VillageDto>> GetListAsync(GetVillagesInput input) => villagesAppService.GetListAsync(input);

        [HttpGet]
        [Route("{id}")]
        public virtual Task<VillageDto> GetAsync(Guid id) => villagesAppService.GetAsync(id);

        [HttpPost]
        public virtual Task<VillageDto> CreateAsync(VillageCreateDto input) => villagesAppService.CreateAsync(input);

        [HttpPut]
        [Route("{id}")]
        public virtual Task<VillageDto> UpdateAsync(Guid id, VillageUpdateDto input) => villagesAppService.UpdateAsync(id, input);

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => villagesAppService.DeleteAsync(id);

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(VillageExcelDownloadDto input) => villagesAppService.GetListAsExcelFileAsync(input);

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => villagesAppService.GetDownloadTokenAsync();

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> villageIds) => villagesAppService.DeleteByIdsAsync(villageIds);

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetVillagesInput input) => villagesAppService.DeleteAllAsync(input);
    }
}
