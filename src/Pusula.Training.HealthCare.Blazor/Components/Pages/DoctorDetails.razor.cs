using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Blazor.Containers;
using Pusula.Training.HealthCare.Protocols;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class DoctorDetails
    {
        [Parameter]
        public Guid DoctorId { get; set; }

        private DateTime selectedDate = DateTime.Now;
        private List<ProtocolDto> ProtocolsList { get; set; } = new();

        private DateTime? StartDate { get; set; } = null;
        private DateTime? EndDate { get; set; } = null;

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private long TotalCount;

        private async void OnDateRangeChange(RangePickerEventArgs<DateTime?> args)
        {
            // Seçilen tarih aralýðýný ProtocolsFilter'a ata
            ProtocolsFilter.StartTime = args.StartDate;
            ProtocolsFilter.EndTime = args.EndDate;

            // Verileri yükle
            await LoadProtocolsAsync();
        }
        private async Task OnInputChange(InputEventArgs args)
        {

            if (string.IsNullOrEmpty(args.Value)) // Searchbox tamamen boþsa
            {
                ProtocolsFilter.FilterText = null; // Filtreyi temizle
                await LoadProtocolsAsync(); // Tüm protokolleri tekrar yükle
                return;
            }

            if (args.Value.Length < 3)
            {
                ProtocolsFilter.FilterText = null; // 3 karakterden az ise filtreyi temizle
                ProtocolsList.Clear(); // Grid'i temizle (isteðe baðlý)
                TotalCount = 0; // Toplam sayýyý sýfýrla (isteðe baðlý)
                StateHasChanged(); // UI'yi güncelle
                return;
            }

            ProtocolsFilter.FilterText = args.Value; // 3 karakterden fazla ise filtreye ata
            await LoadProtocolsAsync(); // Filtreli veriyi yükle
        }
        private GetProtocolsInput ProtocolsFilter { get; set; } = new()
        {
            FilterText = null,
            StartTime = null,
            EndTime = null,
            No = null,
            ProtocolStatus = null,
            ProtocolTypeId = null,
            ProtocolNoteId = null,
            ProtocolInsuranceId = null,
            PatientId = null,
            DepartmentId = null,
            DoctorId = null,
            MaxResultCount = LimitedResultRequestDto.DefaultMaxResultCount,
            SkipCount = 0
        };
        protected override async Task OnInitializedAsync()
        {

            if (DoctorsStateService.SelectedDoctor == null)
            {
                // Eðer seçili doktor yoksa geri yönlendir
                NavigationManager.NavigateTo("/doctors");
                return;
            }
            //ProtocolsFilter.DoctorId = DoctorId; // Baþlangýçta doktor ID'si ile filtreyi hazýrla
            ProtocolsFilter.DoctorId = DoctorsStateService.SelectedDoctor.Doctor.Id;
            await LoadProtocolsAsync();
        }
       
        private async Task LoadProtocolsAsync()
        {
            if (DoctorsStateService.SelectedDoctor == null)
            {
                // Eðer seçili doktor yoksa geri yönlendir
                NavigationManager.NavigateTo("/doctors");
                return;
            }
            ProtocolsFilter.DoctorId = DoctorsStateService.SelectedDoctor.Doctor.Id;
            ProtocolsFilter.SkipCount = (CurrentPage - 1) * PageSize;

            var result = await ProtocolsAppService.GetListAsync(ProtocolsFilter);
            ProtocolsList = result.Items.ToList();
            TotalCount = result.TotalCount;
            //ProtocolsFilter.DoctorId = DoctorId; // Doktor ID'sini filtreye ekleyelim
            //ProtocolsFilter.SkipCount = (CurrentPage - 1) * PageSize; // Sayfa geçiþi için SkipCount hesapla

            //var result = await ProtocolsAppService.GetListAsync(ProtocolsFilter);
            //ProtocolsList = result.Items.ToList();
            //TotalCount = result.TotalCount;
        }

        private void NavigateToExamination(Guid protocolId, Guid patientId)
        {
            //NavigationManager.NavigateTo($"/examination/{protocolId}/{patientId}");
            ProtocolStateService.SetProtocol(protocolId, patientId);
            NavigationManager.NavigateTo("/examination");
        }
    }
}