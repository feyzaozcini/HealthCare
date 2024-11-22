using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.TestGroups;

public class TestGroupsUpdateDto
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(TestGroupConsts.NameMaxLength, MinimumLength = TestGroupConsts.NameMinLength)]
    public string Name { get; set; } = null!;

}
