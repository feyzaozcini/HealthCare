using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.TestProcesses;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class LaborerBacklog
    {
        private List<TestProcessDto> TestProcessesList { get; set; } = new();
        private List<LabRequestDto> LabRequestsList { get; set; } = new();
        private LabRequestDto? SelectedLabRequest;

        private TestProcessesUpdateDto UpdateTestProcessDto = new ();
        private GetTestProcessesInput? TestProcessesFilter { get; set; }
        private GetLabRequestsInput? LabRequestsFilter { get; set; }
        private SfDialog? ResultDialog;
        private Guid? SelectedTestProcessId;

        protected override async Task OnInitializedAsync()
        {
            TestProcessesFilter = new GetTestProcessesInput();
            LabRequestsFilter = new GetLabRequestsInput();
            await GetLabRequestsAsync();
        }

        private async Task GetLabRequestsAsync()
        {
            var result = await LabRequestsAppService.GetListWithNavigationPropertiesAsync(LabRequestsFilter!);
            LabRequestsList = result.Items.ToList();
        }
        private async Task GetTestProcessesAsync()
        {
            TestProcessesList = await TestProcessesAppService.GetByLabRequestIdAsync(SelectedLabRequest!.Id);
        }
        private async Task SaveResult()
        {
            foreach (var testProcess in TestProcessesList)
            {
                await TestProcessesAppService.UpdateAsync(new TestProcessesUpdateDto
                {
                    Id = testProcess.Id,
                    LabRequestId = testProcess.LabRequestId,
                    TestGroupItemId = testProcess.TestGroupItemId,
                    Status = testProcess.Status,
                    Result = testProcess.Result,
                    ResultDate = DateTime.Now
                });
            }

            await GetLabRequestsAsync(); 
            await CloseResultDialog(); 
        }

        private async Task OpenResultUpdateModal(LabRequestDto labRequest)
        {
            SelectedLabRequest = labRequest;
            await GetTestProcessesAsync();
            await ResultDialog!.ShowAsync();
        }

        private async Task CloseResultDialog()
        {
            TestProcessesList = new();
            SelectedLabRequest = null;
            await ResultDialog!.HideAsync();
        }

        private async Task OnInputChange(InputEventArgs args)
        {
            LabRequestsFilter!.FilterText = args.Value;
            await GetLabRequestsAsync();
        }
    }
}