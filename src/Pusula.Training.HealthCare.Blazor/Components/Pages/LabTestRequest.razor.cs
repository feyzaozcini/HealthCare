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

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class LabTestRequest
{
    private List<TestGroupItemDto> TestGroupItemsList { get; set; } = new List<TestGroupItemDto>();
    private GetTestGroupItemsInput? TestGroupItemsFilter { get; set; }
    private IReadOnlyList<LookupDto<Guid>> TestGroupNamesCollection { get; set; } = Array.Empty<LookupDto<Guid>>();
    private GetTestGroupsInput? TestGroupsFilter { get; set; }
    private List<TestGroupDto> TestGroupsList { get; set; } = new List<TestGroupDto>();
    private List<TestProcessesCreateDto> CreatedTestProcesses { get; set; } = new();
    private List<TestProcessDto> TestProcessesList = new();
    private LabRequestDto? LabRequest { get; set; }
    private string SelectedDescription = string.Empty;
    private SfGrid<TestProcessDto>? TestProcessesGrid;

    private Guid? SelectedTestGroupId { get; set; } = null;
    private Guid? SelectedTestProcessId { get; set; } = null;

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private long TotalCount;

    private SfDialog? DeleteTestProcessDialog;
    private bool _isInitialized = false;


    private SfDialog? DescriptionDialog;
    private DateTime[] FilterByDateRange = { DateTime.Today, DateTime.Today.AddDays(30) };

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
        await LoadTestProcessesAsync();
        await GetTestGroupItemsAsync();
        await GetTestGroupsAsync();
        await LoadLookupsAsync();

        _isInitialized = true; 
    }


    #region Click Events
    private async Task OnPrintClick()
    {
        if (TestProcessesGrid != null)
        {
            await TestProcessesGrid.PrintAsync();
        }
    }
    private async Task OnAddClick(TestGroupItemDto testGroupItem)
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

        await LoadTestProcessesAsync();

        if (TestProcessesGrid != null)
        {
            await TestProcessesGrid.Refresh();
        }
    }

    private void OnPlanningClick()
    {
        Console.WriteLine("Planlamaya týklandý!");
    }

    #endregion

    #region Get Actions
    private async Task LoadTestProcessesAsync()
    {
        TestProcessesList = await TestProcessesAppService.GetByLabRequestIdAsync(LabRequestService.SelectedLabRequest!.Id);
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
        var input = new GetTestGroupItemsInput
        {
            FilterText = TestGroupItemsFilter?.FilterText,
            Name = TestGroupItemsFilter?.Name,
            Code = TestGroupItemsFilter?.Code,
            TestType = TestGroupItemsFilter?.TestType,
            Description = TestGroupItemsFilter?.Description,
            MaxResultCount = TestGroupItemsFilter!.MaxResultCount,
            SkipCount = (CurrentPage - 1) * PageSize,
            TestGroupId = SelectedTestGroupId ?? Guid.Empty
        };

        var result = await TestGroupItemsAppService.GetListAsync(input);
        TestGroupItemsList = result.Items.ToList();
        TotalCount = result.TotalCount;
    }
    private async Task GetTestGroupsAsync()
    {
        var input = new GetTestGroupsInput
        {
            FilterText = TestGroupsFilter?.FilterText,
            Name = TestGroupsFilter?.Name,
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize
        };
        var result = await TestGroupsAppService.GetListAsync(input);
        TestGroupsList = result.Items.ToList();
        TotalCount = result.TotalCount;
    }
    private string GetTestGroupName(Guid testGroupId)
    {
        return TestGroupNamesCollection.FirstOrDefault(t => t.Id == testGroupId)?.DisplayName ?? "Unknown";
    }

    #endregion

    #region Modals
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

    private async Task ConfirmTestProcessDelete()
    {

        if (SelectedTestProcessId.HasValue)
        {
            await TestProcessesAppService.DeleteAsync(SelectedTestProcessId.Value);
            await LoadTestProcessesAsync();
            StateHasChanged();
        }
        await CloseTestProcessDeleteModal();
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
    private void DecreaseMonth()
    {
        FilterByDateRange[0] = FilterByDateRange[0].AddMonths(-1);
        FilterByDateRange[1] = FilterByDateRange[1].AddMonths(-1);
    }
    private void IncreaseMonth()
    {
        FilterByDateRange[0] = FilterByDateRange[0].AddMonths(1);
        FilterByDateRange[1] = FilterByDateRange[1].AddMonths(1);
    }

    #endregion

}