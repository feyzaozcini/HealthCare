using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroupItems;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class Insurances
    {
        private List<InsuranceDto> InsurancesList { get; set; } = new List<InsuranceDto>();
        private GetInsurancesInput? InsurancesFilter { get; set; }

        private InsuranceCreateDto CreateInsurancesDto = new();
        private InsuranceUpdateDto UpdateInsruancesDto = new();

        private Guid? SelectedInsuranceId;

        private SfToast? ToastObj;

        private SfDialog? CreateInsurancesDialog;
        private SfDialog? UpdateInsurancesDialog;
        private SfDialog? DeleteInsurancesDialog;

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private long TotalCount;

        SfTextBox TextBoxSearchObj { get; set; }


        protected override async Task OnInitializedAsync()
        {
            InsurancesFilter = new GetInsurancesInput();
            await LoadLookupsAsync();
            await GetInsurancesAsync();
        }


        private async Task LoadLookupsAsync()
        {
            var input = new LookupRequestDto
            {
                MaxResultCount = PageSize,
                Filter = InsurancesFilter!.FilterText
            };
        }


        private async Task GetInsurancesAsync()
        {
            var input = new GetInsurancesInput
            {
                FilterText = InsurancesFilter?.FilterText,
                Name = InsurancesFilter?.Name,
                MaxResultCount = InsurancesFilter!.MaxResultCount,
                SkipCount = (CurrentPage - 1) * PageSize,
            };

            var result = await InsurancesAppService.GetListAsync(input);
            InsurancesList = result.Items.ToList();
            TotalCount = result.TotalCount;
        }


        private async Task SearchAsync(InputEventArgs args)
        {
            CurrentPage = 1;
            InsurancesFilter!.FilterText = args.Value;
            await GetInsurancesAsync();
        }


        private async Task AddInsurance()
        {
            await HandleError(async () =>
            {
                await InsurancesAppService.CreateAsync(CreateInsurancesDto);
                await LoadInsurancesAsync();
                await CloseInsuranceCreateModal();
                await GetInsurancesAsync();
                await ShowToast(InsuranceConsts.InsuranceSuccessfullyCreated, true);

            });
        }


        private async Task UpdateInsurance()
        {
            await HandleError(async () =>
            {
                await InsurancesAppService.UpdateAsync(UpdateInsruancesDto);
                await CloseInsuranceUpdateModal();
                await GetInsurancesAsync();
                await ShowToast(InsuranceConsts.InsuranceSuccessfullyUpdated, true);
            });
        }


        private async Task LoadInsurancesAsync()
        {
            var result = await InsurancesAppService.GetListAsync(new GetInsurancesInput());
            InsurancesList = result.Items.ToList();
        }


        private async Task OpenInsurancesCreateModal()
        {
            CreateInsurancesDto = new InsuranceCreateDto();
            await CreateInsurancesDialog!.ShowAsync();
        }


        private async Task OpenInsuranceUpdateModal(InsuranceDto item)
        {
            UpdateInsruancesDto = new InsuranceUpdateDto
            {
                Id = item.Id,
                Name = item.Name,
            };
            await UpdateInsurancesDialog!.ShowAsync();
        }


        private async Task OpenInsuranceDeleteModal(Guid id)
        {
            SelectedInsuranceId = id;
            await DeleteInsurancesDialog!.ShowAsync();
        }


        private async Task ConfirmInsuranceDelete()
        {
            await HandleError(async () =>
            {
                if (SelectedInsuranceId.HasValue)
                {
                    await InsurancesAppService.DeleteAsync(SelectedInsuranceId.Value);
                    await GetInsurancesAsync();
                }
                await CloseInsuranceDeleteModal();
                await ShowToast(InsuranceConsts.InsuranceSuccessfullyDeleted, true);
            });
        }


        private async Task CloseInsuranceCreateModal()
        {
            await CreateInsurancesDialog!.HideAsync();
        }


        private async Task CloseInsuranceUpdateModal()
        {
            UpdateInsruancesDto = new InsuranceUpdateDto();
            await UpdateInsurancesDialog!.HideAsync();
        }


        private async Task CloseInsuranceDeleteModal()
        {
            SelectedInsuranceId = null;
            await DeleteInsurancesDialog!.HideAsync();
        }

        #region Toast  
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
