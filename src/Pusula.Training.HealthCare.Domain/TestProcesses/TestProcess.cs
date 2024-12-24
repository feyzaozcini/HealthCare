using JetBrains.Annotations;
using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.TestGroupItems;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.TestProcesses;

public class TestProcess : FullAuditedEntity<Guid>
{
    [NotNull]
    public Guid LabRequestId { get; private set; }
    public LabRequest LabRequest { get; private set; }

    [NotNull]
    public Guid TestGroupItemId { get; private set; }
    public TestGroupItem TestGroupItem { get; private set; }

    [NotNull]
    public TestProcessStates Status { get; private set; }
    [CanBeNull]
    public decimal? Result { get; private set; }
    [CanBeNull]
    public DateTime? ResultDate { get; private set; }

    protected TestProcess()
    {

    }

    public TestProcess(Guid id, Guid labRequestId, Guid testGroupItemId, TestProcessStates status, decimal? result, DateTime? resultDate)
    {
        Id = id;
        SetLabRequestId(labRequestId);
        SetTestGroupItemId(testGroupItemId);
        SetTestProcessStates(status);
        SetResult(result);
        SetResultDate(resultDate);
    }

    public void SetLabRequestId(Guid labRequestId) => LabRequestId = Check.NotNull(labRequestId, nameof(labRequestId));
    public void SetTestGroupItemId(Guid testGroupItemId) => TestGroupItemId = Check.NotNull(testGroupItemId, nameof(testGroupItemId));
    public void SetTestProcessStates(TestProcessStates status) => Status = Enum.IsDefined(typeof(TestProcessStates), status) ? status : TestProcessStates.Requested;
    public void SetResult(decimal? result) => Result = result;
    public void SetResultDate(DateTime? resultDate) => ResultDate = resultDate;
}