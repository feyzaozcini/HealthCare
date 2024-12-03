using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Protocols;

public interface IProtocolRepository : IRepository<Protocol, Guid>
{
    Task DeleteAllAsync(
        string? filterText = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        ProtocolStatus? protocolStatus = null,
        Guid? protocolTypeId = null,
        Guid? protocolNoteId = null,
        Guid? protocolInsuranceId = null,
        Guid? patientId = null,
        Guid? departmentId = null,
        Guid? doctorId = null,
        int? no = null,
        CancellationToken cancellationToken = default);

    Task<List<Protocol>> GetListAsync(
        string? filterText = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        ProtocolStatus? protocolStatus = null,
        Guid? protocolTypeId = null,
        Guid? protocolNoteId = null,
        Guid? protocolInsuranceId = null,
        Guid? patientId = null,
        Guid? departmentId = null,
        Guid? doctorId = null,
        int? no = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(
        string? filterText = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        ProtocolStatus? protocolStatus = null,
        Guid? protocolTypeId = null,
        Guid? protocolNoteId = null,
        Guid? protocolInsuranceId = null,
        Guid? patientId = null,
        Guid? departmentId = null,
        Guid? doctorId = null,
        int? no = null,
        CancellationToken cancellationToken = default);

    Task<Protocol> GetWithNavigationPropertiesAsync(
        Guid id,
        CancellationToken cancellationToken = default);


    Task<List<Protocol>> GetListWithNavigationPropertiesAsync(
        string? filterText = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        ProtocolStatus? protocolStatus = null,
        Guid? protocolTypeId = null,
        Guid? protocolNoteId = null,
        Guid? protocolInsuranceId = null,
        Guid? patientId = null,
        Guid? departmentId = null,
        Guid? doctorId = null,
        int? no = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);
}