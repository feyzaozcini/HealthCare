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
    public Guid? ProtocolId { get; set; }
    public Guid? DoctorId { get; set; }
    public string? DoctorName { get; set; }
    public DateTime? Date { get; set; }
    public RequestStatusEnum? Status { get; set; }
    public string? Description { get; set; }

    public GetLabRequestsInput()
    {
    }

    public GetLabRequestsInput(
       string? filterText,
       Guid? protocolId,
       Guid? doctorId,
       string? doctorName,
       DateTime? date,
       RequestStatusEnum? status,
       string? description,
       int currentPage,
       int pageSize)
    {
        FilterText = filterText;
        ProtocolId = protocolId;
        DoctorId = doctorId;
        DoctorName = doctorName;
        Date = date;
        Status = status;
        Description = description;

        MaxResultCount = pageSize;
        SkipCount = (currentPage - 1) * pageSize;
    }

}
