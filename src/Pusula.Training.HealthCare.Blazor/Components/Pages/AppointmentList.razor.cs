using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Shared;
using Syncfusion.Blazor.DropDowns;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using System.Linq;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class AppointmentList
    {
        public List<AppointmentListItem> Appointments { get; set; } = new();
        private List<AppointmentListItem> PastAppointments => Appointments.Where(a => a.StartDate < DateTime.Now).ToList();
        private List<AppointmentListItem> UpcomingAppointments => Appointments.Where(a => a.StartDate >= DateTime.Now).ToList();
        public int TotalPatients { get; set; }
        public int TotalAppointments { get; set; }
        public int TodaysAppointments { get; set; }
        private IReadOnlyList<LookupDto<AppointmentStatus>> AppointmentStatusCollection { get; set; } = new List<LookupDto<AppointmentStatus>>();
        private bool IsEditDialogOpen = false;
        private AppointmentListItem? SelectedAppointment;
        private AppointmentStatus SelectedAppointmentStatus;
        private List<DoctorWithNavigationPropertiesDto> AllDoctors = new();
        private DoctorWithNavigationPropertiesDto SelectedDoctor;
        private List<DoctorWithNavigationPropertiesDto> FilteredDoctors = new();
        private List<DepartmentDto> AllDepartments = new();
        private DepartmentDto SelectedDepartment;
        private List<DepartmentDto> FilteredDepartments = new();
        private SfDropDownList<Guid, DepartmentDto> departmentDropDown;
        private SfDropDownList<Guid, DoctorWithNavigationPropertiesDto> doctorDropDown;
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        // Uyarı için değişken
        private bool IsWarningDialogOpen { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            // Appointment Status
            AppointmentStatusCollection = Enum.GetValues(typeof(AppointmentStatus))
                .Cast<AppointmentStatus>()
                .Select(b => new LookupDto<AppointmentStatus> { Id = b, DisplayName = b.ToString() })
                .ToList();

            await LoadInitialDataAsync();
        }
        private async Task LoadInitialDataAsync()
        {
            var input = new GetAppointmentsInput
            {
                MaxResultCount = 100,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };

            var result = await AppointmentAppService.GetListAsync(input);
            Appointments = result.Items.Select(a => new AppointmentListItem
            {
                DepartmentName = a.Department.Name,
                DoctorFullName = $"{a.Doctor.TitleName} {a.Doctor.Name} {a.Doctor.SurName}",
                PatientFullName = $"{a.Patient.FirstName} {a.Patient.LastName}",
                StartDate = a.Appointment.StartDate,
                EndDate = a.Appointment.EndDate,
                Status = a.Appointment.AppointmentStatus.ToString(),
                AppointmentTypeName = a.AppointmentType.Name
            }).ToList();

            TotalPatients = Appointments.Select(a => a.PatientFullName).Distinct().Count();
            TotalAppointments = Appointments.Count;
            TodaysAppointments = Appointments.Count(a => a.StartDate.Date == DateTime.Today);

            await LoadDoctorAsync();
            await LoadDepartmentAsync();
        }
        private async Task LoadDoctorAsync()
        {
            var doctorResult = await DoctorAppService.GetListAsync(new GetDoctorsInput());
            if (doctorResult?.Items != null)
            {
                AllDoctors = doctorResult.Items.ToList();
                FilteredDoctors = AllDoctors
                .Select(d => new DoctorWithNavigationPropertiesDto
                {
                    Doctor = d.Doctor,
                    Title = d.Title,
                    User = d.User,
                    FullName = $"{d.Title?.Name} {d.User?.Name} {d.User?.Surname}" // FullName oluşturuldu
                })
                .ToList();
            }
        }
        private async Task LoadDepartmentAsync()
        {

            var departmentResult = await DepartmentAppService.GetListAsync(new GetDepartmentsInput());
            if (departmentResult?.Items != null)
            {
                AllDepartments = departmentResult.Items.ToList();
                FilteredDepartments = AllDepartments
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToList();
            }
        }
        private async Task LoadAppointmentsAsync()
        {
            var input = new GetAppointmentsInput
            {
                DoctorId = SelectedDoctor?.Doctor?.Id, //Doctorun randevularını getirme filtresi
                DepartmentId = SelectedDepartment?.Id, //Departmana ait randevuları getirme filtresi
                MaxResultCount = 1000,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };

            var result = await AppointmentAppService.GetListAsync(input);
            Appointments = result.Items.Select(a => new AppointmentListItem
            {
                Id = a.Appointment.Id,
                DepartmentName = a.Department.Name,
                DoctorId = a.Doctor.Id,
                PatientId = a.Patient.Id,
                AppointmentTypeId = a.AppointmentType.Id,
                DepartmentId = a.Department.Id,
                DoctorFullName = $"{a.Doctor.TitleName} {a.Doctor.Name} {a.Doctor.SurName}",
                PatientFullName = $"{a.Patient.FirstName} {a.Patient.LastName}",
                StartDate = a.Appointment.StartDate,
                EndDate = a.Appointment.EndDate,
                Status = a.Appointment.AppointmentStatus.ToString(),
                AppointmentTypeName = a.AppointmentType.Name,
                IsBlock = a.Appointment.IsBlock
            }).ToList();

            TotalPatients = Appointments.Select(a => a.PatientFullName).Distinct().Count();
            TotalAppointments = Appointments.Count;
            TodaysAppointments = Appointments.Count(a => a.StartDate.Date == DateTime.Today);
        }
        private async Task OnDoctorValueChange(ChangeEventArgs<Guid, DoctorWithNavigationPropertiesDto> args)
        {
            // Seçilen doktor Id'si
            Guid selectedDoctorId = args.Value;

            // Seçilen doktor objesi
            DoctorWithNavigationPropertiesDto selectedDoctor = FilteredDoctors.FirstOrDefault(d => d.Doctor.Id == selectedDoctorId);

            if (selectedDoctor != null)
            {
                await SelectDoctorAsync(selectedDoctor);
            }
        }

        private async Task SelectDoctorAsync(DoctorWithNavigationPropertiesDto doctor)
        {
            SelectedDoctor = doctor;
            await LoadAppointmentsAsync();  //Seçilen doktroun randevularını getirmek için çağırıldı
            await Task.CompletedTask;
        }
        private async Task OnDepartmentValueChange(ChangeEventArgs<Guid, DepartmentDto> args)
        {
            
            Guid selectedDepartmentId = args.Value;

           
            DepartmentDto selectedDepartment = FilteredDepartments.FirstOrDefault(d => d.Id == selectedDepartmentId);

            if (selectedDepartment != null)
            {
                await SelectDepartmentAsync(selectedDepartment);
            }
        }
        private async Task SelectDepartmentAsync(DepartmentDto department)
        {
            SelectedDepartment = department;
            await LoadAppointmentsAsync();  //Seçilen departmana ait randevularını getirmek için çağırıldı
            await Task.CompletedTask;
        }
        private async Task ClearFilters()
        {
            SelectedDepartment = null;
            SelectedDoctor = null;
            await departmentDropDown.ClearAsync();
            await doctorDropDown.ClearAsync();
            await LoadDoctorAsync();
            await LoadDepartmentAsync();
            await LoadAppointmentsAsync();
            await LoadInitialDataAsync();
            StateHasChanged();
        }
        private string GetStatusClass(string status)
        {
            if (Enum.TryParse(typeof(AppointmentStatus), status, out var result))
            {
                var appointmentStatus = (AppointmentStatus)result;

                return appointmentStatus switch
                {
                    AppointmentStatus.Scheduled => "status-scheduled",    // Turuncu
                    AppointmentStatus.Confirmed => "status-confirmed",    // Mavi
                    AppointmentStatus.Cancelled => "status-cancelled",    // Kırmızı
                    AppointmentStatus.NoShow => "status-noshow",          // Kırmızı
                    AppointmentStatus.Pending => "status-pending",        // Yeşil
                    AppointmentStatus.InProgress => "status-inprogress",// Yeşil
                    AppointmentStatus.Completed => "status-completed",    // Yeşil
                    AppointmentStatus.Postponed => "status-postponed",    // Kırmızı
                    AppointmentStatus.Failed => "status-failed",          // Kırmızı
                    _ => "status-default",                                // Varsayılan
                };
            }

            return "status-default";
        }

        private void OpenEditDialog(AppointmentListItem appointment)
        {
            SelectedAppointment = appointment;
            SelectedAppointmentStatus = Enum.Parse<AppointmentStatus>(appointment.Status);
            if (SelectedDoctor == null)
            {
                // Doktor seçilmemişse uyarıyı aç
                IsWarningDialogOpen = true;
                return;
            }
            else
            {
                IsEditDialogOpen = true;
            }

        }
        private void CloseDialog()
        {
            IsEditDialogOpen = false;
            SelectedAppointment = null;
        }
        private async Task UpdateAppointmentStatus()
        {
            if (SelectedAppointment == null) return;

            var updateDto = new AppointmentUpdateDto
            {
                Id = SelectedAppointment.Id,
                AppointmentStatus = SelectedAppointmentStatus,
                StartDate = SelectedAppointment.StartDate,
                EndDate = SelectedAppointment.EndDate,
                Note = "Randevu Güncellendi", // Gerekirse doldur
                PatientId = SelectedAppointment.PatientId, // Mevcut değer ile değiştir
                DoctorId = SelectedAppointment.DoctorId, // Mevcut değer ile değiştir
                DepartmentId = SelectedAppointment.DepartmentId, // Mevcut değer ile değiştir
                AppointmentTypeId = SelectedAppointment.AppointmentTypeId, // Mevcut değer ile değiştir
                IsBlock = SelectedAppointment.IsBlock
            };

            await AppointmentAppService.UpdateAsync(updateDto);
            await LoadAppointmentsAsync(); // Randevular yeniden yüklenecek
            IsEditDialogOpen = false;
        }
       
        public class AppointmentListItem
        {
            public Guid Id { get; set; }
            public Guid DepartmentId { get; set; }
            public Guid DoctorId { get; set; }
            public Guid PatientId { get; set; }
            public Guid AppointmentTypeId { get; set; }
            public string DepartmentName { get; set; }
            public string DoctorFullName { get; set; }
            public string PatientFullName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Status { get; set; }
            public string AppointmentTypeName { get; set; }
            public bool IsBlock { get; set; }
            public string Note { get; set; }
        }
        private void CloseWarningDialog()
        {
            IsWarningDialogOpen = false;
        }
    }
}
