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
            // Se�ilen tarih aral���n� ProtocolsFilter'a ata
            ProtocolsFilter.StartTime = args.StartDate;
            ProtocolsFilter.EndTime = args.EndDate;

            // Verileri y�kle
            await LoadProtocolsAsync();
        }
        private async Task OnInputChange(InputEventArgs args)
        {

            if (string.IsNullOrEmpty(args.Value)) // Searchbox tamamen bo�sa
            {
                ProtocolsFilter.FilterText = null; // Filtreyi temizle
                await LoadProtocolsAsync(); // T�m protokolleri tekrar y�kle
                return;
            }

            if (args.Value.Length < 3)
            {
                ProtocolsFilter.FilterText = null; // 3 karakterden az ise filtreyi temizle
                ProtocolsList.Clear(); // Grid'i temizle (iste�e ba�l�)
                TotalCount = 0; // Toplam say�y� s�f�rla (iste�e ba�l�)
                StateHasChanged(); // UI'yi g�ncelle
                return;
            }

            ProtocolsFilter.FilterText = args.Value; // 3 karakterden fazla ise filtreye ata
            await LoadProtocolsAsync(); // Filtreli veriyi y�kle
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
                // E�er se�ili doktor yoksa geri y�nlendir
                NavigationManager.NavigateTo("/doctors");
                return;
            }
            //ProtocolsFilter.DoctorId = DoctorId; // Ba�lang��ta doktor ID'si ile filtreyi haz�rla
            ProtocolsFilter.DoctorId = DoctorsStateService.SelectedDoctor.Doctor.Id;
            await LoadProtocolsAsync();
        }
       
        private async Task LoadProtocolsAsync()
        {
            if (DoctorsStateService.SelectedDoctor == null)
            {
                // E�er se�ili doktor yoksa geri y�nlendir
                NavigationManager.NavigateTo("/doctors");
                return;
            }
            ProtocolsFilter.DoctorId = DoctorsStateService.SelectedDoctor.Doctor.Id;
            ProtocolsFilter.SkipCount = (CurrentPage - 1) * PageSize;

            var result = await ProtocolsAppService.GetListAsync(ProtocolsFilter);
            ProtocolsList = result.Items.ToList();
            TotalCount = result.TotalCount;
            //ProtocolsFilter.DoctorId = DoctorId; // Doktor ID'sini filtreye ekleyelim
            //ProtocolsFilter.SkipCount = (CurrentPage - 1) * PageSize; // Sayfa ge�i�i i�in SkipCount hesapla

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