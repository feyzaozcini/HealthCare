using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class TestGroupItemsUpdateDto
{
    public Guid Id { get; set; }
    [Required]
    public Guid TestGroupId { get; set; }

    [Required]
    [StringLength(TestGroupItemConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(TestGroupItemConsts.CodeMaxLength)]
    public string Code { get; set; } = null!;
    [Required]
    [StringLength(TestGroupItemConsts.TestTypeMaxLength)]
    public string TestType { get; set; } = null!;
    [StringLength(TestGroupItemConsts.DescriptionMaxLength)]
    public string? Description { get; set; } = null;
    [Required]
    public int TurnaroundTime { get; set; }
}
