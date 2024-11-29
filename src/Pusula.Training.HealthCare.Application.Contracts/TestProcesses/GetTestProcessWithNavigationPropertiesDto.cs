using Pusula.Training.HealthCare.LabRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.TestProcesses;

public class GetTestProcessWithNavigationPropertiesDto
{
    public TestProcessStates Status { get; set; }
    public decimal? Result { get; set; }
    public DateTime? ResultDate { get; set; }

    //LabRequest Verileri
    public DateTime? LabRequestCreatedDate { get; set; }
    public RequestStatusEnum LabRequestStatus { get; set; }
    public string? LabRequestDescription { get; set; }

    //TestGroupItem Verileri
    public string? TestGroupItemName { get; set; }
    public string? TestGroupItemCode { get; set; }
    public string? TestGroupItemType { get; set; }
    public string? TestGroupItemDescription { get; set; }
    public string? TestGroupItemTurnaroundTime { get; set; }
}
