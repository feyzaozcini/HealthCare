using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroupItems;
using Pusula.Training.HealthCare.TestGroups;
using Syncfusion.Blazor.DataForm;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class TestGroups
{
    private List<TestGroupItemDto> TestGroupItemsList { get; set; } = new List<TestGroupItemDto>();
    private List<TestGroupDto> TestGroupsList { get; set; } = new List<TestGroupDto>();
    private IReadOnlyList<LookupDto<Guid>> TestGroupNamesCollection { get; set; } = Array.Empty<LookupDto<Guid>>();
    private GetTestGroupItemsInput? TestGroupItemsFilter { get; set; }
    private GetTestGroupsInput? TestGroupsFilter { get; set; }

    SfGrid<TestGroupItemDto> Grid;
    private TestGroupItemsCreateDto CreateTestGroupItemsDto = new();
    private TestGroupItemsUpdateDto UpdateTestGroupItemsDto = new();
    private TestGroupsCreateDto CreateTestGroupsDto = new();
    private TestGroupsUpdateDto UpdateTestGroupsDto = new();

    private SfToast? ToastObj;
    private string ErrorMessage = string.Empty;
    private SfDataForm? DataForm;


    private string SelectedDescription = string.Empty;
    private Guid? SelectedTestGroupId { get; set; } = null;
    private Guid? SelectedTestGroupItemId;

    //private bool CanCreateTest { get; set; }
    //private bool CanEditTest { get; set; }
    //private bool CanDeleteTest { get; set; }

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private long TotalCount;
    SfTextBox TextBoxSearchObj { get; set; }

    //Modal yönetimi
    private SfDialog? CreateTestGroupItemsDialog;
    private SfDialog? CreateTestGroupsDialog;
    private SfDialog? DeleteTestGroupItemsDialog;
    private SfDialog? DeleteTestGroupsDialog;
    private SfDialog? UpdateTestGroupItemsDialog;
    private SfDialog? UpdateTestGroupsDialog;
    private SfDialog? DescriptionDialog;

    protected override async Task OnInitializedAsync()
    {
        //await SetPermissionsAsync();

        TestGroupsFilter = new GetTestGroupsInput();
        TestGroupItemsFilter = new GetTestGroupItemsInput();
        await LoadLookupsAsync();
        await GetTestGroupItemsAsync();
        await GetTestGroupsAsync();
        ToastObj ??= new SfToast();
    }

    #region Get Actions
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

    private async Task LoadTestGroupItemsAsync()
    {
        var result = await TestGroupItemsAppService.GetListAsync(new GetTestGroupItemsInput());
        TestGroupItemsList = result.Items.ToList();
    }

    private async Task LoadTestGroupsAsync()
    {
        var result = await TestGroupsAppService.GetListAsync(new GetTestGroupsInput());
        TestGroupsList = result.Items.ToList();
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
    private async Task OpenTestGroupItemsCreateModal()
    {
        CreateTestGroupItemsDto = new TestGroupItemsCreateDto();
        await CreateTestGroupItemsDialog!.ShowAsync();
    }
    private async Task OpenTestGroupsCreateModal()
    {
        CreateTestGroupsDto = new TestGroupsCreateDto();
        await CreateTestGroupsDialog!.ShowAsync();
    }

    private async Task CloseTestGroupItemCreateModal()
    {
        await CreateTestGroupItemsDialog!.HideAsync();
    }

    private async Task CloseTestGroupCreateModal()
    {
        await CreateTestGroupsDialog!.HideAsync();
    }
    private async Task OpenTestGroupItemDeleteModal(Guid id)
    {
        SelectedTestGroupItemId = id;
        await DeleteTestGroupItemsDialog!.ShowAsync();
    }

    private async Task OpenTestGroupDeleteModal(Guid id)
    {
        SelectedTestGroupId = id;
        await DeleteTestGroupsDialog!.ShowAsync();
    }

    private async Task CloseTestGroupItemDeleteModal()
    {
        SelectedTestGroupItemId = null;
        await DeleteTestGroupItemsDialog!.HideAsync();
    }

    private async Task CloseTestGroupDeleteModal()
    {
        SelectedTestGroupId = null;
        await DeleteTestGroupsDialog!.HideAsync();
    }
    private async Task OpenTestGroupItemUpdateModal(TestGroupItemDto item)
    {
        UpdateTestGroupItemsDto = new TestGroupItemsUpdateDto
        {
            Id = item.Id,
            Name = item.Name,
            Code = item.Code,
            TestGroupId = item.TestGroupId,
            TestType = item.TestType,
            Description = item.Description,
            TurnaroundTime = item.TurnaroundTime
        };
        await UpdateTestGroupItemsDialog!.ShowAsync();
    }
    private async Task OpenTestGroupUpdateModal(TestGroupDto item)
    {
        UpdateTestGroupsDto = new TestGroupsUpdateDto
        {
            Id = item.Id,
            Name = item.Name,
        };
        await UpdateTestGroupsDialog!.ShowAsync();
    }
    private async Task CloseTestGroupItemUpdateModal()
    {
        UpdateTestGroupItemsDto = new TestGroupItemsUpdateDto();
        await UpdateTestGroupItemsDialog!.HideAsync();
    }
    private async Task CloseTestGroupUpdateModal()
    {
        UpdateTestGroupsDto = new TestGroupsUpdateDto();
        await UpdateTestGroupsDialog!.HideAsync();
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
    #endregion

    #region Save Changes
    private async Task UpdateTestGroupItem()
    {
        await HandleError(async () =>
        {
            await TestGroupItemsAppService.UpdateAsync(UpdateTestGroupItemsDto);
            await CloseTestGroupItemUpdateModal();
            await GetTestGroupItemsAsync();
            await ShowToast(TestGroupItemConsts.TestSuccessfullyUpdated, true);
        });
    }
    private async Task UpdateTestGroup()
    {
        await HandleError(async () =>
        {
            await TestGroupsAppService.UpdateAsync(UpdateTestGroupsDto);
            await GetTestGroupsAsync();
            await CloseTestGroupUpdateModal();
            await ShowToast(TestGroupConsts.TestGroupSuccessfullyUpdated, true);
        });
    }
    private async Task AddTestGroupItem()
    {
        await HandleError(async () =>
        {
            await TestGroupItemsAppService.CreateAsync(CreateTestGroupItemsDto);
            await LoadTestGroupItemsAsync();
            await CloseTestGroupItemCreateModal();
            await GetTestGroupItemsAsync(); 
            await ShowToast(TestGroupItemConsts.TestSuccessfullyCreated, true);
        });
    }
    private async Task AddTestGroup()
    {
        await HandleError(async () =>
        {
            await TestGroupsAppService.CreateAsync(CreateTestGroupsDto);
            await LoadTestGroupsAsync();
            await LoadLookupsAsync();
            StateHasChanged();
            await CloseTestGroupCreateModal();
            await GetTestGroupsAsync();
            await ShowToast(TestGroupConsts.TestGroupSuccessfullyCreated, true);
        });
    }
    private async Task ConfirmTestGroupItemDelete()
    {
        await HandleError(async () =>
        {
            if (SelectedTestGroupItemId.HasValue)
            {
                await TestGroupItemsAppService.DeleteAsync(SelectedTestGroupItemId.Value);
                await GetTestGroupItemsAsync();
            }
            await CloseTestGroupItemDeleteModal();
            await ShowToast(TestGroupItemConsts.TestGroupItemDeletedMessage, true);
        });
    }
    private async Task ConfirmTestGroupDelete()
    {
        await HandleError(async () =>
        {
            if (SelectedTestGroupId.HasValue)
            {
                await TestGroupsAppService.DeleteAsync(SelectedTestGroupId.Value);
                await GetTestGroupsAsync();
                await LoadTestGroupsAsync();
                await LoadLookupsAsync();
                StateHasChanged();
            }
            await CloseTestGroupDeleteModal();
            await ShowToast(TestGroupConsts.TestGroupDeletedMessage, true);
        });
    }

    #endregion

    #region Search
    private async Task SearchAsync(InputEventArgs args)
    {
        CurrentPage = 1;
        TestGroupItemsFilter!.FilterText = args.Value;
        await GetTestGroupItemsAsync();
    }


    //Seçili test grubunun elemanlarýný göstermek için.
    private async Task FilterTestGroupItems(Guid groupId)
    {
        SelectedTestGroupId = groupId;
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

    private async void AddDateIcon()
    {
        if (TextBoxSearchObj != null)
        {
            //Add icon to the TextBox
            await TextBoxSearchObj.AddIconAsync("append", "e-icons e-search-icon");
        }
    }
    //#region Permission
    ////Yetkilendirme
    //private async Task SetPermissionsAsync()
    //{
    //    CanCreateTest = await AuthorizationService.IsGrantedAsync(HealthCarePermissions.TestGroupItems.Create);
    //    CanEditTest = await AuthorizationService.IsGrantedAsync(HealthCarePermissions.TestGroupItems.Edit);
    //    CanDeleteTest = await AuthorizationService.IsGrantedAsync(HealthCarePermissions.TestGroupItems.Delete);
    //}
    //#endregion
}