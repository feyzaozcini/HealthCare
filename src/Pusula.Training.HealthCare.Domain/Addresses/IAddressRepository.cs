using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Addresses
{
    public interface IAddressRepository : IRepository<Address, Guid>
    {
        Task DeleteAllAsync(
            string? filterText = null,
            Guid? patientId = null,
            Guid? countryId = null,
            Guid? cityId = null,
            Guid? districtId = null,
            Guid? villageId = null,
            string? addressDescription = null,
            bool? isPrimary = null,
            CancellationToken cancellationToken = default);

        Task<List<Address>> GetListAsync(
            string? filterText = null,
            Guid? patientId = null,
            Guid? countryId = null,
            Guid? cityId = null,
            Guid? districtId = null,
            Guid? villageId = null,
            string? addressDescription = null,
            bool? isPrimary = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string? filterText = null,
            Guid? patientId = null,
            Guid? countryId = null,
            Guid? cityId = null,
            Guid? districtId = null,
            Guid? villageId = null,
            string? addressDescription = null,
            bool? isPrimary = null,
            CancellationToken cancellationToken = default);

        Task<Address> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<List<Address>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            Guid? patientId = null,
            Guid? countryId = null,
            Guid? cityId = null,
            Guid? districtId = null,
            Guid? villageId = null,
            string? addressDescription = null,
            bool? isPrimary = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);
    }
}
