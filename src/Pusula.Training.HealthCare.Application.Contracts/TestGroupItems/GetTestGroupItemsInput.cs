using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class GetTestGroupItemsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public Guid? TestGroupId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? TestType { get; set; }
    public string? Description { get; set; }
    public int? TurnaroundTime { get; set; }


    public GetTestGroupItemsInput()
    {
            FilterText = string.Empty;
            Name = string.Empty;
            Code = string.Empty;
            TestType = string.Empty;
            Description = string.Empty;
            TestGroupId = Guid.Empty;
            MaxResultCount = PagedAndSortedResultRequestDto.DefaultMaxResultCount; 
            SkipCount = 0;
    }
}