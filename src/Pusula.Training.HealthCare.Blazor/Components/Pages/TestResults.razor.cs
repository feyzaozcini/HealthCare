using Pusula.Training.HealthCare.TestProcesses;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        //Belirlenen kurala göre row background-color'ýný güncelleme.
        public void RowBound(RowDataBoundEventArgs<TestProcessDto> args)
        {
            if (args.Data.Result.HasValue)
            {
                var result = args.Data.Result.Value;
                var minValue = args.Data.TestMinValue;
                var maxValue = args.Data.TestMaxValue;

                if (result < minValue || result > maxValue)
                {
                    args.Row.AddClass(new string[] { "bg-danger" });
                }
                else
                {
                    args.Row.AddClass(new string[] { "bg-success" });
                }
            }
        }
        private async Task OnPrintClick()
        {
            if (TestProcessesGrid != null)
            {
                await TestProcessesGrid.PrintAsync();
            }
        }
        #region Filter
        //private async Task OnInputChange(InputEventArgs args)
        //{
        //    TestProcessesFilter!.FilterText = args.Value;
        //    await GetTestResultsAsync();
        //}

        #endregion
    }
}