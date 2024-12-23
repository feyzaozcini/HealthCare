using Pusula.Training.HealthCare.Appointments;
using Syncfusion.Blazor.PivotView;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using System.Linq;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.AppointmentTypes;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class AppointmentReport
    {
        private List<ChartDataDto> ChartData = new List<ChartDataDto>();
        private List<ChartDataDto> DepartmentChartData = new List<ChartDataDto>();
        private List<ChartDataDto> GenderChartData = new List<ChartDataDto>();
        private List<AppointmentStatus> AppointmentStatuses = new List<AppointmentStatus>();
        private IReadOnlyList<LookupDto<Guid>> AppointmentTypesCollection { get; set; } = new List<LookupDto<Guid>>();
        private IReadOnlyList<LookupDto<Gender>> GendersCollection { get; set; } = new List<LookupDto<Gender>>();
        private GetAppointmentsInput Filter { get; set; }
        private List<Pusula.Training.HealthCare.AppointmentTypes.AppointmentTypeDto> FilteredAppointmentTypes { get; set; } = new();
        private List<DepartmentDto> FilteredDepartments = new();
        private List<DoctorWithNavigationPropertiesDto> FilteredDoctors = new();

        SfPivotView<AppointmentPivotData> Pivot;
        public List<AppointmentPivotData> PivotData { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;

        public AppointmentReport()
        {
            Filter = new GetAppointmentsInput
            {
                MaxResultCount = 1000,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };

        }

        protected override async Task OnInitializedAsync()
        {
            GendersCollection = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Select(b => new LookupDto<Gender> { Id = b, DisplayName = b.ToString() })
                .ToList();
            var input = new GetAppointmentTypesInput
            {
                MaxResultCount = 1000,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            var result = await AppointmentTypesAppService.GetListAsync(input);
            if (result?.Items != null)
            {
                FilteredAppointmentTypes = result.Items.ToList();
            }
            await LoadAppointmentsAsync();
            await GetAppointmentTypeCollectionLookupAsync();
            await LoadDataAsync();
        }

        private async Task LoadAppointmentsAsync()
        {
            var input = new GetAppointmentsInput
            {
                StartDate = Filter?.StartDate,
                EndDate = Filter?.EndDate,
                Note = null,
                AppointmentStatus = null,
                IsBlock = null,
                PatientId = null,
                AppointmentTypeId = Filter?.AppointmentTypeId,
                DoctorId = null,
                DepartmentId = null,
                MaxResultCount = 1000,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };

            var result = await AppointmentsAppService.GetListAsync(input);
            ChartData = result.Items
                .GroupBy(a => a.Appointment.AppointmentStatus)
                .Select(group => new ChartDataDto
                {
                    Name = group.Key.ToString(),
                    Count = group.Count()
                })
                .ToList();

            DepartmentChartData = result.Items
                .GroupBy(a => a.Department.Name)
                .Select(group => new ChartDataDto
                {
                    Name = group.Key,
                    Count = group.Count()
                })
                .ToList();

            GenderChartData = result.Items
                .GroupBy(a => a.Patient.Gender.ToString())
                .Select(group => new ChartDataDto
                {
                    Name = group.Key,
                    Count = group.Count()
                })
                .ToList();

        }
        private async Task LoadDataAsync()
        {
            var input = new GetAppointmentsInput
            {
                StartDate = StartDate,
                EndDate = EndDate,
                MaxResultCount = 1000,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            var appointments = await AppointmentsAppService.GetListAsync(input);

            // Randevu verilerini Pivot View için uygun formata dönüştür
            PivotData = appointments.Items.Select(item => new AppointmentPivotData
            {
                DepartmentName = item.Department.Name,
                DoctorName = $"{item.Doctor?.TitleName} {item.Doctor?.Name} {item.Doctor?.SurName}",
                AppointmentStatus = item.Appointment.AppointmentStatus.ToString(),
                AppointmentCount = 1 // Her kayıt bir randevu olarak sayılır
            }).ToList();
        }

        private async Task ClearFilters()
        {
            
            Filter.StartDate = null;
            Filter.EndDate = null;
            Filter.AppointmentTypeId = null;

            Filter = new GetAppointmentsInput
            {
                MaxResultCount = 1000,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };

            await LoadAppointmentsAsync();
        }

        private async Task GetAppointmentTypeCollectionLookupAsync(string? newValue = null)
        {
            AppointmentTypesCollection = (await AppointmentsAppService.GetAppointmentTypeLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

        public async Task LoadFilteredData()
        {
            await LoadDataAsync();
        }
        public async Task ExportToExcel(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            if (Pivot != null)
            {
                await Pivot.ExportToExcelAsync();
            }
        }

        // Pivot View için veri modeli
        public class AppointmentPivotData
        {
            public string DepartmentName { get; set; }
            public string DoctorName { get; set; }
            public string AppointmentStatus { get; set; }
            public int AppointmentCount { get; set; }
        }
        // Chart veri modeli
        public class ChartDataDto
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }
    }
}
