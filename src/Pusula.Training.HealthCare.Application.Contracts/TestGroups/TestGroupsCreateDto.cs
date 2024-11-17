using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.TestGroups;

public class TestGroupsCreateDto
{
    [Required]
    [StringLength(TestGroupConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
}
