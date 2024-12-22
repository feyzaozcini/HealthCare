using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Blazor.Containers;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Handlers;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.Notes;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroupItems;
using Pusula.Training.HealthCare.Villages;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;
using Type = Pusula.Training.HealthCare.Patients.Type;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class Patients
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];
    protected PageToolbar Toolbar { get; } = new PageToolbar();
    protected bool ShowAdvancedFilters { get; set; }
    private IReadOnlyList<PatientWithNavigationPropertiesDto> PatientList { get; set; }
    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; }
    private long TotalCountLong { get; set; }
    private ProtocolDto ProtocolModel = new ProtocolDto();
    private List<LookupDto<Guid>> InsuranceList { get; set; } = new();
    private List<LookupDto<Guid>> ProtocolTypeList { get; set; } = new();
    private List<LookupDto<Guid>> ProtocolNoteList { get; set; } = new();
   
    private DepartmentDto SelectedDepartment;

    private List<DoctorWithNavigationPropertiesDto> AllDoctors = new();
    private bool IsModalVisible { get; set; }

    private List<DepartmentDto> FilteredDepartments = new();

    private List<DoctorWithNavigationPropertiesDto> FilteredDoctors = new();
    private bool CanCreatePatient { get; set; }
    private bool CanEditPatient { get; set; }
    private bool CanDeletePatient { get; set; }
    private int SelectedGenderId { get; set; } = 1;
    private int SelectedPatientTypeId { get; set; } = 1;
    private int SelectedBloodTypeId { get; set; } = 1;
    private int SelectedCompanyId { get; set; }
    private PatientCreateDto NewPatient { get; set; }
    private Validations NewPatientValidations { get; set; } = new();
    private PatientUpdateDto EditingPatient { get; set; }
    private Validations EditingPatientValidations { get; set; } = new();
    private Guid EditingPatientId { get; set; }
    private Modal CreatePatientModal { get; set; } = new();
    private Modal EditPatientModal { get; set; } = new();
    private GetPatientsInput Filter { get; set; }
    private DataGridEntityActionsColumn<PatientWithNavigationPropertiesDto> EntityActionsColumn { get; set; } = new();
    protected string SelectedCreateTab = "Patient-create-tab";
    protected string SelectedEditTab = "Patient-edit-tab";
    private IReadOnlyList<LookupDto<Guid>> CompaniesCollection { get; set; } = [];
    private IReadOnlyList<LookupDto<ProtocolStatus>> ProtocolStatusCollection { get; set; } = new List<LookupDto<ProtocolStatus>>();
    private IReadOnlyList<LookupDto<Gender>> GendersCollection { get; set; } = new List<LookupDto<Gender>>();
    private IReadOnlyList<LookupDto<Type>> TypesCollection { get; set; } = new List<LookupDto<Type>>();
    private IReadOnlyList<LookupDto<BloodType>> BloodTypesCollection { get; set; } = new List<LookupDto<BloodType>>();
    private List<PatientWithNavigationPropertiesDto> SelectedPatients { get; set; } = [];
    private List<CountryDto> CountryList { get; set; } = new List<CountryDto>();
    private GetCountriesInput? CountriesFilter { get; set; }
    private bool AllPatientsSelected { get; set; }
    private IReadOnlyList<GetCountryLookupDto<Guid>> CountriesCodeCollection { get; set; } = [];

    private int currentStep = 1; // Ýlk adým ile baþla

    private string SelectedCountryCode;

    private SfDialog? CreateProtocolsDialog;

    private ProtocolCreateDto ProtocolCreateDto = new();
    private List<ProtocolDto> ProtocolList { get; set; } = new List<ProtocolDto>();
    private GetProtocolsInput? ProtocolsFilter { get; set; }
    private Guid? SelectedPatientId { get; set; } = null;

    private DoctorWithNavigationPropertiesDto SelectedDoctor;


    public Patients()
    {
        NewPatient = new PatientCreateDto();
        EditingPatient = new PatientUpdateDto();
        Filter = new GetPatientsInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting
        };
        PatientList = [];
    }


    #region General Staff
    protected override async Task OnInitializedAsync()
    {
        NewPatient = new PatientCreateDto
        {
            Addresses = new List<AddressCreateDto>()
        };

        CountriesFilter = new GetCountriesInput();


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
            DisplayName = x.Name // Kullanýcýya görünecek sigorta adý
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
            DisplayName = x.Name // Kullanýcýya görünecek sigorta adý
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
            DisplayName = x.Text // Kullanýcýya görünecek sigorta adý
        }).ToList();


        await LoadInitialDataAsync();
        await GetCountriesAsync();
        await SetPermissionsAsync();
        await GetCountryCodeCollectionLookupAsync();
        await GetCompanyCollectionLookupAsync();
        

        ProtocolStatusCollection = Enum.GetValues(typeof(ProtocolStatus))
        .Cast<ProtocolStatus>()
        .Select(status => new LookupDto<ProtocolStatus>
        {
            Id = status,
            DisplayName = status.ToString()
        })
        .ToList();


        GendersCollection = Enum.GetValues(typeof(Gender))
       .Cast<Gender>()
       .Select(g => new LookupDto<Gender>
       {
           Id = g,
           DisplayName = g.ToString()
       })
       .ToList();


        TypesCollection = Enum.GetValues(typeof(Type))
       .Cast<Type>()
       .Select(t => new LookupDto<Type>
       {
           Id = t,
           DisplayName = t.ToString()
       })
       .ToList();


        BloodTypesCollection = Enum.GetValues(typeof(BloodType))
       .Cast<BloodType>()
       .Select(b => new LookupDto<BloodType>
       {
           Id = b,
           DisplayName = b.ToString().Replace('_', ' ')
       })
       .ToList();

    }
    

    private void NavigateToPatientDetails(PatientWithNavigationPropertiesDto selectedPatient)
    {
        // Seçilen hastayý StateService'e kaydet
        StateService.PreviousPageUrl = NavigationManager.Uri;

        StateService.SelectedPatientNavigation = selectedPatient;

        // Detay sayfasýna yönlendir
        NavigationManager.NavigateTo("/patient-details");
    }


    #endregion

    #region Protocol
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
                FullName = $"{d.Title?.Name} {d.User?.Name} {d.User?.Surname}" // FullName oluþturuldu
            })
            .ToList();
        }
        StateHasChanged();

    }

    private async Task SelectDoctorAsync()
    {
        var departmentId = ProtocolCreateDto.DepartmentId;

        if (departmentId != null)
        {
            // Seçilen departmana ait doktorlarý listelemek için
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

    }

 
    private async Task OpenProtocolCreateModal(PatientWithNavigationPropertiesDto input)
    {
        if (input == null) return;

        ProtocolCreateDto = new ProtocolCreateDto
        {
            PatientId = input.Patient.Id, // Hasta ID'sini buraya baðladýk

        };
        await CreateProtocolsDialog!.ShowAsync();
    }

    private async Task CloseProtocolCreateModal()
    {
        await CreateProtocolsDialog!.HideAsync();
    }
    #endregion

    #region Address

    private void RemoveAddress(AddressCreateDto address)
    {
        NewPatient.Addresses.Remove(address);
    }


    private void OnPrimaryChanged(AddressCreateDto selectedAddress, ChangeEventArgs e)
    {
        bool isChecked = (bool)e.Value;

        if (isChecked)
        {
            foreach (var address in NewPatient.Addresses)
            {
                if (address != selectedAddress)
                {
                    address.IsPrimary = false;
                }
            }
        }

        selectedAddress.IsPrimary = isChecked;
    }

    private void AddNewAddress()
    {
        NewPatient.Addresses.Add(new AddressCreateDto
        {
            CountryId = Guid.Empty,
            CityId = Guid.Empty,
            DistrictId = Guid.Empty,
            VillageId = Guid.Empty,
            AddressDescription = string.Empty,
            IsPrimary = NewPatient.Addresses.Count == 0
        });
    }


    private async Task GetCountriesAsync()
    {

        var input = new GetCountriesInput
        {
            FilterText = CountriesFilter!.FilterText,
            Name = CountriesFilter.Name,
            Code = CountriesFilter.Code,
            MaxResultCount = 200,
            SkipCount = (CurrentPage-1)*PageSize
        };

        var result = await countriesAppService.GetListAsync(input);
        CountryList = result.Items.ToList();
        StateHasChanged();

    }

    
    private async Task OnCountryChanged(AddressCreateDto address, ChangeEventArgs e)
    {
        // Seçilen ülke ID'sini al
        if (e.Value != null && Guid.TryParse(e.Value.ToString(), out Guid selectedCountryId))
        {
            address.CountryId = selectedCountryId;

            // Eðer "Select" öðesi (Guid.Empty) seçildiyse, veriyi sýfýrlýyoruz
            if (selectedCountryId == Guid.Empty)
            {
                // Veriyi sýfýrlýyoruz çünkü "Select" seçildi
                address.CityList.Clear();       // Baðýmsýz listeyi sýfýrla
                address.DistrictList.Clear();
                address.VillageList.Clear();

            }
            else
            {
                var cities = await cityRepository.GetListAsync(c => c.CountryId == selectedCountryId);

                // City'leri ilgili property'ye atýyoruz
                address.CityList = cities.OrderBy(c => c.Name).Select(c => new CityDto { Id = c.Id, Name = c.Name }).ToList();

                address.DistrictList.Clear();
                address.VillageList.Clear();
            }

            StateHasChanged();
        }
    }

    private async Task OnCityChanged(AddressCreateDto address, ChangeEventArgs e)
    {
        if (e.Value != null && Guid.TryParse(e.Value.ToString(), out Guid selectedCityId))
        {
            address.CityId = selectedCityId;

            if (selectedCityId == Guid.Empty)
            {
                address.DistrictList.Clear();
                address.VillageList.Clear();

            }
            else
            {
                var districts = await districtRepository.GetListAsync(d => d.CityId == selectedCityId);

                address.DistrictList = districts.OrderBy(d => d.Name).Select(d => new DistrictDto { Id = d.Id, Name = d.Name }).ToList();

                address.VillageList.Clear();

            }

            StateHasChanged();
        }
    }

    private async Task OnDistrictChanged(AddressCreateDto address, ChangeEventArgs e)
    {
        if (e.Value != null && Guid.TryParse(e.Value.ToString(), out Guid selectedDistrictId))
        {
            address.DistrictId = selectedDistrictId;

            if (selectedDistrictId == Guid.Empty)
            {
                address.VillageList.Clear();
                StateHasChanged();

            }
            else
            {
                var villages = await villageRepository.GetListAsync(v => v.DistrictId == selectedDistrictId);

                address.VillageList = villages.OrderBy(v => v.Name).Select(v => new VillageDto { Id = v.Id, Name = v.Name }).ToList();
                StateHasChanged();

            }

        }
    }

    #endregion

    #region Patient

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SetBreadcrumbItemsAsync();
            await SetToolbarItemsAsync();
            await InvokeAsync(StateHasChanged);
        }
    }


    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Patients"]));
        return ValueTask.CompletedTask;
    }


    protected virtual ValueTask SetToolbarItemsAsync()
    {
        
        Toolbar.AddButton(L["NewPatient"], OpenCreatePatientModalAsync, IconName.Add, requiredPolicyName: HealthCarePermissions.Patients.Create);
        return ValueTask.CompletedTask;
    }


    private async Task SetPermissionsAsync()
    {
        CanCreatePatient = await AuthorizationService.IsGrantedAsync(HealthCarePermissions.Patients.Create);
        CanEditPatient = await AuthorizationService.IsGrantedAsync(HealthCarePermissions.Patients.Edit);
        CanDeletePatient = await AuthorizationService.IsGrantedAsync(HealthCarePermissions.Patients.Delete);
    }
    
    private async Task GetPatientsAsync()
    {
        if (Filter == null || (string.IsNullOrEmpty(Filter.FilterText) &&
        (Filter.FirstName == null && Filter.LastName == null && Filter.IdentityNumber == null &&
        Filter.PassportNumber == null && Filter.MobilePhoneNumber == null &&
        Filter.Email == null && Filter.No == null)))
        {
            return; // Hiçbir filtre yoksa, veri getirme iþlemi yapýlmaz
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

    protected virtual async Task SearchAsync()
    {
        CurrentPage = 1;
        await GetPatientsAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task OnSearchTextChanged(ChangeEventArgs args)
    {
        if(string.IsNullOrEmpty(args?.Value?.ToString()))
        {
           
            PatientList = new List<PatientWithNavigationPropertiesDto>();
            TotalCount = 0;
            return;
        }

        Filter.FilterText = args.Value.ToString();
        await GetPatientsAsync();
    }
   

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<PatientWithNavigationPropertiesDto> e)
    {
        CurrentSorting = e.Columns
        .Where(c => c.SortDirection != SortDirection.Default)
        .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
        .JoinAsString(",");
        CurrentPage = e.Page;
        await GetPatientsAsync();
        await InvokeAsync(StateHasChanged);
    }


    private async Task OpenCreatePatientModalAsync()
    {
        NewPatient = new PatientCreateDto
        {
            BirthDate = DateTime.Now,
        };

        SelectedCreateTab = "Patient-create-tab";

        await NewPatientValidations.ClearAll();
        await CreatePatientModal.Show();
    }


    private async Task CloseCreatePatientModalAsync()
    {
        NewPatient = new PatientCreateDto
        {
            BirthDate = DateTime.Now,

            CompanyId = CompaniesCollection.Select(x => x.Id).FirstOrDefault()
        };

        await CreatePatientModal.Hide();
    }


    private async Task OpenEditPatientModalAsync(PatientWithNavigationPropertiesDto input)
    {
        SelectedEditTab = "Patient-edit-tab";

        var Patient = await PatientsAppService.GetAsync(input.Patient.Id);

        EditingPatientId = Patient.Id;
        EditingPatient = ObjectMapper.Map<PatientDto, PatientUpdateDto>(Patient);

        await EditingPatientValidations.ClearAll();
        await EditPatientModal.Show();
    }

    private async Task DeletePatientAsync(PatientWithNavigationPropertiesDto input)
    {
        await PatientsAppService.DeleteAsync(input.Patient.Id);
        await GetPatientsAsync();
    }

    private async Task CreatePatientAsync()
    {
        try
        {
            if (await NewPatientValidations.ValidateAll() == false)
            {
                return;
            }
            await PatientsAppService.CreateAsync(NewPatient);
            await GetPatientsAsync();
            await CloseCreatePatientModalAsync();
        }
        catch (Exception ex)
        {

        }
    }

    private async Task CloseEditPatientModalAsync()
    {
        await EditPatientModal.Hide();
    }

    private async Task UpdatePatientAsync()
    {
        try
        {
            if (await EditingPatientValidations.ValidateAll() == false)
            {
                return;
            }
            await PatientsAppService.UpdateAsync(EditingPatientId, EditingPatient);
            await GetPatientsAsync();
            await CloseEditPatientModalAsync();
        }
        catch (Exception ex)
        {

        }
    }


    protected virtual Task OnVillageChanged(ChangeEventArgs e)
    {
        Guid? villageId = (Guid?)e.Value;
        Filter.PrimaryVillageId = villageId;
        return Task.CompletedTask;
    }


    protected virtual void OnFirstNameChanged(string? firstName)
    {
        Filter.FirstName = firstName;
    }

    protected virtual void OnLastNameChanged(string? lastName)
    {
        Filter.LastName = lastName;
    }


    protected virtual void OnNoChanged(string no)
    {
        if (int.TryParse(no, out int parsedNo))
        {
            Filter.No = parsedNo;
        }
        else
        {
            Filter.No = null;
        }
    }


    protected virtual void OnBirthDateMinChanged(DateTime? birthDateMin)
    {
        Filter.BirthDateMin = birthDateMin.HasValue ? birthDateMin.Value.Date : birthDateMin;
    }


    protected virtual void OnBirthDateMaxChanged(DateTime? birthDateMax)
    {
        Filter.BirthDateMax = birthDateMax.HasValue ? birthDateMax.Value.Date.AddDays(1).AddSeconds(-1) : birthDateMax;
    }


    protected virtual void OnIdentityNumberChanged(string? identityNumber)
    {
        Filter.IdentityNumber = identityNumber;
    }


    protected virtual void OnPassportNumberChanged(string? passportNumber)
    {
        Filter.PassportNumber = passportNumber;
    }


    protected virtual void OnEmailChanged(string? email)
    {
        Filter.Email = email;
    }


    protected virtual void OnMobilePhoneNumberChanged(string? mobilePhoneNumber)
    {
        Filter.MobilePhoneNumber = mobilePhoneNumber;
    }


    protected virtual void OnCompanyNameChanged(Guid? companyId)
    {
            Filter.CompanyId = companyId;
 
    }


    // Butona týklandýðýnda arama yapacak metot
    protected virtual async Task OnSearchButtonClicked()
    {
        if (Filter == null || (string.IsNullOrEmpty(Filter.FilterText) &&
            (Filter.FirstName == null && Filter.LastName == null && Filter.IdentityNumber == null &&
            Filter.PassportNumber == null && Filter.MobilePhoneNumber == null &&
            Filter.Email == null && Filter.No == null)))
        {
            return; // Boþ filtrelerle veri çekme
        }

        // Filtreyi doðrula ve veriyi getir
        await GetPatientsAsync();
    }


    private Task SelectAllItems()
    {
        AllPatientsSelected = true;

        return Task.CompletedTask;
    }


    private async Task ClearSelection()
    {
        AllPatientsSelected = false;
        SelectedPatients.Clear();

        await InvokeAsync(StateHasChanged);
    }


    private Task SelectedPatientRowsChanged()
    {
        if (SelectedPatients.Count != PageSize)
        {
            AllPatientsSelected = false;
        }

        return Task.CompletedTask;
    }


    private async Task GetCountryCodeCollectionLookupAsync(string? newValue = null)
    {
        CountriesCodeCollection = (IReadOnlyList<GetCountryLookupDto<Guid>>)(await PatientsAppService.GetCountryLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
    }


    private async Task GetCompanyCollectionLookupAsync(string? newValue = null)
    {
        CompaniesCollection = (await PatientsAppService.GetCompanyLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
    }


    private async Task DeleteSelectedPatientsAsync()
    {
        var message = AllPatientsSelected ? L["DeleteAllRecords"].Value : L["DeleteSelectedRecords", SelectedPatients.Count].Value;

        if (!await UiMessageService.Confirm(message))
        {
            return;
        }

        if (AllPatientsSelected)
        {
            await PatientsAppService.DeleteAllAsync(Filter);
        }
        else
        {
            await PatientsAppService.DeleteByIdsAsync(SelectedPatients.Select(x => x.Patient.Id).ToList());
        }

        SelectedPatients.Clear();
        AllPatientsSelected = false;

        await GetPatientsAsync();
    }


    private void NextStep()
    {
        if (currentStep < 2)
        {
            currentStep++; // Bir sonraki adýma geç
        }
    }


    private void PreviousStep()
    {
        if (currentStep > 1)
        {
            currentStep--; // Bir önceki adýma dön
        }
    }
    #endregion
}
