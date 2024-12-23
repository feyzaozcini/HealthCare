using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Doctors;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Pusula.Training.HealthCare.Appointments;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class AppointmentType
    {
        private List<AppointmentTypeDto> AppointmentTypeList = new();
        private List<DoctorWithNavigationPropertiesDto> DoctorList = new();
        private AppointmentTypeCreateDto NewAppointmentType = new();
        private bool IsDoctorModalOpen = false;
        private bool IsAddModalOpen = false;
        public List<DoctorListItems> Doctors { get; set; } = new();
        private AppointmentTypeUpdateDto EditAppointmentType = new();
        private bool IsEditModalOpen = false;
        private SfToast? ToastObj;
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private static readonly MultiSelectFieldSettings DoctorDropDownFields = new()

        {
            Text = "Name", // Field to display
            Value = "Id"   // Field to use as value
        };
        protected override async Task OnInitializedAsync()
        {
            var input = new GetAppointmentTypesInput
            {
                MaxResultCount = 1000,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            
            var result = await AppointmentTypeAppService.GetListAsync(input);
            AppointmentTypeList = result.Items.ToList();
            ToastObj ??= new SfToast();
        }

        private async Task ShowDoctors(Guid appointmentTypeId)
        {
            DoctorList = await AppointmentTypeAppService.GetDoctorsByAppointmentTypeIdAsync(appointmentTypeId);
            IsDoctorModalOpen = true;
        }


        private async Task OpenAddModal()
        {
            await LoadDoctors(); // Load doctors
            NewAppointmentType = new AppointmentTypeCreateDto(); // Initialize new AppointmentType DTO
            IsAddModalOpen = true; // Open modal
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

        #region AddAppointmentType
        private void OnModalClose(bool visible)
        {
            IsDoctorModalOpen = visible;
        }

        private void OnAddModalClose(bool visible)
        {
            IsAddModalOpen = visible;
        }

        private async Task SaveAppointmentType()
        {
            await HandleError(async () =>
            {
                await AppointmentTypeAppService.CreateAsync(NewAppointmentType);
                IsAddModalOpen = false;
                var result = await AppointmentTypeAppService.GetListAsync(new GetAppointmentTypesInput());
                AppointmentTypeList = result.Items.ToList();
                await ShowToast("Appoinment Type başarıyla eklendi.", true);
            });
        }
        #endregion

        #region EditAppointmentType
        private async Task OpenEditModal(AppointmentTypeDto appointmentType)
        {
            await LoadDoctors(); // Load doctors
            EditAppointmentType = new AppointmentTypeUpdateDto
            {
                Id = appointmentType.Id,
                Name = appointmentType.Name,
                DurationInMinutes = appointmentType.DurationInMinutes,
                DoctorIds = appointmentType.DoctorAppointmentTypes
            };
            IsEditModalOpen = true;
        }

        private async Task UpdateAppointmentType()
        {
            await HandleError(async () =>
            {
                await AppointmentTypeAppService.UpdateAsync(EditAppointmentType);
                IsEditModalOpen = false;
                var result = await AppointmentTypeAppService.GetListAsync(new GetAppointmentTypesInput());
                AppointmentTypeList = result.Items.ToList();
                await ShowToast("Appoinment Type başarıyla düzenlendi.", true);
            });
            
        }

        private void OnEditModalClose(bool visible)
        {
            IsEditModalOpen = visible;
        }

        #endregion

        #region DeleteAppointmentType
        private async Task DeleteAppointmentType(Guid id)
        {
            await HandleError(async () =>
            {
                await AppointmentTypeAppService.DeleteAsync(id);
                var result = await AppointmentTypeAppService.GetListAsync(new GetAppointmentTypesInput());
                AppointmentTypeList = result.Items.ToList();

                await ShowToast("Appoinment Type başarıyla silindi.", true);
            });
        }

        #endregion

        public class DoctorListItems
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

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
