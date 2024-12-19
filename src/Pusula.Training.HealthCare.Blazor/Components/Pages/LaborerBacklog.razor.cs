using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.TestProcesses;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class LaborerBacklog : IDisposable
    {
        private List<TestProcessWithNavigationPropertiesDto> TestProcessesWithNavigationList { get; set; } = new();
        private List<LabRequestDto> LabRequestsList { get; set; } = new();
        private List<TestProcessWithNavigationPropertiesDto> SelectedTestProcessesList { get; set; } = new();
        private List<LabRequestDto> InProgressLabRequests { get; set; } = new();
        private List<LabRequestDto> CompletedLabRequests { get; set; } = new();
        private GetTestProcessesInput? TestProcessesFilter { get; set; }
        private GetLabRequestsInput? LabRequestsFilter { get; set; }

        private LabRequestDto? SelectedLabRequest;
        private SfDialog? ResultDialog;

        protected override async Task OnInitializedAsync()
        {
            TestProcessState.Subscribe(StateChangedHandler);

            TestProcessesFilter = new GetTestProcessesInput();
            LabRequestsFilter = new GetLabRequestsInput();

            await LoadLabRequestsAsync();
        }

        private void StateChangedHandler()
        {
            InvokeAsync(async () =>
            {
                if (TestProcessState.Refresh)
                {
                    await LoadLabRequestsAsync();
                }
            });
        }

        public void Dispose()
        {
            TestProcessState.Unsubscribe(StateChangedHandler);
        }

        #region GetActions
        private async Task LoadLabRequestsAsync()
        {
            var allLabRequests = await FetchTestProcessesByLabRequestIdAsync();

            InProgressLabRequests = GetInProgressLabRequests(allLabRequests);
            CompletedLabRequests = GetCompletedLabRequests(allLabRequests);

            await InvokeAsync(StateHasChanged);
        }


        #endregion

        #region Save Methods

        private async Task SaveResult()
        {
            var updateTasks = SelectedTestProcessesList
                .Where(tp => tp.TestProcess != null && tp.TestProcess.Result.HasValue)
                .Select(async testProcess =>
                {
                    var updateDto = await CreateUpdateDto(testProcess);
                    var updatedProcess = await TestProcessesAppService.UpdateAsync(updateDto);

                    var result = await TestProcessesAppService.GetWithNavigationPropertiesAsync(updatedProcess.Id);
                    TestProcessState.AddOrUpdateTestProcess(result);
                });

            await Task.WhenAll(updateTasks);

            if (SelectedLabRequest != null)
            {
                await UpdateLabRequestStatusAsync(SelectedLabRequest.Id);
            }

            TestProcessState.NotifyStateChanged();

            await CloseResultDialog();
        }


        private async Task UpdateTestProcessesAsync()
        {
            var updateTasks = TestProcessesWithNavigationList
                .Where(tp => tp.TestProcess != null && tp.TestProcess.Result.HasValue)
                .Select(async testProcess =>
                {
                    var updateDto = await CreateUpdateDto(testProcess);
                    var updatedProcess = await TestProcessesAppService.UpdateAsync(updateDto);

                    testProcess.TestProcess.Result = updatedProcess.Result;
                    testProcess.TestProcess.Status = updatedProcess.Status;
                });

            TestProcessState.SetTestProcesses(TestProcessesWithNavigationList);
            await Task.WhenAll(updateTasks);

            await InvokeAsync(StateHasChanged);
        }


        private async Task UpdateLabRequestStatusAsync(Guid labRequestId)
        {
            bool allResultsEntered = TestProcessState.TestProcesses
                .Where(tp => tp.TestProcess!.LabRequestId == labRequestId)
                .All(tp => tp.TestProcess!.Result.HasValue);

            var relatedLabRequest = TestProcessState.TestProcesses
                .FirstOrDefault(tp => tp.TestProcess!.LabRequestId == labRequestId)?.LabRequest;

            if (relatedLabRequest != null)
            {
                relatedLabRequest.Status = allResultsEntered ? RequestStatusEnum.Completed : RequestStatusEnum.InProgress;

                await LabRequestsAppService.UpdateAsync(new LabRequestUpdateDto
                {
                    Id = relatedLabRequest.Id,
                    ProtocolId = relatedLabRequest.ProtocolId,
                    DoctorId = relatedLabRequest.DoctorId,
                    Date = relatedLabRequest.Date,
                    Description = relatedLabRequest.Description,
                    Status = relatedLabRequest.Status
                });

                TestProcessState.NotifyStateChanged();
            }
        }


        #endregion

        #region Modals
        private async Task OpenResultUpdateModal(LabRequestDto labRequest)
        {
            SelectedLabRequest = labRequest;
            SelectedTestProcessesList = await GetTestProcessesByLabRequestIdAsync(labRequest.Id);
            await ResultDialog!.ShowAsync();
        }

        private async Task<List<TestProcessWithNavigationPropertiesDto>> GetTestProcessesByLabRequestIdAsync(Guid labRequestId)
        {
            return await TestProcessesAppService.GetByLabRequestIdAsync(labRequestId);
        }


        private async Task CloseResultDialog()
        {
            SelectedTestProcessesList.Clear();
            SelectedLabRequest = null;

            if (ResultDialog != null)
            {
                await ResultDialog.HideAsync();
            }
        }

        #endregion

        #region Filter
        private async Task OnInputChange(InputEventArgs args)
        {
            LabRequestsFilter!.FilterText = args.Value;
            await LoadLabRequestsAsync();
        }


        #endregion

        #region Helpers

        private Task<TestProcessesUpdateDto> CreateUpdateDto(TestProcessWithNavigationPropertiesDto testProcess)
        {
            var updateDto = new TestProcessesUpdateDto
            {
                Id = testProcess.TestProcess!.Id,
                LabRequestId = testProcess.TestProcess.LabRequestId,
                TestGroupItemId = testProcess.TestProcess.TestGroupItemId,
                Status = TestProcessStates.Approved,
                Result = testProcess.TestProcess.Result,
                ResultDate = DateTime.Now
            };

            return Task.FromResult(updateDto);
        }

        //Belirli bir lab isteði için iliþkili test süreçlerini API'den çekmek.
        private async Task<List<LabRequestDto>> FetchTestProcessesByLabRequestIdAsync()
        {
            var allLabRequests = await LabRequestsAppService.GetListWithNavigationPropertiesAsync(LabRequestsFilter!);
            return allLabRequests.Items.ToList();
        }

        //Tamamlanmamýþ Lab tetkik istemlerini filtrelemek.
        private List<LabRequestDto> GetInProgressLabRequests(List<LabRequestDto> allLabRequests)
        {
            return allLabRequests
                .Where(lr => TestProcessState.TestProcesses
                    .Any(tp => tp.TestProcess!.LabRequestId == lr.Id && !tp.TestProcess!.Result.HasValue))
                .ToList();
        }

        //Tamamlanmýþ Lab tetkik istemlerini filtrelemek.
        private List<LabRequestDto> GetCompletedLabRequests(List<LabRequestDto> allLabRequests)
        {
            return allLabRequests
                .Where(lr => TestProcessState.TestProcesses
                    .Where(tp => tp.TestProcess!.LabRequestId == lr.Id)
                    .All(tp => tp.TestProcess!.Result.HasValue))
                .ToList();
        }
        #endregion
    }
}
