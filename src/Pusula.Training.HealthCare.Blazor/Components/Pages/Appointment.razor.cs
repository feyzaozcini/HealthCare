using Blazorise;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.Appointments;
using System.Collections.Generic;
using System;
using Volo.Abp.BlazoriseUI.Components;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class Appointment
    {
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private IReadOnlyList<AppointmentWithNavigationPropertiesDto> AppointmentList { get; set; }
        private AppointmentCreateDto NewAppointment { get; set; }
        private Validations NewAppointmentValidations { get; set; } = new();
        private AppointmentUpdateDto EditingAppointment { get; set; }
        private Validations EditingAppointmentValidations { get; set; } = new();
        private Guid EditingAppointmentId { get; set; }
        private Modal CreateAppointmentModal { get; set; } = new();
        private Modal EditAppointmentModal { get; set; } = new();
        private GetAppointmentsInput Filter { get; set; }
        private DataGridEntityActionsColumn<AppointmentWithNavigationPropertiesDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "Appointment-create-tab";
        protected string SelectedEditTab = "Appointment-edit-tab";

        private IReadOnlyList<LookupDto<Guid>> PatientsCollection { get; set; } = [];
        private IReadOnlyList<LookupDto<Guid>> DoctorsCollection { get; set; } = [];
        private IReadOnlyList<LookupDto<Guid>> DepartmentsCollection { get; set; } = [];
        private IReadOnlyList<LookupDto<Guid>> AppointmentTypesCollection { get; set; } = [];

        private IReadOnlyList<LookupDto<AppointmentStatus>> AppointmentStatusCollection { get; set; } = new List<LookupDto<AppointmentStatus>>();

        private List<AppointmentWithNavigationPropertiesDto> SelectedAppointments { get; set; } = [];
        private bool AllAppointmentsSelected { get; set; }

        public Appointment()
        {
            NewAppointment = new AppointmentCreateDto();
            EditingAppointment = new AppointmentUpdateDto();
            Filter = new GetAppointmentsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            AppointmentList = [];
        }

        protected override async Task OnInitializedAsync()
        {
            await GetPatientCollectionLookupAsync();
            await GetDoctorCollectionLookupAsync();
            await GetDepartmentCollectionLookupAsync();
            await GetAppointmentTypeCollectionLookupAsync();

            AppointmentStatusCollection = Enum.GetValues(typeof(AppointmentStatus))
       .Cast<AppointmentStatus>()
       .Select(b => new LookupDto<AppointmentStatus>
       {
           Id = b,
           DisplayName = b.ToString()
       })
            .ToList();
        }

        private async Task OpenCreateAppointmentModalAsync()
        {
            NewAppointment = new AppointmentCreateDto
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1),

            };


            SelectedCreateTab = "Patient-create-tab";

            await NewAppointmentValidations.ClearAll();
            await CreateAppointmentModal.Show();
        }

        private async Task CloseCreateAppointmentModalAsync()
        {
            NewAppointment = new AppointmentCreateDto
            {

            };

            await CreateAppointmentModal.Hide();
        }




        private async Task GetPatientCollectionLookupAsync(string? newValue = null)
        {
            PatientsCollection = (await AppointmentsAppService.GetPatientLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

        private async Task GetDoctorCollectionLookupAsync(string? newValue = null)
        {
            DoctorsCollection = (await AppointmentsAppService.GetDoctorLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

        private async Task GetDepartmentCollectionLookupAsync(string? newValue = null)
        {
            DepartmentsCollection = (await AppointmentsAppService.GetDepartmentLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

        private async Task GetAppointmentTypeCollectionLookupAsync(string? newValue = null)
        {
            AppointmentTypesCollection = (await AppointmentsAppService.GetAppointmentTypeLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }
    }
}
