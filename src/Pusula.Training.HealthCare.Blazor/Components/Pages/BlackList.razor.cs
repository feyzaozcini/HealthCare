using Pusula.Training.HealthCare.BlackLists;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Syncfusion.Blazor.Notifications;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class BlackList
    {
        private List<BlackListDto> BlackListList = new();
        private List<DoctorWithNavigationPropertiesDto> DoctorList = new();
        private List<PatientDto> PatientList = new();
        private bool IsAddModalOpen = false;
        private BlackListCreateDto NewBlackList = new();
        private BlackListUpdateDto EditBlackList = new();
        private bool IsEditModalOpen = false;
        public List<DoctorListItems> Doctors { get; set; } = new();
        public List<PatientListItems> Patients { get; set; } = new();
        public List<BlackListItem> BlackListItems { get; set; } = new();
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private BlackListItem? SelectedBlackList;
        private BlackListStatus SelectedBlackListStatus;
        private IReadOnlyList<LookupDto<BlackListStatus>> BlackListStatusCoolection { get; set; } = new List<LookupDto<BlackListStatus>>();
        private SfToast? ToastObj;

        protected override async Task OnInitializedAsync()
        {
            await LoadBlackListAsync();
            await LoadDoctors();
            await LoadPatientAsync();
            BlackListStatusCoolection = Enum.GetValues(typeof(BlackListStatus))
                    .Cast<BlackListStatus>()
                    .Select(b => new LookupDto<BlackListStatus> { Id = b, DisplayName = b.ToString() })
                    .ToList();
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

        private async Task LoadPatientAsync()
        {
            var result = await PatientsAppService.GetListAsync(new GetPatientsInput());

            Patients = result.Items.Select(d => new PatientListItems
            {
                Id = d.Patient.Id, // Patient ID
                Name = $"{d.Patient.FirstName} {d.Patient.LastName}" // Full name
            }).ToList();
        }

        private async Task LoadBlackListAsync()
        {
            var input = new GetBlackListInput
            {
                MaxResultCount = 1000,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            var blackLists = await BlackListAppService.GetListAsync(input);
            BlackListItems = blackLists.Items.Select(a => new BlackListItem
            {
                Id = a.BlackList.Id,
                DoctorFullName = $"{a.Doctor.TitleName} {a.Doctor.Name} {a.Doctor.SurName}",
                DoctorId = a.Doctor.Id,
                PatientFullName = $"{a.Patient.FirstName} {a.Patient.LastName}",
                PatientId = a.Patient.Id,
                BlackListStatus = a.BlackList.BlackListStatus,
                Note = a.BlackList.Note
            }).ToList();
        }
        private void OpenAddModal()
        {
            NewBlackList = new BlackListCreateDto();
            IsAddModalOpen = true;
        }

        private async Task SaveBlackList()
        {
            await HandleError(async () =>
            {
                await BlackListAppService.CreateAsync(NewBlackList);
                IsAddModalOpen = false;
                await LoadBlackListAsync();

                // Başarılı mesaj
                await ShowToast("Kara Listeye başarıyla eklendi!", true);
            });
            
        }

        private void OpenEditDialog(BlackListItem blackList)
        {
            SelectedBlackList = blackList;
            SelectedBlackListStatus = blackList.BlackListStatus;
            IsEditModalOpen = true;

        }

        private async Task UpdateBlackListStatus()
        {
            if (SelectedBlackList == null) return;

            var updateDto = new BlackListUpdateDto
            {
                Id = SelectedBlackList.Id,
                DoctorId = SelectedBlackList.DoctorId,
                PatientId = SelectedBlackList.PatientId,
                BlackListStatus = SelectedBlackListStatus,
                Note = SelectedBlackList.Note
            };

            await BlackListAppService.UpdateAsync(updateDto);
            await LoadBlackListAsync(); 
            IsEditModalOpen = false;
        }

        private void CloseDialog()
        {
            IsEditModalOpen = false;
            SelectedBlackList = null;
        }

        public class DoctorListItems
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class PatientListItems
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class BlackListItem
        {
            public Guid Id { get; set; }
            public Guid DoctorId { get; set; }
            public Guid PatientId { get; set; }
            public string DoctorFullName { get; set; }
            public string PatientFullName { get; set; }
            public BlackListStatus BlackListStatus { get; set; }
            public string Note { get; set; }

        }

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
