using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroupItems;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Navigations;
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
        private IReadOnlyList<LookupDto<Guid>> TestGroupNamesCollection { get; set; } = Array.Empty<LookupDto<Guid>>();
        private GetTestGroupItemsInput Filter { get; set; }


        private TestGroupItemsCreateDto CreateDto = new();
        private TestGroupItemsUpdateDto UpdateDto = new();

        private SfGrid<TestGroupItemDto> DefaultGrid = null!;

        private Guid? SelectedTestGroupId { get; set; } = null;
        private Guid? SelectedTestGroupItemId;

        private bool IsUpdateModalVisible = false;
        private bool IsDeleteModalVisible = false;
        private bool IsCreateModalVisible = false;

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private long TotalCount;

        //Modal yönetimi
        private SfDialog CreateDialog;
        private SfDialog DeleteDialog;
        private SfDialog UpdateDialog;

        protected override async Task OnInitializedAsync()
        {
            Filter = new GetTestGroupItemsInput
            {
                FilterText = string.Empty,
                Name = string.Empty,
                Code = string.Empty,
                TestType = string.Empty,
                Description = string.Empty,
                TestGroupId = Guid.Empty,
                MaxResultCount = PageSize,
                SkipCount = 0
            };
            await LoadLookupsAsync();
            await GetTestGroupItemsAsync();
        }


        #region Get Actions
        private async Task LoadLookupsAsync()
        {
            var input = new LookupRequestDto
            {
                MaxResultCount = PageSize,
                Filter = Filter.FilterText
            };

            var result = await TestGroupsAppService.GetGroupNameLookupAsync(input);
            TestGroupNamesCollection = result.Items;

            if (TestGroupNamesCollection.Any())
            {
                Filter.TestGroupId = TestGroupNamesCollection.First().Id;
            }
        }

        private async Task GetTestGroupItemsAsync()
        {
            var input = new GetTestGroupItemsInput
            {
                FilterText = Filter.FilterText,
                Name = Filter.Name,
                Code = Filter.Code,
                TestType = Filter.TestType,
                Description = Filter.Description,
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                TestGroupId = SelectedTestGroupId ?? Guid.Empty
            };

            var result = await TestGroupItemsAppService.GetListAsync(input);
            TestGroupItemsList = result.Items.ToList();
            TotalCount = result.TotalCount;
        }

        private string GetTestGroupName(Guid testGroupId)
        {
            return TestGroupNamesCollection.FirstOrDefault(t => t.Id == testGroupId)?.DisplayName ?? "Unknown";
        }

        #endregion

        private async Task SaveNewItem()
        {
            await TestGroupItemsAppService.CreateAsync(CreateDto);
            await CloseCreateModal();
            await GetTestGroupItemsAsync();
        }

        private async Task ConfirmDelete()
        {
            if (SelectedTestGroupItemId.HasValue)
            {
                await TestGroupItemsAppService.DeleteAsync(SelectedTestGroupItemId.Value);
                await GetTestGroupItemsAsync();
            }
            await CloseDeleteModal();
        }

        //Modals
        private async Task OpenCreateModal()
        {
            CreateDto = new TestGroupItemsCreateDto();
            await CreateDialog.ShowAsync(); 
        }
        private async Task CloseCreateModal()
        {
            await CreateDialog.HideAsync(); 
        }
        private async Task OpenDeleteModal(Guid id)
        {
            SelectedTestGroupItemId = id;
            await DeleteDialog.ShowAsync(); 
        }

        private async Task CloseDeleteModal()
        {
            SelectedTestGroupItemId = null;
            await DeleteDialog.HideAsync(); 
        }

        private async Task OpenUpdateModal(TestGroupItemDto item)
        {
            UpdateDto = new TestGroupItemsUpdateDto
            {
                Id = item.Id,
                Name = item.Name,
                Code = item.Code,
                TestGroupId = item.TestGroupId,
                TestType = item.TestType,
                Description = item.Description,
                TurnaroundTime = item.TurnaroundTime
            };
            await UpdateDialog.ShowAsync(); 
        }

        private async Task CloseUpdateModal()
        {
            await UpdateDialog.HideAsync(); 
        }

        private async Task SaveChanges()
        {
            await TestGroupItemsAppService.UpdateAsync(UpdateDto);
            await CloseUpdateModal();
            await GetTestGroupItemsAsync();
        }

        //Search
        private async Task SearchAsync(InputEventArgs args)
        {
            CurrentPage = 1;
            Filter.FilterText = args.Value; 
            await GetTestGroupItemsAsync();
        }
    }
}