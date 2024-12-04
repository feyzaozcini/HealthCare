using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Doctors;
using Syncfusion.Blazor.Inputs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class Doctor
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private List<DoctorWithNavigationPropertiesDto> DoctorsList { get; set; } = new();

        private List<string> Genders = new() { "Erkek", "Kadýn", "Diðer" };
        private bool IsModalVisible { get; set; } = false;
        private string FilterText { get; set; } = null; // Arama metni

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private long TotalCount;

        protected override async Task OnInitializedAsync()
        {
            await GetDoctorsAsync();
        }

        private void OpenCreateModal()
        {
            IsModalVisible = true; // Modal'ý açar
        }

        private async Task GetDoctorsAsync()
        {
            var input = new GetDoctorsInput
            {
                FilterText = FilterText,
                UserId = null,
                TitleId = null,
                IdentityNumber = null,
                BirthDateMin = null,
                BirthDateMax = null,
                Gender = null,
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize
            };


            var result = await DoctorsAppService.GetListAsync(input);

            DoctorsList = result.Items.ToList();
            TotalCount = result.TotalCount;
        }

        private void NavigateToDoctorProtocols(DoctorWithNavigationPropertiesDto doctor)
        {
            var doctorId = doctor.Doctor.Id;
            NavigationManager.NavigateTo($"/doctor-details/{doctorId}");
        }

        private async Task OnSearchAsync(InputEventArgs args)
        {
            CurrentPage = 1; // Her aramada ilk sayfadan baþla
            FilterText = args.Value; // Kullanýcý giriþini al
            await GetDoctorsAsync(); // Doktorlarý filtrele ve grid'i güncelle
        }
        //private void NavigateToDoctorDetails(Guid doctorId)
        //{
        //    // Doktor detaylarý sayfasýna yönlendir
        //    NavigationManager.NavigateTo($"/doctor-details/{doctorId}");
        //}

    }
}

