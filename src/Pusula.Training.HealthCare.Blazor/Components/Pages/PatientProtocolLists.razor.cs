using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.Protocols;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class PatientProtocolLists
    {
        private List<ProtocolDto> ProtocolsList { get; set; } = new List<ProtocolDto>();
        private GetProtocolsInput? ProtocolsFilter { get; set; }
        private SfDialog? NoteDialog;
        private string SelectedNote = string.Empty;
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private long TotalCount;

        protected override async Task OnInitializedAsync()
        {
            ProtocolsFilter = new GetProtocolsInput(
                filterText: null,
                startTime: null,
                endTime: null,
                no: null, // Sequence kullanıldığı için bu alan null olabilir
                protocolStatus: null,
                protocolTypeId: null,
                protocolNoteId: null,
                protocolInsuranceId: null,
                patientId: null,
                departmentId: null,
                doctorId: null,
                currentPage: CurrentPage,
                pageSize: PageSize
            );
            await GetProtocolsAsync();
        }

        private async Task GetProtocolsAsync()
        {
            var result = await ProtocolAppService.GetListAsync(ProtocolsFilter!);
            ProtocolsList = result.Items.ToList();
            TotalCount = result.TotalCount;
            StateHasChanged(); // Veri güncellemesini zorlar
        }

        private async Task OnInputChange(InputEventArgs args)
        {
            ProtocolsFilter!.FilterText = args.Value;
            await GetProtocolsAsync();
        }

        private async Task OpenNoteModal(string? description)
        {
            SelectedNote = description ?? "Açıklama mevcut değil.";
            await NoteDialog!.ShowAsync();
        }
        private async Task CloseNoteModal()
        {
            SelectedNote = string.Empty;
            await NoteDialog!.HideAsync();
        }
    }
}
