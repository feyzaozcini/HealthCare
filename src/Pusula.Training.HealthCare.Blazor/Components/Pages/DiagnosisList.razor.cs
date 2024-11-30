using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.DiagnosisGroups;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using System.Linq;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class DiagnosisList
    {
        protected List<DiagnosisGroupDto> DiagnosisGroupsList { get; set; } = new();
        protected List<DiagnosisWithNavigationPropertiesDto> IcdList { get; set; } = new();

        protected GetDiagnosisGroupsInput DiagnosisGroupsFilter { get; set; } = new();
        protected GetDiagnosisInput DiagnosisFilter { get; set; } = new();

        protected int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        protected int CurrentPage { get; set; } = 1;
        protected long TotalCount { get; set; }
        protected Guid? SelectedGroupId { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            await LoadDiagnosisGroupsAsync();
            await LoadIcdListAsync();
        }

        protected async Task LoadDiagnosisGroupsAsync()
        {
            var input = new GetDiagnosisGroupsInput
            {
                FilterText = DiagnosisGroupsFilter.FilterText,
                Name = DiagnosisGroupsFilter.Name,
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize
            };

            var result = await DiagnosisGroupAppservice.GetListAsync(input);
            DiagnosisGroupsList = result.Items.ToList();
        }

        protected async Task LoadIcdListAsync()
        {
            var input = new GetDiagnosisInput
            {
                FilterText = null,
                Name = null,
                Code = null,
                GroupId = SelectedGroupId,
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize
            };

            var result = await DiagnosisAppService.GetListAsync(input);
            IcdList = result.Items.ToList();
            TotalCount = result.TotalCount;
        }

        protected async Task FilterIcdListByGroup(Guid groupId)
        {
            SelectedGroupId = groupId;
            await LoadIcdListAsync();
        }
    }
}