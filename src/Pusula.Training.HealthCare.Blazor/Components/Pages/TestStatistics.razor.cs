using Pusula.Training.HealthCare.TestProcesses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class TestStatistics
    {
        private List<TestCountDto> TestCounts = new();
        private List<TestGroupCountDto> TestGroupCounts = new();

        protected override async Task OnInitializedAsync()
        {
            await GetTestCountsAsync();
            await GetTestGroupCountsAsync();
        }

        private async Task GetTestCountsAsync()
        {
            var testCounts = await TestProcessesAppService.GetTestCountsAsync();
            TestCounts = testCounts.OrderByDescending(tc => tc.Count).ToList();

        }
        private async Task GetTestGroupCountsAsync()
        {
            var testGroupCounts = await TestProcessesAppService.GetTestGroupCountsAsync();
            TestGroupCounts = testGroupCounts.OrderByDescending(tc => tc.Count).ToList();
        }
    }
}