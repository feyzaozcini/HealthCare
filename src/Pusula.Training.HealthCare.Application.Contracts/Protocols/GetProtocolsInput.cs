using Volo.Abp.Application.Dtos;
using System;

namespace Pusula.Training.HealthCare.Protocols;

public class GetProtocolsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public virtual DateTime? StartTime { get; set; }
    public virtual DateTime? EndTime { get; set; }
    public virtual int? No { get; set; }
    public ProtocolStatus? ProtocolStatus { get; set; }
    public virtual Guid? ProtocolTypeId { get; set; }
    public virtual Guid? ProtocolNoteId { get; set; }
    public virtual Guid? ProtocolInsuranceId { get; set; }
    public virtual Guid? PatientId { get; set; }
    public virtual Guid? DepartmentId { get; set; }
    public virtual Guid? DoctorId { get; set; }

    public GetProtocolsInput()
    {

    }

    public GetProtocolsInput(
        string? filterText,
        DateTime startTime,
        DateTime endTime,
        int no,
        ProtocolStatus protocolStatus,
        Guid protocolTypeId,
        Guid protocolNoteId,
        Guid protocolInsuranceId,
        Guid patientId,
        Guid departmentId,
        Guid doctorId,
        int currentPage,
        int pageSize)
    {
        FilterText = filterText;
        StartTime = startTime;
        EndTime = endTime;
        No = no;
        ProtocolStatus = protocolStatus;
        ProtocolTypeId = protocolTypeId;
        ProtocolNoteId = protocolNoteId;
        ProtocolInsuranceId = protocolInsuranceId;
        PatientId = patientId;
        DepartmentId = departmentId;
        DoctorId = doctorId;

        MaxResultCount = pageSize;
        SkipCount = (currentPage - 1) * pageSize;
    }
}