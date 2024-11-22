using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Handlers;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;
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

    private IReadOnlyList<LookupDto<Guid>> CountriesCollection { get; set; } = [];
    private IReadOnlyList<LookupDto<Guid>> CitiesCollection { get; set; } = [];
    private IReadOnlyList<LookupDto<Guid>> DistrictsCollection { get; set; } = [];
    private IReadOnlyList<LookupDto<Guid>> VillagesCollection { get; set; } = [];
    private IReadOnlyList<LookupDto<Guid>> CompaniesCollection { get; set; } = [];
    private IReadOnlyList<LookupDto<Gender>> GendersCollection { get; set; } = new List<LookupDto<Gender>>();
    private IReadOnlyList<LookupDto<Type>> TypesCollection { get; set; } = new List<LookupDto<Type>>();
    private IReadOnlyList<LookupDto<BloodType>> BloodTypesCollection { get; set; } = new List<LookupDto<BloodType>>();
    private List<PatientWithNavigationPropertiesDto> SelectedPatients { get; set; } = [];

    private List<CountryDto> CountryList { get; set; } = new List<CountryDto>();
    private GetCountriesInput? CountriesFilter { get; set; }
    private bool AllPatientsSelected { get; set; }
    private IReadOnlyList<GetCountryLookupDto<Guid>> CountriesCodeCollection { get; set; } = [];

    private string SelectedCountryCode;

    

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

    private void NavigateToAppointments()
    {
        if (SelectedPatients.Count == 1)
        {
            StateService.SelectedPatient = SelectedPatients.First().Patient;
            NavigationManager.NavigateTo("/dashboard");
        }
    }

    private async Task GetCountriesAsync()
    {

        var input = new GetCountriesInput
        {
            FilterText = CountriesFilter!.FilterText,
            Name = CountriesFilter.Name,
            Code = CountriesFilter.Code,
            Sorting = CountriesFilter.Sorting,
            MaxResultCount = 200,
            SkipCount = (CurrentPage-1)*PageSize
        };

        var result = await countriesAppService.GetListAsync(input);
        CountryList = result.Items.ToList();
    }
    private async Task OnCountryChanged(ChangeEventArgs e)
    {
        // Seçilen ülke ID'sini al
        if (e.Value != null && Guid.TryParse(e.Value.ToString(), out Guid selectedCountryId))
        {
            // Eðer "Select" öðesi (Guid.Empty) seçildiyse, veriyi sýfýrlýyoruz
            if (selectedCountryId == Guid.Empty)
            {
                // Veriyi sýfýrlýyoruz çünkü "Select" seçildi
                CitiesCollection = new List<LookupDto<Guid>>();
                DistrictsCollection = new List<LookupDto<Guid>>(); // Ýlçeleri sýfýrla
                VillagesCollection = new List<LookupDto<Guid>>();
                SelectedCountryCode = null;  // Baþlangýçta boþ býrakýyoruz

                // UI'nin yeniden render edilmesini saðlýyoruz
                StateHasChanged();
                return;
            }

            // Seçilen ülkeyi CountriesCodeCollection içinden bul
            var selectedCountry = CountriesCodeCollection.FirstOrDefault(c => c.Id == selectedCountryId);

            if (selectedCountry != null)
            {
                // Ülkeye baðlý city'leri alýyoruz
                var cities = await cityRepository.GetListAsync(c => c.CountryId == selectedCountryId);

                // City'leri ilgili property'ye atýyoruz
                CitiesCollection = cities.Select(c => new LookupDto<Guid> { Id = c.Id, DisplayName = c.Name }).ToList();

                // Seçilen ülke kodunu güncelliyoruz
                SelectedCountryCode = selectedCountry.Code;

                // UI'nin yeniden render edilmesi için
                StateHasChanged();
            }
        }
    }

    private async Task OnCityChanged(ChangeEventArgs e)
    {
        // Seçilen ülke ID'sini al
        if (e.Value != null && Guid.TryParse(e.Value.ToString(), out Guid selectedCityId))
        {
                // Ülkeye baðlý city'leri alýyoruz
                var districts = await districtRepository.GetListAsync(d => d.CityId == selectedCityId);

                // City'leri ilgili property'ye atýyoruz
                DistrictsCollection = districts.Select(d => new LookupDto<Guid> { Id = d.Id, DisplayName = d.Name }).ToList();

                // UI'nin yeniden render edilmesi için
                StateHasChanged();
        }
    }

    private async Task OnDistrictChanged(ChangeEventArgs e)
    {
        // Seçilen ülke ID'sini al
        if (e.Value != null && Guid.TryParse(e.Value.ToString(), out Guid selectedDistrictId))
        {
            // Ülkeye baðlý city'leri alýyoruz
            var villages = await villageRepository.GetListAsync(v => v.DistrictId == selectedDistrictId);

            // City'leri ilgili property'ye atýyoruz
            VillagesCollection = villages.Select(v => new LookupDto<Guid> { Id = v.Id, DisplayName = v.Name }).ToList();

            // UI'nin yeniden render edilmesi için
            StateHasChanged();
        }
    }


    protected override async Task OnInitializedAsync()
    {
        await Task.WhenAll(
       SetPermissionsAsync(),
       GetCountryCodeCollectionLookupAsync(),
       GetCompanyCollectionLookupAsync()
   );

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

        //Filter = FilterValidator.ValidateFilters(Filter);

        //if (Filter == null) return;

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


    /*private async Task DownloadAsExcelAsync()
    {
        var token = (await PatientsAppService.GetDownloadTokenAsync()).Token;
        var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("HealthCare") ?? await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
        var culture = CultureInfo.CurrentUICulture.Name ?? CultureInfo.CurrentCulture.Name;
        if (!culture.IsNullOrEmpty())
        {
            culture = "&culture=" + culture;
        }
        await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
        NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/app/Patients/as-excel-file?DownloadToken={token}&FilterText={HttpUtility.UrlEncode(Filter.FilterText)}{culture}&Name={HttpUtility.UrlEncode(Filter.FirstName)}", forceLoad: true);
    }*/

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

            PassportNumber = "",

            CountryId = CountriesCodeCollection.Select(x => x.Id).FirstOrDefault(),

            //CompanyId = CompaniesCollection.Select(x => x.Id).FirstOrDefault()
        };

        var selectedCountry = CountriesCodeCollection.FirstOrDefault(c => c.Id == NewPatient.CountryId);
        if (selectedCountry != null)
        {
            SelectedCountryCode = selectedCountry.Code;
        }

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
    //protected virtual Task OnCityChanged(ChangeEventArgs e)
    //{
    //    Guid? cityId = (Guid?)e.Value;
    //    Filter.CityId = cityId;
    //    return Task.CompletedTask;
    //}

    //protected virtual Task OnDistrictChanged(ChangeEventArgs e)
    //{
    //    Guid? districtId = (Guid?)e.Value;
    //    Filter.DistrictId = districtId;
    //    return Task.CompletedTask;
    //}

    protected virtual Task OnVillageChanged(ChangeEventArgs e)
    {
        Guid? villageId = (Guid?)e.Value;
        Filter.VillageId = villageId;
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
}
