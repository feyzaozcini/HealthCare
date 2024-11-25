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

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class LabTestRequest
{
    private List<TestGroupItemDto> TestGroupItemsList { get; set; } = new List<TestGroupItemDto>();
    private List<TestGroupDto> TestGroupsList { get; set; } = new List<TestGroupDto>();
    private IReadOnlyList<LookupDto<Guid>> TestGroupNamesCollection { get; set; } = Array.Empty<LookupDto<Guid>>();
    private GetTestGroupItemsInput? TestGroupItemsFilter { get; set; }
    private GetTestGroupsInput? TestGroupsFilter { get; set; }
    private List<object> GridData = new() { new object() };
    private List<string> OrderTypes = new List<string> { "Laboratuvar", "Patoloji", "Ameliyat" };


    private string SelectedDescription = string.Empty;
    private Guid? SelectedTestGroupId { get; set; } = null;
    private string SelectedOrderType { get; set; } = string.Empty;
    private string SelectedGroupName { get; set; } = "Laboratuvar";
    private List<Guid> SelectedIds = new List<Guid>();

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private long TotalCount;

    private SfDialog? DescriptionDialog;

    private DateTime[] FilterByDateRange = { DateTime.Today, DateTime.Today.AddDays(30) };

    
    protected override async Task OnInitializedAsync()
    {
        TestGroupsFilter = new GetTestGroupsInput();
        TestGroupItemsFilter = new GetTestGroupItemsInput();
        await GetTestGroupItemsAsync();
        await GetTestGroupsAsync();
        await LoadLookupsAsync();
        SelectedIds = new List<Guid> { TestGroupItemsList[0].Id };
    }
    private void OnPrintClick()
    {
        Console.WriteLine("Ýstem Yazdýr týklandý.");

    }
    private void OnAddClick()
    {
        Console.WriteLine("Test eklendi!");
    }
    private void OnPlanningClick()
    {
        Console.WriteLine("Planlamaya týklandý!");
    }
    private void OnCheckboxChanged(bool isChecked, Guid testId)
    {
        if (isChecked)
        {
            if (!SelectedIds.Contains(testId))
            {
                SelectedIds.Add(testId);
            }
        }
        else
        {
            SelectedIds.Remove(testId);
        }
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
        SelectedGroupName = selectedGroup?.Name ?? "Laboratuvar";

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
}