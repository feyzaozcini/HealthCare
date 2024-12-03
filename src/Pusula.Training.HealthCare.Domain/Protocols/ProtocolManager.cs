using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Protocols;

public class ProtocolManager(IProtocolRepository protocolRepository) : DomainService
{
    public virtual async Task<Protocol> CreateAsync(
    DateTime startTime,
    DateTime endTime,
    ProtocolStatus protocolStatus,
    Guid protocolTypeId,
    Guid protocolNoteId,
    Guid protocolInsuranceId,
    Guid patientId,
    Guid departmentId,
    Guid doctorId
    )
    {
        var protocol = new Protocol(
         GuidGenerator.Create(),
         startTime,
         endTime,
         protocolStatus,
         protocolTypeId,
         protocolNoteId,
         protocolInsuranceId,
         patientId,
         departmentId,
         doctorId
         );

        return await protocolRepository.InsertAsync(protocol);
    }

    public virtual async Task<Protocol> UpdateAsync(
        Guid id,
        DateTime startTime,
        DateTime endTime,
        ProtocolStatus protocolStatus,
        Guid protocolTypeId,
        Guid protocolNoteId,
        Guid protocolInsuranceId,
        Guid patientId,
        Guid departmentId,
        Guid doctorId,
        [CanBeNull] string? concurrencyStamp = null
    )
    {
        var protocol = await protocolRepository.GetAsync(id);

        protocol.SetStartTime(startTime);
        protocol.SetEndTime(endTime);
        protocol.SetProtocolStatus(protocolStatus);
        protocol.SetProtocolTypeId(protocolTypeId);
        protocol.SetProtocolNoteId(protocolNoteId);
        protocol.SetProtocolInsuranceId(protocolInsuranceId);
        protocol.SetPatientId(patientId);
        protocol.SetDepartmentId(departmentId);
        protocol.SetDoctorId(doctorId);
        protocol.SetConcurrencyStampIfNotNull(concurrencyStamp);

        return await protocolRepository.UpdateAsync(protocol);
    }

}