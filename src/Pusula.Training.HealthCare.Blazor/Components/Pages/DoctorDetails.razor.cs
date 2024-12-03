using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Protocols;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using System.Linq;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class DoctorDetails
    {
        [Parameter]
        public Guid DoctorId { get; set; }

        private DateTime selectedDate = DateTime.Now;
        private List<ProtocolDto> ProtocolsList { get; set; } = new();


        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private long TotalCount;

        protected override async Task OnInitializedAsync()
        {
            await LoadProtocolsAsync();
        }

        private async Task LoadProtocolsAsync()
        {
            var input = new GetProtocolsInput
            {
                FilterText =null,
                StartTime = null,
                EndTime = null,
                No = null,
                ProtocolStatus =null,
                ProtocolTypeId =null,
                ProtocolNoteId =null,
                ProtocolInsuranceId =null,
                PatientId = null,
                DepartmentId = null,
                DoctorId = DoctorId,
                MaxResultCount = 10,
                SkipCount = 0
            };

            var result = await ProtocolsAppService.GetListAsync(input);
            ProtocolsList = result.Items.ToList();
            TotalCount = result.TotalCount;
        }
    }
}