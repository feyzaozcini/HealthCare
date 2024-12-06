using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestProcesses;

public class GetTestProcessesInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public Guid? LabRequestId { get; set; }
    public Guid? TestGroupItemId { get; set; }
    public TestProcessStates? Status { get; set; }
    public decimal? Result { get; set; }
    public DateTime? ResultDate { get; set; }

    //Doktor
    public string? DoctorName { get; set; }
    public string? DoctorSurname { get; set; }
    public string PatientName { get; set; } = null!;
    public string PatientSurname { get; set; } = null!;

    //Test
    public string? TestGroupItemName { get; set; }

    //LabRequest
    public DateTime? LabRequestCreatedTime { get; set; }



    public GetTestProcessesInput()
    {
        MaxResultCount = PagedAndSortedResultRequestDto.MaxMaxResultCount;
        SkipCount = 0;
    }


    public GetTestProcessesInput(string? filterText, Guid? labRequestId, Guid? testGroupItemId, TestProcessStates? status, decimal? result, DateTime? resultDate)
    {
        FilterText = filterText;
        LabRequestId = labRequestId;
        TestGroupItemId = testGroupItemId;
        Status = status;
        Result = result;
        ResultDate = resultDate;
    }
}
