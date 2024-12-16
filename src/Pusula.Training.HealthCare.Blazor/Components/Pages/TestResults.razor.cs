using Blazorise;
using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.TestProcesses;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class TestResults
    {
        private List<TestProcessDto> TestResultsList = new();
        private SfGrid<TestProcessDto>? TestProcessesGrid;
        private GetTestProcessesInput? TestProcessesFilter { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TestProcessesFilter = new GetTestProcessesInput();
            await GetTestResultsAsync();
        }

        private async Task GetTestResultsAsync()
        {
            var result = await TestProcessesAppService.GetListWithNavigationPropertiesAsync(TestProcessesFilter!);
            TestResultsList = result.Items
            .Where(test => test.Result.HasValue)
            .ToList();
        }

        private async Task OnPrintClick()
        {
            if (TestProcessesGrid != null)
            {
                await TestProcessesGrid.PrintAsync();
            }
        }
        #region Filter
        private async Task OnInputChange(InputEventArgs args)
        {
            TestProcessesFilter!.FilterText = args.Value;
            await GetTestResultsAsync();
        }

        #endregion
    }
}