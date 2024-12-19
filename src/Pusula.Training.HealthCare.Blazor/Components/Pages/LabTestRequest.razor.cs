using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroupItems;
using Pusula.Training.HealthCare.TestGroups;
using System.Collections.Generic;
using System;
using Syncfusion.Blazor.Grids;
using Volo.Abp.Application.Dtos;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor.Notifications;
using System.Threading.Tasks;
using Syncfusion.Blazor.Inputs;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.TestProcesses;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class LabTestRequest : IDisposable
{
    private List<TestGroupItemDto> TestGroupItemsList { get; set; } = new List<TestGroupItemDto>();
    private List<TestGroupDto> TestGroupsList { get; set; } = new List<TestGroupDto>();
    private List<TestProcessWithNavigationPropertiesDto> TestProcessesWithNavigationList = new();
    private List<TestProcessWithNavigationPropertiesDto> ApprovedTestProcesses { get; set; } = new();
    private IReadOnlyList<LookupDto<Guid>> TestGroupNamesCollection { get; set; } = Array.Empty<LookupDto<Guid>>();
    private GetTestGroupItemsInput? TestGroupItemsFilter { get; set; }
    private GetTestGroupsInput? TestGroupsFilter { get; set; }
    private GetTestProcessesInput? TestProcessesFilter { get; set; }

    private SfGrid<TestProcessWithNavigationPropertiesDto>? TestProcessesGrid;
    private SfGrid<TestProcessWithNavigationPropertiesDto>? TestResultsGrid;
    private List<TestProcessWithNavigationPropertiesDto> CompletedTestProcesses { get; set; } = new();
    private Guid? SelectedTestGroupId { get; set; } = null;
    private string? SelectedDescription { get; set; } = string.Empty;
    private Guid? SelectedTestProcessId { get; set; } = null;

    private SfToast? ToastObj;

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private long TotalCount;


    private SfDialog? PatientDetailsModal;
    private SfDialog? DeleteTestProcessDialog;
    private SfDialog? HistoryDialog;
    private SfDialog? DescriptionDialog;
    private bool _isInitialized = false;


    protected override async Task OnInitializedAsync()
    {
        TestProcessState.Subscribe(StateChangedHandler);


        if (LabRequestService.SelectedLabRequest == null || LabRequestService.SelectedLabRequest.Id == Guid.Empty)
        {
            NavigationManager.NavigateTo("/lab-protocols");
            return;
        }

        TestGroupsFilter = new GetTestGroupsInput();
        TestGroupItemsFilter = new GetTestGroupItemsInput();
        TestProcessesFilter = new GetTestProcessesInput();

        await LoadTestProcessesWithNavigationAsync();
        await LoadApprovedTestProcessesAsync();
        await GetTestGroupItemsAsync();
        await GetTestGroupsAsync();

        _isInitialized = true;
    }

    private void StateChangedHandler()
    {
        InvokeAsync(async () =>
        {
            if (TestProcessState.Refresh)
            {
                await LoadTestProcessesWithNavigationAsync();
                await LoadApprovedTestProcessesAsync();
                StateHasChanged();
            }
        });
    }

    public void Dispose()
    {
        TestProcessState.Unsubscribe(StateChangedHandler);
    }

    #region Click Events
    private async Task OnTestProcessesPrintClick()
    {
        if (TestProcessesGrid != null)
        {
            await TestProcessesGrid.PrintAsync();
        }
    }

    private async Task OnTestResultsPrintClick()
    {
        if (TestResultsGrid != null)
        {
            await TestResultsGrid.PrintAsync();
        }
    }
    private async Task OnAddClick(TestGroupItemDto testGroupItem)
    {
        await HandleError(async () =>
        {
            var newTestProcess = new TestProcessesCreateDto
            {
                LabRequestId = LabRequestService.SelectedLabRequest!.Id,
                TestGroupItemId = testGroupItem.Id,
                Status = TestProcessStates.Requested,
                Result = null,
                ResultDate = null
            };

            var createdProcess = await TestProcessesAppService.CreateAsync(newTestProcess);

            var result = await TestProcessesAppService.GetWithNavigationPropertiesAsync(createdProcess.Id);

            TestProcessState.AddOrUpdateTestProcess(result);

            if (LabRequestService.SelectedLabRequest!.Status != RequestStatusEnum.InProgress)
            {
                LabRequestService.SelectedLabRequest.Status = RequestStatusEnum.InProgress;

                await LabRequestsAppService.UpdateAsync(new LabRequestUpdateDto
                {
                    Id = LabRequestService.SelectedLabRequest.Id,
                    ProtocolId = LabRequestService.SelectedLabRequest.ProtocolId,
                    DoctorId = LabRequestService.SelectedLabRequest.DoctorId,
                    Date = LabRequestService.SelectedLabRequest.Date,
                    Description = LabRequestService.SelectedLabRequest.Description,
                    Status = RequestStatusEnum.InProgress
                });
            }
        });
    }

    private async Task ConfirmTestProcessDelete()
    {
        await HandleError(async () =>
        {
            if (SelectedTestProcessId.HasValue)
            {
                await TestProcessesAppService.DeleteAsync(SelectedTestProcessId.Value);

                TestProcessesWithNavigationList.RemoveAll(tp => tp.TestProcess!.Id == SelectedTestProcessId.Value);
                TestProcessState.SetTestProcesses(TestProcessesWithNavigationList);

                await CloseTestProcessDeleteModal();
                await ShowToast(TestGroupItemConsts.TestGroupItemDeletedMessage, true);
            }
        });
    }

    #endregion

    #region Get Actions

    private async Task LoadApprovedTestProcessesAsync()
    {
        if (LabRequestService.SelectedLabRequest == null)
            return;

        ApprovedTestProcesses = TestProcessState.TestProcesses
            .Where(tp => tp.TestProcess != null
                         && tp.TestProcess.LabRequestId == LabRequestService.SelectedLabRequest!.Id
                         && tp.TestProcess.Result.HasValue)
            .ToList();

        if (!ApprovedTestProcesses.Any())
        {
            var allProcesses = await TestProcessesAppService.GetByLabRequestIdAsync(LabRequestService.SelectedLabRequest!.Id);

            ApprovedTestProcesses = allProcesses
                .Where(tp => tp.TestProcess!.Result.HasValue)
                .ToList();

            TestProcessState.SetTestProcesses(allProcesses);
        }

        StateHasChanged();
    }

    private List<TestProcessWithNavigationPropertiesDto> GetApprovedTestProcessesFromState()
    {
        return TestProcessState.TestProcesses
            .Where(tp => tp.TestProcess != null
                         && tp.TestProcess.LabRequestId == LabRequestService.SelectedLabRequest!.Id
                         && tp.TestProcess.Result.HasValue)
            .ToList();
    }

    private async Task<List<TestProcessWithNavigationPropertiesDto>> LoadApprovedTestProcessesFromServiceAsync()
    {
        var allProcesses = await TestProcessesAppService.GetByLabRequestIdAsync(LabRequestService.SelectedLabRequest!.Id);

        var approvedProcesses = allProcesses
            .Where(tp => tp.TestProcess!.Result.HasValue)
            .ToList();

        TestProcessState.SetTestProcesses(allProcesses);

        return approvedProcesses;
    }



    private async Task LoadTestProcessesWithNavigationAsync()
    {
        var allTestProcesses = await TestProcessesAppService.GetByLabRequestIdAsync(LabRequestService.SelectedLabRequest!.Id);
        TestProcessesWithNavigationList = allTestProcesses.Where(tp => tp.TestProcess?.Status == TestProcessStates.Requested).ToList();
    }

    private async Task LoadLookupsAsync()
    {
        var input = new LookupRequestDto
        {
            MaxResultCount = PageSize,
            Filter = TestGroupItemsFilter!.FilterText
        };

        var result = await TestGroupsAppService.GetGroupNameLookupAsync(input);
        TestGroupNamesCollection = result.Items;

        if (TestGroupNamesCollection.Any())
        {
            TestGroupItemsFilter.TestGroupId = TestGroupNamesCollection.First().Id;
        }
    }

    private async Task GetTestGroupItemsAsync()
    {
        var result = await TestGroupItemsAppService.GetListAsync(TestGroupItemsFilter!);
        TestGroupItemsList = result.Items.ToList();
        TotalCount = result.TotalCount;
    }
    private async Task GetTestGroupsAsync()
    {
        var result = await TestGroupsAppService.GetListAsync(TestGroupsFilter!);
        TestGroupsList = result.Items.ToList();
        TotalCount = result.TotalCount;
    }
    private string GetTestGroupName(Guid testGroupId)
    {
        return TestGroupNamesCollection.FirstOrDefault(t => t.Id == testGroupId)?.DisplayName ?? "Unknown";
    }

    #endregion

    #region Modals
    private void OpenPatientDetailsModal()
    {
        PatientDetailsModal!.ShowAsync();
    }
    private async Task ClosePatientDetailsModal()
    {
        await PatientDetailsModal!.HideAsync();
    }
    private async Task OpenHistoryModal()
    {
        await LoadApprovedTestProcessesAsync();
        await HistoryDialog!.ShowAsync();
    }

    private async Task OpenDescriptionModal(string? description)
    {
        SelectedDescription = description ?? "Açýklama mevcut deðil.";
        await DescriptionDialog!.ShowAsync();
    }
    private async Task CloseDescriptionModal()
    {
        SelectedDescription = string.Empty;
        await DescriptionDialog!.HideAsync();
    }

    private async Task OpenTestProcessDeleteModal(Guid id)
    {
        SelectedTestProcessId = id;
        await DeleteTestProcessDialog!.ShowAsync();
    }
    private async Task CloseTestProcessDeleteModal()
    {
        SelectedTestProcessId = null;
        await DeleteTestProcessDialog!.HideAsync();
    }
    #endregion

    #region Filter
    private async Task SearchAsync(InputEventArgs args)
    {
        CurrentPage = 1;
        TestGroupItemsFilter!.FilterText = args.Value;
        await GetTestGroupItemsAsync();
    }
    private async Task FilterTestGroupItems(Guid groupId)
    {
        SelectedTestGroupId = groupId;

        var selectedGroup = TestGroupsList.FirstOrDefault(g => g.Id == groupId);
        var result = await TestGroupItemsAppService.GetListAsync(new GetTestGroupItemsInput
        {
            TestGroupId = groupId
        });

        TestGroupItemsList = result.Items.ToList();
    }

    #endregion

    #region Toast & Exception Controls

    public async Task HandleError(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch (UserFriendlyException ex)
        {
            await ShowToast(ex.Message, false);
        }
        catch (Exception)
        {
            await ShowToast("Bir hata oluþtu. Lütfen tekrar deneyin.", false);
        }
    }

    private async Task ShowToast(string message, bool isSuccess = true)
    {
        await ToastObj!.ShowAsync(new ToastModel
        {
            Content = message,
            CssClass = isSuccess ? "e-toast-success" : "e-toast-danger",
            Timeout = 3000,
            ShowCloseButton = true
        });
    }

    #endregion
}