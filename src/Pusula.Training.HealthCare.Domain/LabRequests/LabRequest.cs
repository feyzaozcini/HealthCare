using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequest : FullAuditedEntity<Guid>
{
    [NotNull]
    public Guid ProtocolId { get; private set; }
    [NotNull]
    public Guid DoctorId { get; private set; }
    [NotNull]
    public Guid TestGroupItemId { get; private set; }

    [NotNull]
    public string Name { get; private set; } = null!;
    public DateTime Date { get; private set; }
    [NotNull]
    public RequestStatusEnum Status { get; private set; }

    protected LabRequest()
    {

    }

    public LabRequest(Guid id, Guid protocolId, Guid doctorId, Guid testGroupItemId, string name, DateTime date, RequestStatusEnum status)
    {
        Id = id;
        SetProtocolId(protocolId);
        SetDoctorId(doctorId);
        SetTestGroupItemId(testGroupItemId);
        SetName(name);
        SetDate(date);
        SetRequestStatus(LabRequestConsts.DefaultStatus);
    }

    public void SetProtocolId(Guid protocolId)
    {
        ProtocolId = Check.NotNull(protocolId, nameof(protocolId));
    }

    public void SetDoctorId(Guid doctorId)
    {
        DoctorId = Check.NotNull(doctorId, nameof(doctorId));
    }

    public void SetTestGroupItemId(Guid testGroupItemId)
    {
        TestGroupItemId = Check.NotNull(testGroupItemId, nameof(testGroupItemId));
    }
    public void SetName(string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), LabRequestConsts.NameMaxLength, LabRequestConsts.NameMinLength);
        Name = name;
    }

    public void SetDate(DateTime date)
    {
        Date = date;
    }

    public void SetRequestStatus(RequestStatusEnum status)
    {
        Status = Enum.IsDefined(typeof(RequestStatusEnum), status) ? status : RequestStatusEnum.Pending;
    }
}

