using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Blazor.Containers;
using Pusula.Training.HealthCare.LabRequests;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class LabProtocols
    {
        private List<LabRequestDto> LabRequestsList { get; set; } = new List<LabRequestDto>();
        private GetLabRequestsInput? LabRequestsFilter { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private long TotalCount;

        protected override async Task OnInitializedAsync()
        {
            LabRequestsFilter = new GetLabRequestsInput(
                filterText: null,
                protocolId: null,
                doctorId: null,
                doctorName: null,
                date: null,
                status: null,
                description: null,
                currentPage: CurrentPage,
                pageSize: PageSize
            );
            await GetLabRequestsAsync();
        }

        private async Task GetLabRequestsAsync()
        {
            var result = await LabRequestsAppService.GetListWithNavigationPropertiesAsync(LabRequestsFilter!);
            LabRequestsList = result.Items.ToList();
            TotalCount = result.TotalCount;
        }

        private async Task OnInputChange(InputEventArgs args)
        {
            LabRequestsFilter!.FilterText = args.Value;
            await GetLabRequestsAsync();
        }

        private void RowSelectHandler(RowSelectEventArgs<LabRequestDto> args)
        {
            LabRequestService.SelectedLabRequest = args.Data;
        }

        private void RedirectToTestRequest()
        {
            if (LabRequestService.SelectedLabRequest != null)
            {
                NavigationManager.NavigateTo("/lab-request");
            }
        }
    }
}
