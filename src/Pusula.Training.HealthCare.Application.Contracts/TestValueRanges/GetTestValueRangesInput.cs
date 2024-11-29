using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestValueRanges;

public class GetTestValueRangesInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public Guid? TestGroupItemId { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public TestUnitTypes? Unit { get; set; }

    public GetTestValueRangesInput()
    {
    }

    public GetTestValueRangesInput(string? filterText, Guid? testGroupItemId, decimal? minValue, decimal? maxValue, TestUnitTypes? unit)
    {
        FilterText = filterText;
        TestGroupItemId = testGroupItemId;
        MinValue = minValue;
        MaxValue = maxValue;
        Unit = unit;
    }
}
