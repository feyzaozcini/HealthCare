using Pusula.Training.HealthCare.AppointmentRules;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Shared;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class AppointmentRule
    {
        private IReadOnlyList<LookupDto<Gender>> GendersCollection { get; set; } = new List<LookupDto<Gender>>();
        private IReadOnlyList<LookupDto<Guid>> DepartmentsCollection { get; set; } = new List<LookupDto<Guid>>();
        private IReadOnlyList<LookupDto<Guid>> DoctorsCollection { get; set; } = new List<LookupDto<Guid>>();

        private AppointmentRuleCreateDto NewAppointmentRule = new AppointmentRuleCreateDto();
        private AppointmentRuleUpdateDto EditAppointmentRule = new AppointmentRuleUpdateDto();
        
        private SfToast? ToastObj;

        private List<DoctorWithNavigationPropertiesDto> AllDoctors = new();
        private List<DepartmentDto> AllDepartments = new();

        private DoctorWithNavigationPropertiesDto SelectedDoctor;
        private DepartmentDto SelectedDepartment;

        private List<DepartmentDto> FilteredDepartments = new();
        private List<DoctorWithNavigationPropertiesDto> FilteredDoctors = new();

        public List<AppointmentListItem> AppointmentRules { get; set; } = new();

        private bool IsEditModalOpen = false;
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private bool IsAddNewDialogOpen = false;

        protected override async Task OnInitializedAsync()
        {
            await GetDoctorCollectionLookupAsync();
            await GetDepartmentCollectionLookupAsync();
            await LoadInitialDataAsync();
            await LoadAppointmentRulesAsync();
            ToastObj ??= new SfToast();
            GendersCollection = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Select(b => new LookupDto<Gender> { Id = b, DisplayName = b.ToString() })
                .ToList();

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
        #region Senkron Departman ve Doktor Seçimi Metotları
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

                }
            }
            else
            {
                FilteredDoctors.Clear();
            }
            await Task.CompletedTask;
        }
        #endregion

        private async Task LoadAppointmentRulesAsync()
        {
            var input = new GetAppointmentRulesInput
            {
                MaxResultCount = 1000,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            var result = await AppointmentRulesAppService.GetListAsync(input);
            AppointmentRules = result.Items.Select(a => new AppointmentListItem
            {
                Id = a.AppointmentRule.Id,
                DepartmentName = a.Department?.Name,
                DoctorFullName = $"{a.Doctor?.TitleName} {a.Doctor?.Name} {a.Doctor?.SurName}",
                MinAge = a.AppointmentRule?.MinAge,
                MaxAge = a.AppointmentRule?.MaxAge,
                PatientGender = a.AppointmentRule?.Gender,
                Description = a.AppointmentRule?.Description
            }).ToList();
        }
        #region Lookup Metotları
        private async Task GetDoctorCollectionLookupAsync(string? newValue = null)
        {
            DoctorsCollection = (await AppointmentRulesAppService.GetDoctorLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

        private async Task GetDepartmentCollectionLookupAsync(string? newValue = null)
        {
            DepartmentsCollection = (await AppointmentRulesAppService.GetDepartmentLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }
        #endregion

        #region Create Appointment Rule
        private void OpenAddNewDialog()
        {
            IsAddNewDialogOpen = true;
        }
       
        private async Task CreateAppointmentRule()
        {
            await HandleError(async () =>
            {
                NewAppointmentRule.DepartmentId = SelectedDepartment?.Id;
                NewAppointmentRule.DoctorId = SelectedDoctor?.Doctor.Id;
                await AppointmentRulesAppService.CreateAsync(NewAppointmentRule);

                IsAddNewDialogOpen = false; // Dialog kapat
                await LoadAppointmentRulesAsync(); // Listeyi güncelle
                await ShowToast("Appoinment Rule başarıyla eklendi.", true);
            });
            
        }
        #endregion

        private async Task DeleteAppointmentRule(Guid id)
        {
            await HandleError(async () =>
            {
                await AppointmentRulesAppService.DeleteAsync(id);
                var result = await AppointmentRulesAppService.GetListAsync(new GetAppointmentRulesInput());
                await LoadAppointmentRulesAsync(); // Listeyi güncelle
                await ShowToast("Appoinment Rule başarıyla silindi.", true);
            });
        }


        #region Tablo için veri modeli
        public class AppointmentListItem
        {
            public Guid Id { get; set; }
            public Guid? DepartmentId { get; set; }
            public Guid? DoctorId { get; set; }
            public string? DepartmentName { get; set; }
            public string? DoctorFullName { get; set; }
            public int? Age { get; set; }
            public int? MinAge { get; set; }
            public int? MaxAge { get; set; }
            public Gender? PatientGender { get; set; }
            public string? Description { get; set; }
        }
        #endregion

        #region HandleError
        public async Task HandleError(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (UserFriendlyException ex)
            {
                await ShowToast(ex.Message, false);
            }
            catch (Exception)
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
