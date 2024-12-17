using Pusula.Training.HealthCare.Protocols;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Popups;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class PatientDetails
    {
        private List<ProtocolDto> ProtocolsList { get; set; } = new List<ProtocolDto>();
        private GetProtocolsInput? ProtocolsFilter { get; set; }

        private SfDialog? NoteDialog;

        private string SelectedNote = string.Empty;
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;

        private long TotalCount;

        private bool ShowTable = true;


        protected override async Task OnInitializedAsync()
        {
            if (PatientStateService.SelectedPatientNavigation.Patient?.Id != null)
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
                patientId: PatientStateService.SelectedPatientNavigation.Patient.Id,
                departmentId: null,
                doctorId: null,
                currentPage: CurrentPage,
                pageSize: PageSize
            );

                await GetProtocolsAsync();

            }
        }


        private async Task GetProtocolsAsync()
        {
            if (PatientStateService.SelectedPatientNavigation.Patient?.Id != null)
            {
                // API çağrısı ile protokolleri getir
                var result = await ProtocolAppService.GetListAsync(ProtocolsFilter!);
                ProtocolsList = result.Items.ToList();
                TotalCount = result.TotalCount;

                StateHasChanged(); // UI'yı güncelle
            }
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


        private void NavigateBack()
        {
            if (!string.IsNullOrEmpty(StateService.PreviousPageUrl))
            {
                //Patient'ta filtrelense olsa bile url değişmediği için için otomatik olarak /patients sayfasına yönlendiriliyor zorunlu olarak 
                //ama yapı değişirse ilerde ve url alanı filtrelemeye yapıldıgında o filtrelemeye göre değişirse mesela Ali Ozturk u filtrelersek /patients/Ali-Ozturk gibi
                // PreviousPageUrl yapısı sayesinde filtrelenmiş önceki haline yönlendirilecektir. Kısacası İlerdeki yapıya uygun genişletilebilir bir kullanıldı.
                NavigationManager.NavigateTo(StateService.PreviousPageUrl);
            }
            else
            {
                // Önceki URL yoksa varsayılan bir sayfaya yönlendir
                NavigationManager.NavigateTo("/patients");
            }
        }
    }
}
