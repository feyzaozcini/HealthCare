using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.LabRequests;

public interface ILabRequestRepository : IRepository<LabRequest, Guid>
{
    Task DeleteAllAsync(
        string? filterText = null,
        Guid? protocolId = null,
        Guid? doctorId = null,
        string? doctorName = null,
        string? doctorSurname = null,
        string? patientName = null,
        string? patientSurname = null,
        int? protocolNo = null,
        DateTime? date = null,
        RequestStatusEnum? status = null,
        string? description = null,
        CancellationToken cancellationToken = default
        );

    Task<List<LabRequest>> GetListAsync(
                string? filterText = null,
                Guid? protocolId = null,
                Guid? doctorId = null,
                string? doctorName = null,
                string? doctorSurname = null,
        string? patientName = null,
        string? patientSurname = null,
        int? protocolNo = null,
                DateTime? date = null,
                RequestStatusEnum? status = null,
                string? description = null,
                string? sorting = null,
                int maxResultCount = int.MaxValue,
                int skipCount = 0,
                CancellationToken cancellationToken = default
        );

    Task<long> GetCountAsync(
                string? filterText = null,
                Guid? protocolId = null,
                Guid? doctorId = null,
                string? doctorName = null,
                string? doctorSurname = null,
        string? patientName = null,
        string? patientSurname = null,
        int? protocolNo = null,
                DateTime? date = null,
                RequestStatusEnum? status = null,
                string? description = null,
                CancellationToken cancellationToken = default
                );

    Task<LabRequest> GetWithNavigationPropertiesAsync(
                Guid id,
                CancellationToken cancellationToken = default);

    Task<List<LabRequest>> GetListWithNavigationPropertiesAsync(
                string? filterText = null,
                Guid? protocolId = null,
                Guid? doctorId = null,
                string? doctorName = null,
                string? doctorSurname = null,
        string? patientName = null,
        string? patientSurname = null,
        int? protocolNo = null,
                DateTime? date = null,
                RequestStatusEnum? status = null,
                string? description = null,
                string? sorting = null,
                int maxResultCount = int.MaxValue,
                int skipCount = 0,
                CancellationToken cancellationToken = default);
}
