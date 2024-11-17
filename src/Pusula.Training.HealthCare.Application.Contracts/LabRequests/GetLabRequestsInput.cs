using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.LabRequests;

public class GetLabRequestsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public Guid ProtocolId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid TestGroupItemId { get; set; }

    public string? Name { get; set; }
    public DateTime? Date { get; set; }
    public RequestStatusEnum? Status { get; set; }

    public GetLabRequestsInput()
    {
    }
}
