using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.DiagnosisGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class DiagnosisList
    {
        protected List<DiagnosisGroupDto> DiagnosisGroupsList { get; set; } = new();
        protected List<DiagnosisWithNavigationPropertiesDto> IcdList { get; set; } = new();

        protected GetDiagnosisGroupsInput DiagnosisGroupsFilter { get; set; } = new();
        protected GetDiagnosisInput DiagnosisFilter { get; set; } = new();

        protected int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        protected int CurrentPage { get; set; } = 1;
        protected long TotalCount { get; set; }
        protected Guid? SelectedGroupId { get; set; } = null;

        private bool IsDeleteModalVisible { get; set; } = false;
        private bool IsEditModalVisible { get; set; } = false;
        private DiagnosisGroupCreateDto DiagnosisGroupCreateDto { get; set; } = new DiagnosisGroupCreateDto();

        private DiagnosisGroupUpdateDto EditDto { get; set; } = new DiagnosisGroupUpdateDto();
        protected bool IsAddDiagnosisGroupModalVisible { get; set; } = false;

        private DiagnosisCreateDto DiagnosisCreateDto = new DiagnosisCreateDto();

        private bool IsAddDiagnosisModalVisible = false;
        private string? SearchText { get; set; } = null;

        private Guid? SelectedDiagnosisId = null;

        private bool IsDeleteDiagnosisModalVisible = false;

        private DiagnosisUpdateDto EditDiagnosisDto = new();
        private bool IsEditDiagnosisModalVisible = false;

        //CREATE DIAGNOSIS GROUP
        #region CREATE DG



        protected void OpenAddDiagnosisGroupModal()
        {
            IsAddDiagnosisGroupModalVisible = true;
        }


        protected void CloseAddDiagnosisGroupModal()
        {
            IsAddDiagnosisGroupModalVisible = false;

            DiagnosisGroupCreateDto = new DiagnosisGroupCreateDto();
        }

        protected async Task SubmitAddDiagnosisGroup()
        {
            await DiagnosisGroupAppservice.CreateAsync(DiagnosisGroupCreateDto);

            await LoadDiagnosisGroupsAsync();

            CloseAddDiagnosisGroupModal();
        }

        #endregion

        //EDIT DIAGNOSIS GROUP
        #region EDIT DG
        private void OpenEditModal(DiagnosisGroupDto group)
        {
            EditDto = new DiagnosisGroupUpdateDto
            {
                Id = group.Id,
                Name = group.Name,
                Code = group.Code
            };

            IsEditModalVisible = true;
        }


        private void CloseEditModal()
        {

            IsEditModalVisible = false;
            EditDto = new DiagnosisGroupUpdateDto();
        }

        private async Task SubmitEdit()
        {

            var updatedGroup = await DiagnosisGroupAppservice.UpdateAsync(EditDto);


            // Listeyi tamamen yeniden yükle
            await LoadDiagnosisGroupsAsync();
            CloseEditModal();
        }

        #endregion

        //DELETE DIAGNOSIS GROUP
        #region DELETE DG
        private void OpenDeleteModal(Guid groupId)
        {
            SelectedGroupId = groupId;
            IsDeleteModalVisible = true;
        }


        private void CloseDeleteModal()
        {
            IsDeleteModalVisible = false;
            SelectedGroupId = null;
        }


        private async Task ConfirmDelete()
        {

            if (SelectedGroupId.HasValue)
            {

                await DiagnosisGroupAppservice.DeleteAsync(SelectedGroupId.Value);


                await LoadDiagnosisGroupsAsync();

                SelectedGroupId = null;

                CloseDeleteModal();
            }
            else
            {
                CloseDeleteModal();
            }
        }

        #endregion

        protected override async Task OnInitializedAsync()
        {
            await LoadDiagnosisGroupsAsync();
            await LoadIcdListAsync();
        }

        protected async Task LoadDiagnosisGroupsAsync()
        {
            var input = new GetDiagnosisGroupsInput
            {
                FilterText = DiagnosisGroupsFilter.FilterText,
                Name = DiagnosisGroupsFilter.Name,
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize
            };

            var result = await DiagnosisGroupAppservice.GetListAsync(input);
            DiagnosisGroupsList = result.Items.ToList();
        }

        protected async Task LoadIcdListAsync(string? filterText = null)
        {
            var input = new GetDiagnosisInput
            {
                FilterText = filterText,
                Name = null,
                Code = null,
                GroupId = SelectedGroupId,
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize
            };

            var result = await DiagnosisAppService.GetListAsync(input);
            IcdList = result.Items.ToList();
            TotalCount = result.TotalCount;
        }

        protected async Task FilterIcdListByGroup(Guid groupId)
        {
            SelectedGroupId = groupId;
            await LoadIcdListAsync();
        }

        private async Task SearchDiagnosis()
        {
            await LoadIcdListAsync(SearchText);
        }

        //DiagnosisCreate
        #region CREATE ICD
        protected async Task SubmitAddDiagnosis()
        {
            // Backend'e taný ekleme isteði gönder
            await DiagnosisAppService.CreateAsync(DiagnosisCreateDto);

            // Listeyi yenile
            await LoadIcdListAsync();

            // Modalý kapat
            CloseAddDiagnosisModal();
        }

        private void CloseAddDiagnosisModal()
        {
            IsAddDiagnosisModalVisible = false;
        }
        protected void OpenAddDiagnosisModal()
        {
            // Form modelini sýfýrla
            DiagnosisCreateDto = new DiagnosisCreateDto();

            // Modalý görünür yap
            IsAddDiagnosisModalVisible = true;
        }
        #endregion

        #region DELETE ICD
        // Diagnosis Silme Modalýný Aç
        protected void OpenDeleteDiagnosisModal(Guid diagnosisId)
        {
            SelectedDiagnosisId = diagnosisId;
            IsDeleteDiagnosisModalVisible = true;
        }

        // Diagnosis Silme Ýþlemini Onayla
        protected async Task ConfirmDeleteDiagnosisAsync()
        {
            if (SelectedDiagnosisId.HasValue)
            {
                // Backend'de tanýyý sil
                await DiagnosisAppService.DeleteAsync(SelectedDiagnosisId.Value);

                // Taný listesini yeniden yükle
                await LoadIcdListAsync();

                // Modalý kapat
                CloseDeleteDiagnosisModal();
            }
        }

        // Diagnosis Silme Modalýný Kapat
        protected void CloseDeleteDiagnosisModal()
        {
            IsDeleteDiagnosisModalVisible = false;
            SelectedDiagnosisId = null;
        }

        #endregion

        #region EDIT ICD

        protected void OpenEditDiagnosisModal(DiagnosisDto diagnosis)
        {
            EditDiagnosisDto = new DiagnosisUpdateDto
            {
                Id = diagnosis.Id,
                Name = diagnosis.Name,
                Code = diagnosis.Code,
                GroupId = diagnosis.GroupId // Taný grubunu deðiþmeyecek þekilde tutuyoruz
            };

            IsEditDiagnosisModalVisible = true;
        }


        protected async Task SubmitEditDiagnosis()
        {
            await DiagnosisAppService.UpdateAsync(EditDiagnosisDto);


            await LoadIcdListAsync();


            CloseEditDiagnosisModal();
        }


        protected void CloseEditDiagnosisModal()
        {
            IsEditDiagnosisModalVisible = false;
            EditDiagnosisDto = new DiagnosisUpdateDto();
        }

        #endregion
    }
}