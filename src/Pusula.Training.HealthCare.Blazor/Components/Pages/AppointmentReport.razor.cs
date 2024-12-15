using Pusula.Training.HealthCare.Appointments;
using Syncfusion.Blazor.PivotView;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using System.Linq;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class AppointmentReport
    {
        SfPivotView<AppointmentPivotData> Pivot;
        public List<AppointmentPivotData> PivotData { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            /*var input = new GetAppointmentsInput
                    {
                    StartDate = DateTime.Now.AddMonths(-1),
                    EndDate = DateTime.Now
                };*/

            var input = new GetAppointmentsInput
            {
                StartDate = StartDate,
                EndDate = EndDate,
                MaxResultCount = 100,
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
    }
}
