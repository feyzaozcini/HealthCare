using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
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
    private bool CanCreatePatient { get; set; }
    private bool CanEditPatient { get; set; }
    private bool CanDeletePatient { get; set; }
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
    private IReadOnlyList<LookupDto<Guid>> CompaniesCollection { get; set; } = [];
    private IReadOnlyList<LookupDto<Gender>> GendersCollection { get; set; } = new List<LookupDto<Gender>>();
    private IReadOnlyList<LookupDto<Type>> TypesCollection { get; set; } = new List<LookupDto<Type>>();
    private IReadOnlyList<LookupDto<BloodType>> BloodTypesCollection { get; set; } = new List<LookupDto<BloodType>>();
    private List<PatientWithNavigationPropertiesDto> SelectedPatients { get; set; } = [];
    private bool AllPatientsSelected { get; set; }

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

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetCountryCollectionLookupAsync();
        
        GendersCollection = Enum.GetValues(typeof(Gender))
       .Cast<Gender>()
       .Select(g => new LookupDto<Gender>
       {
           Id = g,
           DisplayName = g.ToString()
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
        Filter.MaxResultCount = PageSize;
        Filter.SkipCount = (CurrentPage - 1) * PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await PatientsAppService.GetListAsync(Filter);
        PatientList = (IReadOnlyList<PatientWithNavigationPropertiesDto>)result.Items;
        TotalCount = (int)result.TotalCount;


        await ClearSelection();
    }

    protected virtual async Task SearchAsync()
    {
        CurrentPage = 1;
        await GetPatientsAsync();
        await InvokeAsync(StateHasChanged);
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

            CountryId = CountriesCollection.Select(x => x.Id).FirstOrDefault(),

            CompanyId = CompaniesCollection.Select(x => x.Id).FirstOrDefault()
        };

        SelectedCreateTab = "Patient-create-tab";

        await NewPatientValidations.ClearAll();
        await CreatePatientModal.Show();
    }

    private async Task CloseCreatePatientModalAsync()
    {
        NewPatient = new PatientCreateDto
        {

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

    protected virtual async Task OnFirstNameChangedAsync(string? firstName)
    {
        Filter.FirstName = firstName;
        await SearchAsync();
    }
    protected virtual async Task OnLastNameChangedAsync(string? lastName)
    {
        Filter.LastName = lastName;
        await SearchAsync();
    }
    protected virtual async Task OnNoChangedAsync(int? no)
    {
        Filter.No = no;
        await SearchAsync();
    }
    protected virtual async Task OnBirthDateMinChangedAsync(DateTime? birthDateMin)
    {
        Filter.BirthDateMin = birthDateMin.HasValue ? birthDateMin.Value.Date : birthDateMin;
        await SearchAsync();
    }
    protected virtual async Task OnBirthDateMaxChangedAsync(DateTime? birthDateMax)
    {
        Filter.BirthDateMax = birthDateMax.HasValue ? birthDateMax.Value.Date.AddDays(1).AddSeconds(-1) : birthDateMax;
        await SearchAsync();
    }
    protected virtual async Task OnIdentityNumberChangedAsync(string? identityNumber)
    {
        Filter.IdentityNumber = identityNumber;
        await SearchAsync();
    }
    //protected virtual async Task OnEmailAddressChangedAsync(string? emailAddress)
    //{
    //    Filter.Email = emailAddress;
    //    await SearchAsync();
    //}
    protected virtual async Task OnPassportNumberChangedAsync(string? passportNumber)
    {
        Filter.PassportNumber = passportNumber;
        await SearchAsync();
    }
    protected virtual async Task OnMobilePhoneNumberChangedAsync(string? mobilePhoneNumber)
    {
        Filter.MobilePhoneNumber = mobilePhoneNumber;
        await SearchAsync();
    }
    protected virtual async Task OnFatherNameChangedAsync(string? fatherName)
    {
        Filter.FatherName = fatherName;
        await SearchAsync();
    }
    protected virtual async Task OnHomePhoneNumberChangedAsync(string? homePhoneNumber)
    {
        Filter.MobilePhoneNumber = homePhoneNumber;
        await SearchAsync();
    }
    
    private Task SelectAllItems()
    {
        AllPatientsSelected = true;

        return Task.CompletedTask;
    }

    private Task ClearSelection()
    {
        AllPatientsSelected = false;
        SelectedPatients.Clear();

        return Task.CompletedTask;
    }

    private Task SelectedPatientRowsChanged()
    {
        if (SelectedPatients.Count != PageSize)
        {
            AllPatientsSelected = false;
        }

        return Task.CompletedTask;
    }

    private async Task GetCountryCollectionLookupAsync(string? newValue = null)
    {
        CountriesCollection = (await PatientsAppService.GetCountryLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
    }

    private async Task GetCompanyCollectionLookupAsync(string? newValue = null)
    {
        CompaniesCollection = (await PatientsAppService.GetCountryLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
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
