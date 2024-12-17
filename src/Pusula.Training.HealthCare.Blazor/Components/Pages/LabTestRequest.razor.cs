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

public partial class LabTestRequest
{
    private List<TestGroupItemDto> TestGroupItemsList { get; set; } = new List<TestGroupItemDto>();
    private List<TestGroupDto> TestGroupsList { get; set; } = new List<TestGroupDto>();
    private List<TestProcessDto> TestProcessesList = new();
    private List<TestProcessDto> ApprovedTestProcesses { get; set; } = new();
    private IReadOnlyList<LookupDto<Guid>> TestGroupNamesCollection { get; set; } = Array.Empty<LookupDto<Guid>>();
    private GetTestGroupItemsInput? TestGroupItemsFilter { get; set; }
    private GetTestGroupsInput? TestGroupsFilter { get; set; }
    private GetTestProcessesInput? TestProcessesFilter { get; set; }

    private SfGrid<TestProcessDto>? TestProcessesGrid;
    private SfGrid<TestProcessDto>? TestResultsGrid;
    private List<TestProcessDto> CompletedTestProcesses { get; set; } = new();
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
        if (LabRequestService.SelectedLabRequest == null || LabRequestService.SelectedLabRequest.Id == Guid.Empty)
        {
            NavigationManager.NavigateTo("/lab-protocols");
            return;
        }

        // LabRequest mevcutsa gerekli iþlemleri yap
        TestGroupsFilter = new GetTestGroupsInput();
        TestGroupItemsFilter = new GetTestGroupItemsInput();
        TestProcessesFilter = new GetTestProcessesInput();
        await LoadTestProcessesAsync();
        await GetTestGroupItemsAsync();
        await GetTestGroupsAsync();
        await LoadLookupsAsync();
        await LoadApprovedTestProcessesAsync();
        StateHasChanged();

        _isInitialized = true;
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

            await TestProcessesAppService.CreateAsync(newTestProcess);
 
            var labRequest = await LabRequestsAppService.GetAsync(LabRequestService.SelectedLabRequest.Id);
            if (labRequest.Status == RequestStatusEnum.Completed)
            {
                labRequest.Status = RequestStatusEnum.InProgress;

                await LabRequestsAppService.UpdateAsync(new LabRequestUpdateDto
                {
                    Id = labRequest.Id,
                    ProtocolId = labRequest.ProtocolId,
                    DoctorId = labRequest.DoctorId,
                    Date = labRequest.Date,
                    Description = labRequest.Description,
                    Status = labRequest.Status
                });
            }

            await LoadTestProcessesAsync();

            if (TestProcessesGrid != null)
            {
                await TestProcessesGrid.Refresh();
            }

            await ShowToast(TestGroupItemConsts.TestSuccessfullyCreated, true);
        });
    }

    private async Task ConfirmTestProcessDelete()
    {
        await HandleError(async () =>
        {
            if (SelectedTestProcessId.HasValue)
            {
                await TestProcessesAppService.DeleteAsync(SelectedTestProcessId.Value);
                await LoadTestProcessesAsync();
                StateHasChanged();
            }
            await CloseTestProcessDeleteModal();
            await ShowToast(TestGroupItemConsts.TestGroupItemDeletedMessage, true);
        });
    }
    #endregion

    #region Get Actions

    private async Task LoadApprovedTestProcessesAsync()
    {
        var allTestProcesses = await TestProcessesAppService.GetByLabRequestIdAsync(LabRequestService.SelectedLabRequest!.Id);

        ApprovedTestProcesses = allTestProcesses.Where(tp => tp.Status == TestProcessStates.Approved).ToList();
    }

    private async Task LoadTestProcessesAsync()
    {
        var allTestProcesses = await TestProcessesAppService.GetByLabRequestIdAsync(LabRequestService.SelectedLabRequest!.Id);
        TestProcessesList = allTestProcesses.Where(tp => tp.Status == TestProcessStates.Requested).ToList();
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
}