﻿using Pusula.Training.HealthCare.Shared;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Popups;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Pusula.Training.HealthCare.ProtocolTypes;
using System.Linq;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class ProtocolTypes
    {
        private List<ProtocolTypeDto> ProtocolTypesList { get; set; } = new List<ProtocolTypeDto>();
        private GetProtocolTypesInput? ProtocolTypesFilter { get; set; }

        private ProtocolTypeCreateDto CreateProtocolTypesDto = new();
        private ProtocolTypeUpdateDto UpdateProtocolTypesDto = new();

        private Guid? SelectedProtocolTypeId;

        private SfDialog? CreateProtocolTypesDialog;
        private SfDialog? UpdateProtocolTypesDialog;
        private SfDialog? DeleteProtocolTypesDialog;

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private long TotalCount;

        SfTextBox TextBoxSearchObj { get; set; }


        protected override async Task OnInitializedAsync()
        {
            ProtocolTypesFilter = new GetProtocolTypesInput();
            await LoadLookupsAsync();
            await GetProtocolTypesAsync();
        }


        private async Task LoadLookupsAsync()
        {
            var input = new LookupRequestDto
            {
                MaxResultCount = PageSize,
                Filter = ProtocolTypesFilter!.FilterText
            };
        }


        private async Task GetProtocolTypesAsync()
        {
            var input = new GetProtocolTypesInput
            {
                FilterText = ProtocolTypesFilter?.FilterText,
                Name = ProtocolTypesFilter?.Name,
                MaxResultCount = ProtocolTypesFilter!.MaxResultCount,
                SkipCount = (CurrentPage - 1) * PageSize,
            };

            var result = await ProtocolTypesAppService.GetListAsync(input);
            ProtocolTypesList = result.Items.ToList();
            TotalCount = result.TotalCount;
        }


        private async Task SearchAsync(InputEventArgs args)
        {
            CurrentPage = 1;
            ProtocolTypesFilter!.FilterText = args.Value;
            await GetProtocolTypesAsync();
        }


        private async Task AddProtocolType()
        {
            await ProtocolTypesAppService.CreateAsync(CreateProtocolTypesDto);
            await LoadProtocolTypesAsync();
            await CloseProtocolTypeCreateModal();
            await GetProtocolTypesAsync();
        }


        private async Task UpdateProtocolType()
        {
            await ProtocolTypesAppService.UpdateAsync(UpdateProtocolTypesDto);
            await CloseProtocolTypeUpdateModal();
            await GetProtocolTypesAsync();
        }


        private async Task LoadProtocolTypesAsync()
        {
            var result = await ProtocolTypesAppService.GetListAsync(new GetProtocolTypesInput());
            ProtocolTypesList = result.Items.ToList();
        }


        private async Task OpenProtocolTypesCreateModal()
        {
            CreateProtocolTypesDto = new ProtocolTypeCreateDto();
            await CreateProtocolTypesDialog!.ShowAsync();
        }


        private async Task OpenProtocolTypeUpdateModal(ProtocolTypeDto item)
        {
            UpdateProtocolTypesDto = new ProtocolTypeUpdateDto
            {
                Id = item.Id,
                Name = item.Name,
            };
            await UpdateProtocolTypesDialog!.ShowAsync();
        }


        private async Task OpenProtocolTypeDeleteModal(Guid id)
        {
            SelectedProtocolTypeId = id;
            await DeleteProtocolTypesDialog!.ShowAsync();
        }


        private async Task ConfirmProtocolTypeDelete()
        {
            if (SelectedProtocolTypeId.HasValue)
            {
                await ProtocolTypesAppService.DeleteAsync(SelectedProtocolTypeId.Value);
                await GetProtocolTypesAsync();
            }
            await CloseProtocolTypeDeleteModal();
        }


        private async Task CloseProtocolTypeCreateModal()
        {
            await CreateProtocolTypesDialog!.HideAsync();
        }


        private async Task CloseProtocolTypeUpdateModal()
        {
            UpdateProtocolTypesDto = new ProtocolTypeUpdateDto();
            await UpdateProtocolTypesDialog!.HideAsync();
        }


        private async Task CloseProtocolTypeDeleteModal()
        {
            SelectedProtocolTypeId = null;
            await DeleteProtocolTypesDialog!.HideAsync();
        }
    }
}