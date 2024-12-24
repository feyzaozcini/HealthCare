using JetBrains.Annotations;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Protocols;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequest : FullAuditedEntity<Guid>
{
    [NotNull]
    public Guid ProtocolId { get; private set; }
    public Protocol Protocol { get; private set; }
    [NotNull]
    public Guid DoctorId { get; private set; }
    public Doctor Doctor { get; private set; }
    [NotNull]
    public DateTime Date { get; private set; }
    [NotNull]
    public RequestStatusEnum Status { get; private set; }
    public string? Description { get; private set; }

    protected LabRequest()
    {

    }

    public LabRequest(Guid id, Guid protocolId, Guid doctorId, DateTime date, RequestStatusEnum status, string? description)
    {
        Id = id;
        SetProtocolId(protocolId);
        SetDoctorId(doctorId);
        SetDate(date);
        SetRequestStatus(status);
        SetDescription(description);
    }

    public void SetProtocolId(Guid protocolId) => ProtocolId = Check.NotNull(protocolId, nameof(protocolId));

    public void SetDoctorId(Guid doctorId) => DoctorId = Check.NotNull(doctorId, nameof(doctorId));

    public void SetDate(DateTime date) => Date = date;

    public void SetRequestStatus(RequestStatusEnum status) => Status = Enum.IsDefined(typeof(RequestStatusEnum), status) ? status : LabRequestConsts.DefaultStatus;

    public void SetDescription(string? description) => Description = Check.Length(description, nameof(description), LabRequestConsts.DescriptionMaxLength);
}

