using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.Notes;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Shared;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class PatientDetails
    {
        private List<ProtocolDto> ProtocolsList { get; set; } = new List<ProtocolDto>();
        private GetProtocolsInput? ProtocolsFilter { get; set; }

        private SfDialog? NoteDialog;
        private SfDialog? CreateProtocolsDialog;
        private SfDialog? UpdateProtocolsDialog;
        private SfDialog? DeleteProtocolsDialog;

        private Guid? SelectedProtocolId;
        private string SelectedNote = string.Empty;


        private ProtocolCreateDto ProtocolCreateDto = new();
        private ProtocolUpdateDto ProtocolUpdateDto = new();

        private List<DepartmentDto> FilteredDepartments = new();
        private List<DoctorWithNavigationPropertiesDto> FilteredDoctors = new();
        private List<LookupDto<Guid>> ProtocolTypeList { get; set; } = new();
        private List<LookupDto<Guid>> ProtocolNoteList { get; set; } = new();
        private List<LookupDto<Guid>> InsuranceList { get; set; } = new();

        private List<DoctorWithNavigationPropertiesDto> AllDoctors = new();
        private IReadOnlyList<LookupDto<ProtocolStatus>> ProtocolStatusCollection { get; set; } = new List<LookupDto<ProtocolStatus>>();

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;

        private long TotalCount;

        private bool ShowTable = true;


        #region General Staff
        protected override async Task OnInitializedAsync()
        {
            //Eğer patient detay sayfasında sayfa yenilenirse SelectedPatientNavigation bir patient id tutamayacağından hata fırlatır,
            //bu yüzden hata fırlatmasın diye /patients sayfasına yönlendiriliyor.
            if (PatientStateService?.SelectedPatientNavigation?.Patient?.Id == null)
            {
                // Hata durumunda yönlendirme
                NavigationManager.NavigateTo("/patients");
                return;
            }

            ProtocolsFilter = new GetProtocolsInput(
                filterText: null,
                startTime: null,
                endTime: null,
                no: null,
                protocolStatus: null,
                protocolTypeId: null,
                protocolNoteId: null,
                protocolInsuranceId: null,
                patientId: PatientStateService.SelectedPatientNavigation.Patient.Id,
                departmentId: null,
                doctorId: null,
                currentPage: CurrentPage,
                pageSize: PageSize
            );

            await GetProtocolsAsync();
            await LoadInitialDataAsync();

            var insurances = await InsurancesAppService.GetListAsync(new GetInsurancesInput
            {
                FilterText = "",
                Name = null,
                Sorting = "Name asc",
                MaxResultCount = 100,
                SkipCount = 0
            });

            InsuranceList = insurances.Items.Select(x => new LookupDto<Guid>
            {
                Id = x.Id,
                DisplayName = x.Name // Kullanıcıya görünecek sigorta adı
            }).ToList();


            var protocolTypes = await ProtocolTypesAppService.GetListAsync(new GetProtocolTypesInput
            {
                FilterText = "",
                Name = null,
                Sorting = "Name asc",
                MaxResultCount = 100,
                SkipCount = 0
            });

            ProtocolTypeList = protocolTypes.Items.Select(x => new LookupDto<Guid>
            {
                Id = x.Id,
                DisplayName = x.Name // Kullanıcıya görünecek sigorta adı
            }).ToList();

            var protocolNotes = await NotesAppService.GetListAsync(new GetNotesInput
            {
                FilterText = "",
                Text = null,
                Sorting = "Text asc",
                MaxResultCount = 100,
                SkipCount = 0
            });

            ProtocolNoteList = protocolNotes.Items.Select(x => new LookupDto<Guid>
            {
                Id = x.Id,
                DisplayName = x.Text // Kullanıcıya görünecek sigorta adı
            }).ToList();

            ProtocolStatusCollection = Enum.GetValues(typeof(ProtocolStatus))
            .Cast<ProtocolStatus>()
            .Select(status => new LookupDto<ProtocolStatus>
            {
                Id = status,
                DisplayName = status.ToString()
            })
            .ToList();
        }


        private async Task GetProtocolsAsync()
        {
            if (ProtocolsFilter == null || PatientStateService?.SelectedPatientNavigation?.Patient?.Id == null)
                return;

            var result = await ProtocolsAppService.GetListAsync(ProtocolsFilter);
            ProtocolsList = result.Items.ToList();
            TotalCount = result.TotalCount;

            StateHasChanged();
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
            StateHasChanged();
        }


        private async Task OnInputChange(InputEventArgs args)
        {
            ProtocolsFilter!.FilterText = args.Value;
            await GetProtocolsAsync();
        }


        private async Task OpenNoteModal(string? description)
        {
            SelectedNote = description ?? "Açıklama mevcut değil.";
            await NoteDialog!.ShowAsync();
        }


        private async Task CloseNoteModal()
        {
            SelectedNote = string.Empty;
            await NoteDialog!.HideAsync();
        }


        private void NavigateBack()
        {
            if (!string.IsNullOrEmpty(StateService.PreviousPageUrl))
            {
                //Patient'ta filtrelense olsa bile url değişmediği için için otomatik olarak /patients sayfasına yönlendiriliyor zorunlu olarak 
                //ama yapı değişirse ilerde ve url alanı filtrelemeye yapıldıgında o filtrelemeye göre değişirse mesela Ali Ozturk u filtrelersek /patients/Ali-Ozturk gibi
                // PreviousPageUrl yapısı sayesinde filtrelenmiş önceki haline yönlendirilecektir. Kısacası İlerdeki yapıya uygun genişletilebilir bir kullanıldı.
                NavigationManager.NavigateTo(StateService.PreviousPageUrl);
            }
            else
            {
                // Önceki URL yoksa varsayılan bir sayfaya yönlendir
                NavigationManager.NavigateTo("/patients");
            }
        }

        #endregion

        #region Protocol Create

        private async Task SelectDoctorAsync()
        {
            var departmentId = ProtocolCreateDto.DepartmentId;

            if (departmentId != null)
            {
                // Seçilen departmana ait doktorları listelemek için
                var departmentWithDoctors = await DepartmentsAppService.GetDoctorsByDepartmentIdAsync(departmentId);
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

                StateHasChanged();
            }
            else
            {
                FilteredDoctors.Clear();
            }
            await Task.CompletedTask;
        }


        private async Task AddNewProtocol()
        {

            await ProtocolsAppService.CreateAsync(ProtocolCreateDto);
            await CloseProtocolCreateModal();
            await GetProtocolsAsync();
        }


        private async Task OpenProtocolCreateModal(PatientWithNavigationPropertiesDto input)
        {
            if (input?.Patient == null) return;

            ProtocolCreateDto = new ProtocolCreateDto
            {
                PatientId = input.Patient.Id, // Hasta ID'sini buraya bağladık

            };
            await CreateProtocolsDialog!.ShowAsync();
        }


        private async Task CloseProtocolCreateModal()
        {
            await CreateProtocolsDialog!.HideAsync();
        }

        #endregion

        #region Protocol Update

        private async Task UpdateProtocol()
        {

            await ProtocolsAppService.UpdateAsync(ProtocolUpdateDto.Id, ProtocolUpdateDto);
            await CloseProtocolUpdateModal();
            await GetProtocolsAsync();
        }


        private async Task OpenProtocolUpdateModal(ProtocolDto protocol)
        {
            ProtocolUpdateDto = new ProtocolUpdateDto
            {
                Id = protocol.Id,
                PatientId = protocol.PatientId, // Buraya dikkat edin
                DepartmentId = protocol.DepartmentId,
                DoctorId = protocol.DoctorId,
                ProtocolTypeId = protocol.ProtocolTypeId,
                ProtocolInsuranceId = protocol.ProtocolInsuranceId,
                ProtocolStatus = protocol.ProtocolStatus,
                NoteText = protocol.NoteText,
                StartTime = protocol.StartTime,
                EndTime = protocol.EndTime
            };
            await LoadInitialDataAsync();
            await SelectDoctorUpdateAsync();

            await UpdateProtocolsDialog!.ShowAsync();
        }


        private async Task SelectDoctorUpdateAsync()
        {
            var departmentId = ProtocolUpdateDto?.DepartmentId; // Guid?

            if (departmentId.HasValue) // Nullable kontrolü
            {
                var departmentWithDoctors = await DepartmentsAppService.GetDoctorsByDepartmentIdAsync(departmentId.Value);
                if (departmentWithDoctors != null)
                {
                    FilteredDoctors = departmentWithDoctors
                        .Select(dd => new DoctorWithNavigationPropertiesDto
                        {
                            Doctor = dd.Doctor,
                            Title = dd.Title,
                            User = dd.User,
                            FullName = $"{dd.Title?.Name} {dd.User?.Name} {dd.User?.Surname}"
                        })
                        .ToList();
                }
                else
                {
                    FilteredDoctors.Clear();
                }

                StateHasChanged();
            }
            else
            {
                FilteredDoctors.Clear();
            }

            await Task.CompletedTask;
        }


        private async Task CloseProtocolUpdateModal()
        {
            ProtocolUpdateDto = new ProtocolUpdateDto();
            await UpdateProtocolsDialog!.HideAsync();
        }

        #endregion

        #region Protocol Delete

        private async Task ConfirmProtocolDelete()
        {
            if (SelectedProtocolId.HasValue)
            {
                await ProtocolsAppService.DeleteAsync(SelectedProtocolId.Value);
                await GetProtocolsAsync();
            }
            await CloseProtocolDeleteModal();
        }


        private async Task CloseProtocolDeleteModal()
        {
            SelectedProtocolId = null;
            await DeleteProtocolsDialog!.HideAsync();
        }


        private async Task OpenProtocolDeleteModal(Guid id)
        {
            SelectedProtocolId = id;
            await DeleteProtocolsDialog!.ShowAsync();
        }

        #endregion 
    }
}
