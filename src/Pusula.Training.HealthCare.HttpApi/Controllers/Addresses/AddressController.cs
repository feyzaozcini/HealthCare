using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Pusula.Training.HealthCare.Addresses;

namespace Pusula.Training.HealthCare.Controllers.Addresses
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Address")]
    [Route("api/app/addresses")]
    public class AddressController(IAddressesAppService addressesAppService)
    : HealthCareController, IAddressesAppService
    {


        [HttpGet]
        public virtual Task<PagedResultDto<AddressDto>> GetListAsync(GetAddressesInput input) => addressesAppService.GetListAsync(input);


        [HttpGet]
        [Route("get-with-navigation-properties/{id}")]
        public virtual Task<AddressDto> GetWithNavigationPropertiesAsync(Guid id) => addressesAppService.GetWithNavigationPropertiesAsync(id);


        [HttpGet]
        [Route("{id}")]
        public virtual Task<AddressDto> GetAsync(Guid id) => addressesAppService.GetAsync(id);

       
        [HttpPost]
        public virtual Task<AddressDto> CreateAsync(AddressCreateDto input) => addressesAppService.CreateAsync(input);


        [HttpPut]
        [Route("{id}")]
        public virtual Task<AddressDto> UpdateAsync(Guid id, AddressUpdateDto input) => addressesAppService.UpdateAsync(id, input);


        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id) => addressesAppService.DeleteAsync(id);


        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => addressesAppService.GetDownloadTokenAsync();


        [HttpGet]
        [Route("get-list-with-navigation-properties")]
        public Task<PagedResultDto<AddressDto>> GetListWithNavigationPropertiesAsync(GetAddressesInput input) => addressesAppService.GetListWithNavigationPropertiesAsync(input);


        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> addressIds) => addressesAppService.DeleteByIdsAsync(addressIds);


        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetAddressesInput input) => addressesAppService.DeleteAllAsync(input);

    }
}
