using System;

namespace Pusula.Training.HealthCare.TestGroups;

public class TestGroupsDeletedDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Message { get; set; }
}
