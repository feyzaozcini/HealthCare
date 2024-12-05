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
        private TestProcessesUpdateDto UpdateTestProcessDto = new();
        private List<LabRequestDto> InProgressLabRequests { get; set; } = new();
        private List<LabRequestDto> CompletedLabRequests { get; set; } = new();
        private GetTestProcessesInput? TestProcessesFilter { get; set; }
        private GetLabRequestsInput? LabRequestsFilter { get; set; }

        private LabRequestDto? SelectedLabRequest;
        private Guid? SelectedTestProcessId;

        private SfDialog? ResultDialog;

        protected override async Task OnInitializedAsync()
        {
            TestProcessesFilter = new GetTestProcessesInput();
            LabRequestsFilter = new GetLabRequestsInput();
            await LoadLabRequestsAsync();
        }

        #region GetActions
        private async Task LoadLabRequestsAsync()
        {
            var allLabRequests = await LabRequestsAppService.GetListWithNavigationPropertiesAsync(LabRequestsFilter!);
            InProgressLabRequests = allLabRequests.Items.Where(lr => lr.Status == RequestStatusEnum.InProgress).ToList();
            CompletedLabRequests = allLabRequests.Items.Where(lr => lr.Status == RequestStatusEnum.Completed).ToList();
        }

        #endregion

        #region Save
        private async Task SaveResult()
        {
            //Eðer TestProcess'in deðeri güncellendiyse veya eklendiyse her durumda Status'ü 'Approved', ResultDate'ini DateTime.Now olarak güncelle.
            var updateTasks = TestProcessesList
            .Select(async testProcess =>
            {
                var existingTestProcess = await TestProcessesAppService.GetAsync(testProcess.Id);
                if (testProcess.Result.HasValue &&
                    (!existingTestProcess.Result.HasValue || existingTestProcess.Result != testProcess.Result))
                {
                    await TestProcessesAppService.UpdateAsync(new TestProcessesUpdateDto
                    {
                        Id = testProcess.Id,
                        LabRequestId = testProcess.LabRequestId,
                        TestGroupItemId = testProcess.TestGroupItemId,
                        Status = TestProcessStates.Approved,
                        Result = testProcess.Result,
                        ResultDate = DateTime.Now
                    });
                }
            });

            await Task.WhenAll(updateTasks);

            //Eðer lab request'e ait tüm testlerin sonucu girildiyse, lab request'i completed olarak iþaretle.
            bool allResultsEntered = TestProcessesList.All(tp => tp.Result.HasValue);

            if (allResultsEntered && SelectedLabRequest != null)
            {
                SelectedLabRequest.Status = RequestStatusEnum.Completed;

                await LabRequestsAppService.UpdateAsync(new LabRequestUpdateDto
                {
                    Id = SelectedLabRequest.Id,
                    ProtocolId = SelectedLabRequest.ProtocolId,
                    DoctorId = SelectedLabRequest.DoctorId,
                    Date = SelectedLabRequest.Date,
                    Description = SelectedLabRequest.Description,
                    Status = SelectedLabRequest.Status
                });
            }

            await LoadLabRequestsAsync();
            await CloseResultDialog();
        }

        #endregion

        #region Modals
        private async Task OpenResultUpdateModal(LabRequestDto labRequest)
        {
            SelectedLabRequest = labRequest;
            TestProcessesList = await TestProcessesAppService.GetByLabRequestIdAsync(labRequest.Id);
            await ResultDialog!.ShowAsync();
        }

        private async Task CloseResultDialog()
        {
            TestProcessesList = new();
            SelectedLabRequest = null;
            await ResultDialog!.HideAsync();
        }

        #endregion

        #region Filter
        private async Task OnInputChange(InputEventArgs args)
        {
            LabRequestsFilter!.FilterText = args.Value;
            await LoadLabRequestsAsync();
        }

        #endregion
    }
}