using Microsoft.JSInterop;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Shared;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Schedule;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using System.Linq;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class Appointment
    {
        private SfSchedule<FlatAppointmentDto> SfScheduleInstance;
        DateTime CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        View CurrentView = View.WorkWeek;
        Syncfusion.Blazor.Schedule.ValidationRules ValidationRules = new Syncfusion.Blazor.Schedule.ValidationRules { Required = true };
        private AppointmentUpdateDto EditAppointment { get; set; } = new AppointmentUpdateDto();
        private IReadOnlyList<LookupDto<Guid>> PatientsCollection { get; set; } = new List<LookupDto<Guid>>();
        private IReadOnlyList<LookupDto<Guid>> DoctorsCollection { get; set; } = new List<LookupDto<Guid>>();
        private IReadOnlyList<LookupDto<Guid>> DepartmentsCollection { get; set; } = new List<LookupDto<Guid>>();
        private IReadOnlyList<LookupDto<Guid>> AppointmentTypesCollection { get; set; } = new List<LookupDto<Guid>>();
        private IReadOnlyList<LookupDto<AppointmentStatus>> AppointmentStatusCollection { get; set; } = new List<LookupDto<AppointmentStatus>>();
        private IReadOnlyList<LookupDto<Gender>> GendersCollection { get; set; } = new List<LookupDto<Gender>>();
        private IReadOnlyList<LookupDto<Pusula.Training.HealthCare.Patients.Type>> TypesCollection { get; set; } = new List<LookupDto<Pusula.Training.HealthCare.Patients.Type>>();
        private List<FlatAppointmentDto> AppointmentList { get; set; } = new List<FlatAppointmentDto>();
        private AppointmentCreateDto NewAppointment { get; set; } = new AppointmentCreateDto();

        private List<DoctorWithNavigationPropertiesDto> AllDoctors = new();
        private List<DepartmentDto> AllDepartments = new();

        private DoctorWithNavigationPropertiesDto SelectedDoctor;
        private DepartmentDto SelectedDepartment;

        private List<DepartmentDto> FilteredDepartments = new();
        private List<DoctorWithNavigationPropertiesDto> FilteredDoctors = new();
        private int IntervalValue { get; set; } = 60;
        private int SlotValue { get; set; } = 4;
        private bool GridLine { get; set; } = true;
        private PatientCreateDto NewPatient = new();
        private bool IsPatientDialogOpen = false;
        private List<Pusula.Training.HealthCare.AppointmentTypes.AppointmentType> FilteredAppointmentTypes { get; set; } = new();
        private string ErrorMessage { get; set; } = string.Empty;
        private string FilterText = "";
        private bool IsPatientSelectionPopupVisible { get; set; }

        private IReadOnlyList<PatientWithNavigationPropertiesDto> PatientList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private long TotalCountLong { get; set; }
        private GetPatientsInput Filter { get; set; }
        private bool AllPatientsSelected { get; set; }
        private List<PatientWithNavigationPropertiesDto> SelectedPatients { get; set; } = [];
        private Guid SelectedPatientId;

        private int[] _workingDays = new int[] { 1,2,4,3,5 };
        public string startHour = "09:00";
        public string endHour = "18:00";

        private SfToast? ToastObj;

        protected override async Task OnInitializedAsync()
        {
            // Fetch lookup data
            await GetPatientCollectionLookupAsync();
            await GetDoctorCollectionLookupAsync();
            await GetDepartmentCollectionLookupAsync();
            await GetAppointmentTypeCollectionLookupAsync();

            // Appointment Status
            AppointmentStatusCollection = Enum.GetValues(typeof(AppointmentStatus))
                .Cast<AppointmentStatus>()
                .Select(b => new LookupDto<AppointmentStatus> { Id = b, DisplayName = b.ToString() })
                .ToList();
            //Hasta kaydı için Gender listesi
            GendersCollection = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Select(b => new LookupDto<Gender> { Id = b, DisplayName = b.ToString() })
                .ToList();
            //Hasta kaydı için hasta tipleri 
            TypesCollection = Enum.GetValues(typeof(Pusula.Training.HealthCare.Patients.Type))
                .Cast<Pusula.Training.HealthCare.Patients.Type>()
                .Select(b => new LookupDto<Pusula.Training.HealthCare.Patients.Type> { Id = b, DisplayName = b.ToString() })
                .ToList();


            await LoadInitialDataAsync();

            ToastObj ??= new SfToast();
        }

        public Appointment()
        {
            Filter = new GetPatientsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            PatientList = [];
        }

        private async Task LoadInitialDataAsync()
        {

            var departmentResult = await DepartmentsAppService.GetListAsync(new GetDepartmentsInput());
            if (departmentResult?.Items != null)
            {
                FilteredDepartments = departmentResult.Items.ToList();
            }

            var doctorResult = await DoctorsAppService.GetListAsync(new GetDoctorsInput());
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
        private async Task LoadAppointmentsAsync()
        {
            var input = new GetAppointmentsInput
            {
                StartDate = null,
                EndDate = null,
                Note = null,
                AppointmentStatus = null,
                IsBlock = null,
                PatientId = null,
                AppointmentTypeId = null,
                DoctorId = SelectedDoctor?.Doctor?.Id, //Doctorun randevularını getirme filtresi
                DepartmentId = SelectedDepartment?.Id //Departmanın randevularını getirme filtresi
            };

            var result = await AppointmentsAppService.GetListAsync(input);

            AppointmentList = result.Items.Select(a => new FlatAppointmentDto
            {
                Id = a.Appointment.Id.GetHashCode(),
                Subject = a.AppointmentType.Name,
                StartTime = a.Appointment.StartDate,
                EndTime = a.Appointment.EndDate,
                IsBlock = a.Appointment.IsBlock,
            }).ToList();

        }

        
        #region Doctor Department Selection
        private async Task OnDepartmentValueChange(ChangeEventArgs<Guid, DepartmentDto> args)
        {
            // Seçilen departmanın Id'si
            Guid selectedDepartmentId = args.Value;

            // Seçilen departman objesi
            DepartmentDto selectedDepartment = FilteredDepartments.FirstOrDefault(d => d.Id == selectedDepartmentId);

            if (selectedDepartment != null)
            {
                await SelectDepartmentAsync(selectedDepartment);
            }
        }

        private async Task OnDoctorValueChange(ChangeEventArgs<Guid, DoctorWithNavigationPropertiesDto> args)
        {
            // Seçilen departmanın Id'si
            Guid selectedDoctorId = args.Value;

            // Seçilen departman objesi
            DoctorWithNavigationPropertiesDto selectedDoctor = FilteredDoctors.FirstOrDefault(d => d.Doctor.Id == selectedDoctorId);

            if (selectedDoctor != null)
            {
                await SelectDoctorAsync(selectedDoctor);
            }
        }

        private async Task SelectDoctorAsync(DoctorWithNavigationPropertiesDto doctor)
        {
            SelectedDoctor = doctor;
            // API çağrısı: Doktorun randevu türlerini getir
            FilteredAppointmentTypes = await AppointmentTypeRepository.GetAppointmentTypesForDoctorAsync(SelectedDoctor.Doctor.Id);

            var schedule = await DoctorWorkSchedulesAppService.GetScheduleForDoctorAsync(SelectedDoctor.Doctor.Id);
            if (schedule != null && schedule.Any())
            {
                // Eğer doktorun özel bir programı varsa, alınan değerleri ata
                var doctorSchedule = schedule.First(); // İlk programı seç (eğer birden fazlaysa bunu genişletebilirsiniz)
                _workingDays = doctorSchedule.WorkingDays;
                startHour = doctorSchedule.StartHour;
                endHour = doctorSchedule.EndHour;
            }
            else
            {
                 _workingDays = new int[] { 1, 2, 4, 3, 5 };
                 startHour = "09:00";
                 endHour = "18:00";
            }
            // Front-end'i yenile
            StateHasChanged();
            await LoadAppointmentsAsync();  //Seçilen doktroun randevularını getirmek için çağırıldı
            await Task.CompletedTask;
        }
        private async Task SelectDepartmentAsync(DepartmentDto department)
        {
            SelectedDepartment = department;

            if (department != null)
            {
                // Seçilen departmana ait doktorları listelemek için
                var departmentWithDoctors = await DepartmentsAppService.GetDoctorsByDepartmentIdAsync(department.Id);
                if (departmentWithDoctors != null)
                {
                    FilteredDoctors = departmentWithDoctors
                        .Select(dd => new DoctorWithNavigationPropertiesDto
                        {
                            Doctor = dd.Doctor,
                            Title = dd.Title,
                            User = dd.User,
                            FullName = $"{dd.Title.Name} {dd.User.Name} {dd.User.Surname}"
                        })
                        .ToList();

                    await LoadAppointmentsAsync();
                }
            }
            else
            {
                FilteredDoctors.Clear();
            }
            await Task.CompletedTask;
        }
        #endregion

        #region Appointment Create
        // NewAppointment nesnesini sıfırlama
        private void ClearNewAppointment()
        {
            NewAppointment = new AppointmentCreateDto
            {

            };
        }

        private async Task CreateAndCloseModal()
        {

            if (SelectedDoctor == null)
            {
                await JSRuntime.InvokeVoidAsync("alert", "Lütfen bir doktor seçiniz!");
                return;
            }
            if (SelectedDepartment == null)
            {
                await JSRuntime.InvokeVoidAsync("alert", "Lütfen bir departman seçiniz!");
                return;
            }
            if (SelectedPatientId == Guid.Empty)
            {
                await JSRuntime.InvokeVoidAsync("alert", "Lütfen bir hasta seçiniz!");
                return;
            }
            await CreateAppointment();// Randevu oluşturma işlemini çağır
            await LoadAppointmentsAsync(); // Verileri yeniden yükle
            // await SfScheduleInstance.Refresh(); // Takvimi güncelle
            ClearNewAppointment(); // Formdaki bilgileri temizle
            SfScheduleInstance.CloseEditor(); // Modalı kapat

        }
        private async Task CreateAppointment()
        {
            await HandleError(async () =>
            {
                NewAppointment.PatientId = SelectedPatientId;
                NewAppointment.DoctorId = SelectedDoctor.Doctor.Id;
                NewAppointment.DepartmentId = SelectedDepartment.Id;

                // Randevuyu oluştur
                var createdAppointment = await AppointmentsAppService.CreateAsync(NewAppointment);

                // Başarılı mesaj
                await ShowToast("Randevu başarıyla oluşturuldu!", true);
            });
        }
        #endregion

        #region Patient Create
        private void OpenPatientDialog()
        {
            NewPatient = new PatientCreateDto
            {
                BirthDate = DateTime.Now
            };
            IsPatientDialogOpen = true;
        }

        private void ClosePatientDialog()
        {
            IsPatientDialogOpen = false;
            NewPatient = new(); // Form temizliği
        }
        private async Task CreatePatient()
        {

            await PatientsAppService.CreateAsync(NewPatient);
            await GetPatientCollectionLookupAsync();
            ClosePatientDialog();
            await Task.CompletedTask;
        }
        #endregion

        #region Patient Selection
        private async Task OnSearchTextChanged(Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            if (string.IsNullOrEmpty(args?.Value?.ToString()))
            {

                PatientList = new List<PatientWithNavigationPropertiesDto>();
                TotalCount = 0;
                return;
            }

            Filter.FilterText = args.Value.ToString();
            await GetPatientsAsync();
        }
        private async Task GetPatientsAsync()
        {
            if (Filter == null || (string.IsNullOrEmpty(Filter.FilterText) &&
            (Filter.FirstName == null && Filter.LastName == null && Filter.IdentityNumber == null &&
            Filter.PassportNumber == null && Filter.MobilePhoneNumber == null &&
            Filter.Email == null && Filter.No == null)))
            {
                return; // Hiçbir filtre yoksa, veri getirme işlemi yapılmaz
            }

            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;


            if (Filter == null)
            {
                PatientList = [];
                TotalCount = 0;
            }
            else
            {
                var result = await PatientsAppService.GetListAsync(Filter);
                PatientList = (IReadOnlyList<PatientWithNavigationPropertiesDto>)result.Items;
                TotalCount = (int)result.TotalCount;
            }

            await ClearSelection();
        }
        private async Task ClearSelection()
        {
            AllPatientsSelected = false;
            SelectedPatients.Clear();

            await InvokeAsync(StateHasChanged);
        }
        protected virtual async Task OnSearchButtonClicked()
        {
            if (Filter == null || (string.IsNullOrEmpty(Filter.FilterText) &&
                (Filter.FirstName == null && Filter.LastName == null && Filter.IdentityNumber == null &&
                Filter.PassportNumber == null && Filter.MobilePhoneNumber == null &&
                Filter.Email == null && Filter.No == null)))
            {
                return; // Boş filtrelerle veri çekme
            }

            // Filtreyi doğrula ve veriyi getir
            await GetPatientsAsync();
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetPatientsAsync();
            await InvokeAsync(StateHasChanged);
        }
        private void OpenPatientSelectionPopup()
        {
            IsPatientSelectionPopupVisible = true;
        }

        private void ClosePatientSelectionPopup()
        {
            IsPatientSelectionPopupVisible = false;
        }
        private void SelectPatient(PatientWithNavigationPropertiesDto patient)
        {
            SelectedPatientId = patient.Patient.Id; // Hastanın ID'sini sakla
                                                    // Pop-up'ı kapatma işlemi
            IsPatientSelectionPopupVisible = false;
        }
        #endregion

        #region Lookups
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
        #endregion

        #region Dto For Scheduler
        public class FlatAppointmentDto
        {
            public int Id { get; set; }
            public string Subject { get; set; }
            public string Location { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string Description { get; set; }
            public bool IsAllDay { get; set; }
            public bool IsBlock { get; set; }
            public bool IsReadOnly { get; set; }
            public string RecurrenceRule { get; set; }
            public string RecurrenceException { get; set; }
            public Nullable<int> RecurrenceID { get; set; }
        }
        #endregion

        #region Handle Error
        public async Task HandleError(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (UserFriendlyException ex) // Özel hata
            {
                await ShowToast(ex.Message, false);
            }
            catch (Exception) // Genel hata
            {
                await ShowToast("Bir hata oluştu. Lütfen tekrar deneyin.", false);
            }
        }

        private async Task ShowToast(string message, bool isSuccess = true)
        {
            await ToastObj!.ShowAsync(new ToastModel
            {
                Content = message,
                CssClass = isSuccess ? "e-toast-success" : "e-toast-danger",
                Timeout = 3000,
                ShowCloseButton = true
            });
        }
        #endregion
    }
}
