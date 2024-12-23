using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.DoctorWorkSchedules;
using Syncfusion.Blazor.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Pusula.Training.HealthCare.AppointmentRules;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class DoctorWorkSchedule
    {
        public List<DoctorListItems> Doctors { get; set; } = new();
        public List<DoctorWorkSchedulerListItem> DoctorWorkSchedulers { get; set; } = new();
        private List<DoctorWorkScheduleWithNavigationPropertiesDto> DoctorWorkScheduleList = new();
        private DoctorWorkScheduleCreateDto NewDoctorWorkSchedule = new();
        private DoctorWorkScheduleUpdateDto EditDoctorWorkSchedule = new();
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private bool IsAddModalOpen = false;
        private bool IsEditModalOpen = false;
        private SfToast? ToastObj;
        public int[] WorkingDays { get; set; } = Array.Empty<int>();
        private List<object> WeekDays = new()
        {
            new { Text = "Pazartesi", Value = 1 },
            new { Text = "Salı", Value = 2 },
            new { Text = "Çarşamba", Value = 3 },
            new { Text = "Perşembe", Value = 4 },
            new { Text = "Cuma", Value = 5 },
            new { Text = "Cumartesi", Value = 6 },
            new { Text = "Pazar", Value = 7 }
        };
        protected override async Task OnInitializedAsync()
        {
            await LoadDoctors();
            await LoadDoctorWorkSchedurlerAsync();
            ToastObj ??= new SfToast();
        }
        private async Task LoadDoctors()
        {
            var result = await DoctorsAppService.GetListAsync(new GetDoctorsInput());

            Doctors = result.Items.Select(d => new DoctorListItems
            {
                Id = d.Doctor.Id, // Doctor ID
                Name = $"{d.Doctor.TitleName} {d.Doctor.Name} {d.Doctor.SurName}" // Full name
            }).ToList();
        }
        private async Task LoadDoctorWorkSchedurlerAsync()
        {
            var input = new GetDoctorWorkSchedulesInput
            {
                MaxResultCount = 1000,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            var doctorWorkSchedules = await DoctorWorkScheduleAppService.GetListAsync(input);
            DoctorWorkSchedulers = doctorWorkSchedules.Items.Select(a => new DoctorWorkSchedulerListItem
            {
                Id = a.DoctorWorkSchedule.Id,
                DoctorFullName = $"{a.Doctor.TitleName} {a.Doctor.Name} {a.Doctor.SurName}",
                DoctorId = a.Doctor.Id,
                WorkingDays = a.DoctorWorkSchedule.WorkingDays,
                StartHour = a.DoctorWorkSchedule.StartHour,
                EndHour = a.DoctorWorkSchedule.EndHour
            }).ToList();
        }

        #region AddDoctorWorkSchedule

        private void OpenAddModal()
        {
            NewDoctorWorkSchedule = new DoctorWorkScheduleCreateDto();
            IsAddModalOpen = true;
        }

        private async Task SaveAppointmentType()
        {
            await HandleError(async () =>
            {
                await DoctorWorkScheduleAppService.CreateAsync(NewDoctorWorkSchedule);
                IsAddModalOpen = false;
                await LoadDoctorWorkSchedurlerAsync();

                await ShowToast("Doctor Work Schedule başarıyla eklendi.", true);
            });
        }
        #endregion

        #region EditDoctorWorkSchedule

        private async Task OpenEditModal(DoctorWorkSchedulerListItem doctorWorkSchedule)
        {
            await LoadDoctors(); // Load doctors
            EditDoctorWorkSchedule = new DoctorWorkScheduleUpdateDto
            {
                Id = doctorWorkSchedule.Id,
                DoctorId = doctorWorkSchedule.DoctorId,
                WorkingDays = doctorWorkSchedule.WorkingDays,
                StartHour = doctorWorkSchedule.StartHour,
                EndHour = doctorWorkSchedule.EndHour
            };
            IsEditModalOpen = true;
        }

        private async Task UpdateDoctorWorkSchedule()
        {
            await HandleError(async () =>
            {
                await DoctorWorkScheduleAppService.UpdateAsync(EditDoctorWorkSchedule);
                IsEditModalOpen = false;
                await LoadDoctorWorkSchedurlerAsync();
                await ShowToast("Doctor Work Schedule başarıyla düzenlendi.", true);
            });
        }

        private void OnEditModalClose(bool visible)
        {
            IsEditModalOpen = visible;
        }
        #endregion

        #region DeleteDoctorWorkSchedule

        private async Task DeleteDoctorWorkSchedule(Guid id)
        {
            await HandleError(async () =>
            {
                await DoctorWorkScheduleAppService.DeleteAsync(id);
                await LoadDoctorWorkSchedurlerAsync();
                await ShowToast("Doctor Work Schedule başarıyla silindi.", true);
            });
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

        public class DoctorListItems
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class DoctorWorkSchedulerListItem
        {
            public Guid Id { get; set; }
            public Guid DoctorId { get; set; }
            public string DoctorFullName { get; set; }
            public int[] WorkingDays { get; set; }
            public string StartHour { get; set; }
            public string EndHour { get; set; }
        }
    }
}
