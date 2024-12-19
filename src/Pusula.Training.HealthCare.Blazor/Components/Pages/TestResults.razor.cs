using Pusula.Training.HealthCare.TestProcesses;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class TestResults : IDisposable
    {
        private List<TestProcessWithNavigationPropertiesDto> TestResultsList = new();
        private SfGrid<TestProcessWithNavigationPropertiesDto>? TestProcessesGrid;
        private GetTestProcessesInput? TestProcessesFilter { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TestProcessState.Subscribe(OnStateChangeHandler);

            TestProcessesFilter = new GetTestProcessesInput();
            await GetTestResultsAsync();
        }
        public void Dispose()
        {
            TestProcessState.Unsubscribe(OnStateChangeHandler);
        }
        private void OnStateChangeHandler()
        {
            TestResultsList = TestProcessState.TestProcesses
                .Where(tp => tp.TestProcess!.Result.HasValue)
                .ToList();

            InvokeAsync(StateHasChanged); 
        }

        private async Task GetTestResultsAsync()
        {
            var result = await TestProcessesAppService.GetListWithNavigationPropertiesAsync(TestProcessesFilter!);
            var testResults = result.Items
                .Where(test => test.TestProcess!.Result.HasValue)
                .ToList();

            TestProcessState.SetTestProcesses(testResults);
        }

        private async Task OnPrintClick()
        {
            if (TestProcessesGrid != null)
            {
                await TestProcessesGrid.PrintAsync();
            }
        }

        private async Task OnInputChange(InputEventArgs args)
        {
            TestProcessesFilter!.FilterText = args.Value;
            await GetTestResultsAsync();
        }

    }
}
