using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroupItems;
using Pusula.Training.HealthCare.TestGroups;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class TestGroups
    {
        private List<TestGroupItemDto> TestGroupItemsList { get; set; } = new List<TestGroupItemDto>();
        private List<TestGroupDto> TestGroupsList { get; set; } = new List<TestGroupDto>();
        private IReadOnlyList<LookupDto<Guid>> TestGroupNamesCollection { get; set; } = Array.Empty<LookupDto<Guid>>();
        private GetTestGroupItemsInput TestGroupItemsFilter { get; set; }
        private GetTestGroupsInput TestGroupsFilter { get; set; }


        private TestGroupItemsCreateDto CreateTestGroupItemsDto = new();
        private TestGroupItemsUpdateDto UpdateTestGroupItemsDto = new();
        private TestGroupsCreateDto CreateTestGroupsDto = new();
        private TestGroupsUpdateDto UpdateTestGroupsDto = new();

        private SfToast ToastObj;
        private string ErrorMessage = string.Empty;

        private SfGrid<TestGroupItemDto> DefaultGrid = null!;

        private Guid? SelectedTestGroupId { get; set; } = null;
        private Guid? SelectedTestGroupItemId;

        //private bool CanCreateTest { get; set; }
        //private bool CanEditTest { get; set; }
        //private bool CanDeleteTest { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private long TotalCount;

        //Modal yönetimi
        private SfDialog CreateTestGroupItemsDialog;
        private SfDialog CreateTestGroupsDialog;
        private SfDialog DeleteTestGroupItemsDialog;
        private SfDialog DeleteTestGroupsDialog;
        private SfDialog UpdateTestGroupItemsDialog;
        private SfDialog UpdateTestGroupsDialog;

        protected override async Task OnInitializedAsync()
        {
            //await SetPermissionsAsync();

            TestGroupsFilter = new GetTestGroupsInput();
            TestGroupItemsFilter = new GetTestGroupItemsInput();
            await LoadLookupsAsync();
            await GetTestGroupItemsAsync();
            await GetTestGroupsAsync();
        }


        #region Get Actions
        private async Task LoadLookupsAsync()
        {
            var input = new LookupRequestDto
            {
                MaxResultCount = PageSize,
                Filter = TestGroupItemsFilter.FilterText
            };

            var result = await TestGroupsAppService.GetGroupNameLookupAsync(input);
            TestGroupNamesCollection = result.Items;

            if (TestGroupNamesCollection.Any())
            {
                TestGroupItemsFilter.TestGroupId = TestGroupNamesCollection.First().Id;
            }
        }
        private async Task LoadTestGroupItems(Guid groupId)
        {
            // Test grubu ID'sine göre testleri getir
            SelectedTestGroupId = groupId;
            var result = await TestGroupItemsAppService.GetListAsync(new GetTestGroupItemsInput
            {
                TestGroupId = groupId
            });

            TestGroupItemsList = result.Items.ToList();
        }
        private async Task GetTestGroupItemsAsync()
        {
            var input = new GetTestGroupItemsInput
            {
                FilterText = TestGroupItemsFilter.FilterText,
                Name = TestGroupItemsFilter.Name,
                Code = TestGroupItemsFilter.Code,
                TestType = TestGroupItemsFilter.TestType,
                Description = TestGroupItemsFilter.Description,
                MaxResultCount = 20,
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
                FilterText = TestGroupsFilter.FilterText,
                Name = TestGroupsFilter.Name,
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
            await CreateTestGroupItemsDialog.ShowAsync(); 
        }
        private async Task OpenTestGroupsCreateModal()
        {
            CreateTestGroupsDto = new TestGroupsCreateDto();
            await CreateTestGroupsDialog.ShowAsync();
        }
  
        private async Task CloseTestGroupItemCreateModal()
        {
            await CreateTestGroupItemsDialog.HideAsync(); 
        }

        private async Task CloseTestGroupCreateModal()
        {
            await CreateTestGroupsDialog.HideAsync();
        }
        private async Task OpenTestGroupItemDeleteModal(Guid id)
        {
            SelectedTestGroupItemId = id;
            await DeleteTestGroupItemsDialog.ShowAsync(); 
        }

        private async Task OpenTestGroupDeleteModal(Guid id)
        {
            SelectedTestGroupId = id;
            await DeleteTestGroupsDialog.ShowAsync();
        }

        private async Task CloseTestGroupItemDeleteModal()
        {
            SelectedTestGroupItemId = null;
            await DeleteTestGroupItemsDialog.HideAsync(); 
        }

        private async Task CloseTestGroupDeleteModal()
        {
            SelectedTestGroupId = null;
            await DeleteTestGroupsDialog.HideAsync();
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
            await UpdateTestGroupItemsDialog.ShowAsync(); 
        }
        private async Task OpenTestGroupUpdateModal(TestGroupDto item)
        {
            UpdateTestGroupsDto = new TestGroupsUpdateDto
            {
                Id = item.Id,
                Name = item.Name,
            };
            await UpdateTestGroupsDialog.ShowAsync();
        }
        private async Task CloseTestGroupItemUpdateModal()
        {
            UpdateTestGroupItemsDto = new TestGroupItemsUpdateDto();
            await UpdateTestGroupItemsDialog.HideAsync();
        }
        private async Task CloseTestGroupUpdateModal()
        {
            UpdateTestGroupsDto = new TestGroupsUpdateDto();
            await UpdateTestGroupsDialog.HideAsync();
        }

        #endregion

        #region Save Changes
        //Deðiþiklikleri Kaydetme
        private async Task UpdateTestGroupItem()
        {
            await TestGroupItemsAppService.UpdateAsync(UpdateTestGroupItemsDto);
            await CloseTestGroupItemUpdateModal();
            await GetTestGroupItemsAsync();
        }
        private async Task UpdateTestGroup()
        {
            await TestGroupsAppService.UpdateAsync(UpdateTestGroupsDto);
            await CloseTestGroupUpdateModal();
            await GetTestGroupsAsync();
        }
        private async Task AddTestGroupItem()
        {
            await TestGroupItemsAppService.CreateAsync(CreateTestGroupItemsDto);
            await CloseTestGroupItemCreateModal();
            await GetTestGroupItemsAsync();
        }

        private async Task AddTestGroup()
        {
            await TestGroupsAppService.CreateAsync(CreateTestGroupsDto);
            await CloseTestGroupCreateModal();
            await GetTestGroupsAsync();
        }
        private async Task ConfirmTestGroupItemDelete()
        {
            if (SelectedTestGroupItemId.HasValue)
            {
                await TestGroupItemsAppService.DeleteAsync(SelectedTestGroupItemId.Value);
                await GetTestGroupItemsAsync();
            }
            await CloseTestGroupItemDeleteModal();
        }

        private async Task ConfirmTestGroupDelete()
        {
            if (SelectedTestGroupId.HasValue)
            {
                await TestGroupsAppService.DeleteAsync(SelectedTestGroupId.Value);
                await GetTestGroupsAsync();
            }
            await CloseTestGroupDeleteModal();
        }

        #endregion

        #region Search
        //Search
        private async Task SearchAsync(InputEventArgs args)
        {
            CurrentPage = 1;
            TestGroupItemsFilter.FilterText = args.Value; 
            await GetTestGroupItemsAsync();
        }

        #endregion


        #region Toast
        private async Task HandleValidSubmit()
        {
            Console.WriteLine("Ýþlem Baþarýlý!");
        }

        private async Task HandleInvalidSubmit(EditContext editContext)
        {
            var messages = editContext.GetValidationMessages().ToList();
            if (messages.Any())
            {
                ErrorMessage = string.Join(", ", messages);
                await ToastObj.ShowAsync();
            }
        }

        #endregion
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
}