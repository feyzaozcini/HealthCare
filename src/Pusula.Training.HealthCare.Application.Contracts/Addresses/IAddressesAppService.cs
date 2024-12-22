using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Addresses
{
    public interface IAddressesAppService : IApplicationService
    {
        Task<PagedResultDto<AddressDto>> GetListAsync(GetAddressesInput input);
        Task<PagedResultDto<AddressDto>> GetListWithNavigationPropertiesAsync(GetAddressesInput input);
        Task<AddressDto> GetWithNavigationPropertiesAsync(Guid id);
        Task<AddressDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<AddressDto> CreateAsync(AddressCreateDto input);
        Task<AddressDto> UpdateAsync(Guid id, AddressUpdateDto input);
        Task DeleteByIdsAsync(List<Guid> addressesIds);
        Task DeleteAllAsync(GetAddressesInput input);
        Task<Pusula.Training.HealthCare.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
