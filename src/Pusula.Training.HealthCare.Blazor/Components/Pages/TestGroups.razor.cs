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
        private IReadOnlyList<LookupDto<Guid>> FilteredTestGroupNamesCollection { get; set; } = Array.Empty<LookupDto<Guid>>();

        private TestGroupItemsCreateDto CreateDto = new();
        private TestGroupItemsUpdateDto UpdateDto = new();

        private SfGrid<TestGroupItemDto> DefaultGrid = null!;

        private Guid? SelectedTestGroupId { get; set; } = null;
        private Guid? SelectedTestGroupItemId;

        private bool IsUpdateModalVisible = false;
        private bool IsDeleteModalVisible = false;
        private bool IsCreateModalVisible = false;

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private string CurrentSorting { get; set; } = string.Empty;
        private int CurrentPage { get; set; } = 1;
        private long TotalCount;

        SfSidebar sidebarObj;
        private string SearchQuery { get; set; } = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            await LoadLookupsAsync();
            await GetTestGroupItemsAsync();
        }


        #region Get Actions
        private async Task LoadLookupsAsync()
        {
            var input = new LookupRequestDto
            {
                MaxResultCount = PageSize,
                Filter = null
            };

            var result = await TestGroupsAppService.GetGroupNameLookupAsync(input);
            TestGroupNamesCollection = result.Items;
        }

        private async Task GetTestGroupItemsAsync()
        {
            var input = new GetTestGroupItemsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                TestGroupId = SelectedTestGroupId ?? Guid.Empty
            };

            var result = await TestGroupItemsAppService.GetListAsync(input);
            TestGroupItemsList = result.Items.ToList();
        }

        private string GetTestGroupName(Guid testGroupId)
        {
            return TestGroupNamesCollection.FirstOrDefault(t => t.Id == testGroupId)?.DisplayName ?? "Unknown";
        }

        #endregion

        private void OnInput(InputEventArgs args)
        {
            CurrentSorting = args.Value?.ToString() ?? string.Empty;
            _ = GetTestGroupItemsAsync();
        }

        private async Task SaveNewItem()
        {
            await TestGroupItemsAppService.CreateAsync(CreateDto);
            CloseCreateModal();
            await GetTestGroupItemsAsync();
        }

        private void OpenCreateModal()
        {
            CreateDto = new TestGroupItemsCreateDto();
            IsCreateModalVisible = true;

            IsDeleteModalVisible = false;
            IsUpdateModalVisible = false;
        }


        private void CloseCreateModal()
        {
            IsCreateModalVisible = false;
        }

        private void OpenDeleteModal(Guid id)
        {
            SelectedTestGroupItemId = id;
            IsDeleteModalVisible = true;

            IsUpdateModalVisible = false;
            IsCreateModalVisible = false;
        }

        private void CloseDeleteModal()
        {
            SelectedTestGroupItemId = null;
            IsDeleteModalVisible = false;
        }

        private async Task ConfirmDelete()
        {
            if (SelectedTestGroupItemId.HasValue)
            {
                await TestGroupItemsAppService.DeleteAsync(SelectedTestGroupItemId.Value);
                await GetTestGroupItemsAsync();
            }
            CloseDeleteModal();
        }

        private void CancelDelete()
        {
            CloseDeleteModal();
        }

        private async Task OnTestGroupChange(Syncfusion.Blazor.DropDowns.ChangeEventArgs<Guid, LookupDto<Guid>> args)
        {
            SelectedTestGroupId = args.Value;
            await GetTestGroupItemsAsync();
        }

        private void OpenUpdateModal(TestGroupItemDto item)
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
            IsUpdateModalVisible = true;
        }

        private void CloseUpdateModal()
        {
            IsUpdateModalVisible = false;
        }

        private async Task SaveChanges()
        {
            await TestGroupItemsAppService.UpdateAsync(UpdateDto);
            CloseUpdateModal();
            await GetTestGroupItemsAsync(); // Grid'i güncelle
        }

    }
}