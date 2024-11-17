using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestGroups;

public class GetTestGroupsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public string? Name { get; set; }

    public GetTestGroupsInput()
    {
    }
}
