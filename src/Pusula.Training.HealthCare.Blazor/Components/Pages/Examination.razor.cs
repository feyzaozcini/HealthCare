using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.ControlNotes;
using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.FallRisks;
using Pusula.Training.HealthCare.FamilyHistories;
using Pusula.Training.HealthCare.FollowUpPlans;
using Pusula.Training.HealthCare.PainDetails;
using Pusula.Training.HealthCare.PatientHistories;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.PhysicalExaminations;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.PshychologicalStates;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.SystemChecks;
using Pusula.Training.HealthCare.TestGroups;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class Examination
    {
        [Parameter]
        public Guid ProtocolId { get; set; }

        [Parameter]
        public Guid PatientId { get; set; }

        private PatientDto Patient { get; set; } = new();

        private ProtocolDto protocolDto { get; set; } = new();

        #region Toast , Dialog and Error handling

        private SfToast? ToastObj;

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

        private bool IsErrorModalOpen { get; set; }
        private List<string> ValidationMessages { get; set; } = new List<string>();
        private async Task ShowErrorModal(string errors)
        {
            ValidationMessages = errors.Split("<br>").ToList();
            IsErrorModalOpen = true;
            await InvokeAsync(StateHasChanged);
        }

        private void CloseErrorModal()
        {
            IsErrorModalOpen = false;
        }
        private async Task ShowValidationErrors(IList<ValidationResult> validationErrors)
        {
            // Tüm hata mesajlarýný birleþtirip kullanýcýya gösteriyoruz
            var errors = string.Join("<br>", validationErrors.Select(v => v.ErrorMessage));
            await ShowErrorModal(errors);
        }
        public async Task HandleError(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Volo.Abp.Validation.AbpValidationException ex) // AbpValidationException'ý yakalýyoruz
            {
               
                await ShowValidationErrors(ex.ValidationErrors);
                // Sistem sorgusu modali validasyon hatasi geldiginde kapanmamali
              
            }
            catch (UserFriendlyException ex)
            {
                await ShowErrorModal(ex.Message);
                
            }
            catch (Exception)
            {
                await ShowToast("Bir hata oluþtu. Lütfen tekrar deneyin.", false);
            }
        }
        //public async Task HandleError(Func<Task> action)
        //{
        //    try
        //    {
        //        await action();
        //    }
        //    catch (UserFriendlyException ex)
        //    {

        //        await ShowToast(ex.Message, false);
        //    }
        //    catch (Exception)
        //    {

        //       await ShowToast("Bir hata oluþtu. Lütfen tekrar deneyin.", false);
        //    }
        //}
        #endregion
        protected override async Task OnInitializedAsync()
        {
            // Hasta bilgilerini çek
            Patient = await PatientsAppService.GetAsync(PatientId);
            await LoadAnamnesisAsync();
            await LoadFallRiskAsync();
            await LoadPhysicalExaminationAsync();
            await LoadPainTypesAsync();
            await LoadPainDetailAsync();
            await LoadIcdListAsync();
            await LoadExaminationDiagnosesAsync();
            await GetProtocolAsync();
            await LoadSystemCheckAsync();
            await LoadFollowUpPlanAsync();
            await LoadFamilyHistoryAsync();
            await LoadControlNotesAsync();
            await LoadPsychologicalStateAsync();
            await LoadPatientHistoryAsync();

            ToastObj ??= new SfToast();

            EducationLevelsCollection = Enum.GetValues(typeof(EducationLevel))
                .Cast<EducationLevel>()
                .Select(b => new LookupDto<EducationLevel> { Id = b, DisplayName = b.ToString() })
                .ToList();
        }

        private async Task GetProtocolAsync()
        {
           protocolDto = await ProtocolAppService.GetWithNavigationPropertiesAsync(ProtocolId);

        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }

        #region ANAMNESIS

        private AnamnesisCreateDto anamnesisCreateDto = new();
        private AnamnesisDto anamnesisDto = new();
        private bool isExistingAnamnesis;


        private int durationValue = 0; 
        private string selectedUnit = "Gün"; // Varsayýlan birim
        

        private async Task SaveAnamnesisAsync()
        {
            try
            {
                if (isExistingAnamnesis)
                {
                    await UpdateAnamnesisAsync();

                }
                else
                {
                    await CreateAnamnesisAsync();
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Kaydetme iþlemi sýrasýnda hata oluþtu: {ex.Message}");
            }
        }

        #region Create
        private async Task CreateAnamnesisAsync()
        {
            await HandleError(async () =>
            {
                var createDto = new AnamnesisCreateDto
                {
                    Complaint = anamnesisDto.Complaint,
                    StartDate=anamnesisDto.StartDate,
                    Story = anamnesisDto.Story,
                    ProtocolId = ProtocolId
                };

                await AnamnesisAppService.CreateAsync(createDto);
                await ShowToast("Ýþlem Baþarýlý", true);
            });
        }
        #endregion

        #region UPDATE
        private async Task UpdateAnamnesisAsync()
        {
            await HandleError(async () =>
            {
                var updateDto = new AnamnesisUpdateDto
                {
                    Id = anamnesisDto.Id,
                    Complaint = anamnesisDto.Complaint,
                    StartDate = anamnesisDto.StartDate,
                    Story = anamnesisDto.Story,
                    ProtocolId = ProtocolId
                };

                await AnamnesisAppService.UpdateAsync(updateDto);
                await ShowToast("Ýþlem Baþarýlý", true);
            });
        }
        #endregion
        private async Task LoadAnamnesisAsync()
        {
            try
            {
                var result = await AnamnesisAppService.GetWithProtocolIdAsync(ProtocolId);

                if (result != null)
                {
                    anamnesisDto = result;
                    isExistingAnamnesis = true;

                }
                else
                {
                    isExistingAnamnesis = false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Hata oluþtu: {ex.Message}");

            }
        }

        private void UpdateUnit(string unit)
        {
            selectedUnit = unit;
            UpdateDate();
        }

        private void UpdateDate()
        {
            var now = DateTime.Now;
            anamnesisDto.StartDate = selectedUnit switch
            {
                "Saat" => now.AddHours(-durationValue),
                "Gün" => now.AddDays(-durationValue),
                "Hafta" => now.AddDays(-durationValue * 7),
                "Ay" => now.AddMonths(-durationValue),
                "Yýl" => now.AddYears(-durationValue),
                _ => now
            };
        }
        private string GetButtonClass(string unit)
        {
            return unit == selectedUnit ? "e-primary" : "e-outline";
        }

        #endregion

        #region PsychologicalState
        private MentalState selectedMentalState;
        //private string description = string.Empty;

        private PshychologicalStateDto pshychologicalStateDto = new();
        private bool isExistingPsychologicalState;

        private async Task SavePsychologicalStateAsync()
        {
            try
            {
                if (isExistingPsychologicalState)
                {
                    await UpdatePsychologicalStateAsync();

                }
                else
                {
                    await CreatePsychologicalStateAsync();
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Kaydetme iþlemi sýrasýnda hata oluþtu: {ex.Message}");
            }
        }

        #region Create
        private async Task CreatePsychologicalStateAsync()
        {
            await HandleError(async () =>
            {
                var createDto = new PshychologicalStateCreateDto
                {
                    Description = pshychologicalStateDto.Description,
                    MentalState = pshychologicalStateDto.MentalState,
                    ProtocolId = ProtocolId
                };

                await PsychologicalStateAppService.CreateAsync(createDto);
                await ShowToast("Ýþlem Baþarýlý", true);
            });
        }
        #endregion

        #region UPDATE
        private async Task UpdatePsychologicalStateAsync()
        {
            await HandleError(async () =>
            {
                var updateDto = new PshychologicalStateUpdateDto
                {
                    Id = pshychologicalStateDto.Id,
                    Description = pshychologicalStateDto.Description,
                    MentalState = pshychologicalStateDto.MentalState,
                    ProtocolId = ProtocolId
                };

                await PsychologicalStateAppService.UpdateAsync(updateDto);
                await ShowToast("Ýþlem Baþarýlý", true);
            });
        }
        #endregion

        private async Task LoadPsychologicalStateAsync()
        {
            try
            {
                var result = await PsychologicalStateAppService.GetWithProtocolIdAsync(ProtocolId);

                if (result != null)
                {
                    pshychologicalStateDto = result;
                    isExistingPsychologicalState = true;

                }
                else
                {
                    isExistingPsychologicalState = false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Hata oluþtu: {ex.Message}");

            }
        }
        private void SetMentalState(MentalState state)
        {
            pshychologicalStateDto.MentalState = state;
        }

        private string GetButtonClass(MentalState state)
        {
            return state == pshychologicalStateDto.MentalState ? "e-primary" : "e-outline";
        }
        #endregion

        #region FALLRISK
       
        private FallRiskCreateDto fallRiskCreateDto = new FallRiskCreateDto();
        private FallRiskDto fallRiskDto = new FallRiskDto();
        private bool isFallRisk;
        private bool isFallRiskModalOpen = false;
        private string fallRiskDescription = string.Empty;
        private int score = 0;
        private bool isExistingRecord = false;

        private async Task UpdateDescriptionAndScoreAsync()
        {
            try
            {
                if (isExistingRecord)
                {
                    
                    var updateDto = new FallRiskUpdateDto
                    {
                        Id = fallRiskDto.Id,
                        ProtocolId = fallRiskDto.ProtocolId, 
                        Description = fallRiskDto.Description,
                        Score = fallRiskDto.Score,
                        HasFallHistory = fallRiskDto.HasFallHistory,
                        UsesMedications = fallRiskDto.UsesMedications,
                        HasAddiction = fallRiskDto.HasAddiction,
                        HasBalanceDisorder = fallRiskDto.HasBalanceDisorder,
                        HasVisionImpairment = fallRiskDto.HasVisionImpairment,
                        MentalState = fallRiskDto.MentalState,
                        GeneralHealthState = fallRiskDto.GeneralHealthState
                    };

                   
                    await FallRiskAppService.UpdateAsync(updateDto);
                }
                else
                {
                   
                    Console.WriteLine("Kayýt bulunamadý, önce bir kayýt oluþturun.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluþtu: {ex.Message}");
            }
        }
        private async Task LoadFallRiskAsync()
        {
            try
            {
               
                var protocolId = ProtocolId; 
                var result = await FallRiskAppService.GetWithProtocolIdAsync(protocolId);

                if (result != null)
                {
                    
                    fallRiskDto = new FallRiskDto
                    {
                        Id = result.Id,
                        ProtocolId = result.ProtocolId,
                        Score = result.Score,
                        Description = result.Description,
                        HasFallHistory = result.HasFallHistory,
                        UsesMedications = result.UsesMedications,
                        HasAddiction = result.HasAddiction,
                        HasBalanceDisorder = result.HasBalanceDisorder,
                        HasVisionImpairment = result.HasVisionImpairment,
                        MentalState = result.MentalState,
                        GeneralHealthState = result.GeneralHealthState
                    };
                    isFallRisk = true;
                    score = fallRiskDto.Score;
                    fallRiskDescription = fallRiskDto.Description;

                    isExistingRecord = true; 
                }
                else
                {
                  
                    fallRiskDto = new FallRiskDto();
                    isFallRisk = false; 
                    score = 0;
                    fallRiskDescription = string.Empty;
                    isExistingRecord = false;
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Hata oluþtu: {ex.Message}");
            }
        }

        private async Task SaveFallRiskAsync()
        {
            try
            {
                if (isExistingRecord)
                {
                    isFallRisk = true;
                    // Güncelleme iþlemi
                    var updateDto = new FallRiskUpdateDto
                    {
                        Id = fallRiskDto.Id,
                        ProtocolId = ProtocolId,
                        Score = fallRiskDto.Score,
                        Description = fallRiskDto.Description,
                        HasFallHistory = fallRiskDto.HasFallHistory,
                        UsesMedications = fallRiskDto.UsesMedications,
                        HasAddiction = fallRiskDto.HasAddiction,
                        HasBalanceDisorder = fallRiskDto.HasBalanceDisorder,
                        HasVisionImpairment = fallRiskDto.HasVisionImpairment,
                        MentalState = fallRiskDto.MentalState,
                        GeneralHealthState = fallRiskDto.GeneralHealthState
                    };

                   var updatedFallRisk = await FallRiskAppService.UpdateAsync(updateDto);


                    fallRiskDto = new FallRiskDto
                    {
                        Id = updatedFallRisk.Id,
                        ProtocolId = updatedFallRisk.ProtocolId,
                        Score = updatedFallRisk.Score,
                        Description = updatedFallRisk.Description,
                        HasFallHistory = updatedFallRisk.HasFallHistory,
                        UsesMedications = updatedFallRisk.UsesMedications,
                        HasAddiction = updatedFallRisk.HasAddiction,
                        HasBalanceDisorder = updatedFallRisk.HasBalanceDisorder,
                        HasVisionImpairment = updatedFallRisk.HasVisionImpairment,
                        MentalState = updatedFallRisk.MentalState,
                        GeneralHealthState = updatedFallRisk.GeneralHealthState
                    };

                    isExistingRecord = true; // Artýk kayýt mevcut
                    isFallRiskModalOpen = false;
                    StateHasChanged();
                }
                else
                {
                    // Yeni kayýt oluþturma
                    var createDto = new FallRiskCreateDto
                    {
                        ProtocolId = fallRiskDto.ProtocolId,
                        Score = fallRiskDto.Score,
                        Description = fallRiskDto.Description,
                        HasFallHistory = fallRiskDto.HasFallHistory,
                        UsesMedications = fallRiskDto.UsesMedications,
                        HasAddiction = fallRiskDto.HasAddiction,
                        HasBalanceDisorder = fallRiskDto.HasBalanceDisorder,
                        HasVisionImpairment = fallRiskDto.HasVisionImpairment,
                        MentalState = fallRiskDto.MentalState,
                        GeneralHealthState = fallRiskDto.GeneralHealthState
                    };

                    var createdFallRisk = await FallRiskAppService.CreateAsync(createDto);
                   
                    // DTO'yu güncelle ve mevcut kayýt olarak iþaretle
                    fallRiskDto = new FallRiskDto
                    {
                        Id = createdFallRisk.Id,
                        ProtocolId = createdFallRisk.ProtocolId,
                        Score = createdFallRisk.Score,
                        Description = createdFallRisk.Description,
                        HasFallHistory = createdFallRisk.HasFallHistory,
                        UsesMedications = createdFallRisk.UsesMedications,
                        HasAddiction = createdFallRisk.HasAddiction,
                        HasBalanceDisorder = createdFallRisk.HasBalanceDisorder,
                        HasVisionImpairment = createdFallRisk.HasVisionImpairment,
                        MentalState = createdFallRisk.MentalState,
                        GeneralHealthState = createdFallRisk.GeneralHealthState
                    };

                    isExistingRecord = true; // Artýk kayýt mevcut
                    isFallRiskModalOpen = false;
                    StateHasChanged();

                }

                // Ýþlem tamamlandýðýnda modalý kapat
                isFallRiskModalOpen = false;
                StateHasChanged();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluþtu: {ex.Message}");
            }
        }
     
        private void SetHasFallHistory(bool value)
        {
            fallRiskDto.HasFallHistory = value;
            StateHasChanged();
        }
        private void SetUsesMedications(bool value)
        {
            fallRiskDto.UsesMedications = value;
            StateHasChanged();
        }

        private void SetHasAddiction(bool value)
        {
            fallRiskDto.HasAddiction = value;
            StateHasChanged();
        }

        private void SetHasBalanceDisorder(bool value)
        {
            fallRiskDto.HasBalanceDisorder = value;
            StateHasChanged();
        }

        private void SetHasVisionImpairment(bool value)
        {
            fallRiskDto.HasVisionImpairment = value;
            StateHasChanged();
        }

        private void SetMentalState(bool value)
        {
            fallRiskDto.MentalState = value;
            StateHasChanged();
        }

        private void SetGeneralHealthState(bool value)
        {
            fallRiskDto.GeneralHealthState = value;
            StateHasChanged();
        }

        private void OpenFallRiskModal()
        {
            isFallRiskModalOpen = true;
        }
        private void CloseFallRiskModal()
        {
            isFallRiskModalOpen = false;
        }
        private void SetFallRisk(bool fallRisk)
        {
            isFallRisk = fallRisk;

            if (fallRisk)
            {
                
                isFallRiskModalOpen = true;
            }
        }

        private string GetFallRiskButtonClass(bool fallRisk)//bool? state, bool value
        {
            return fallRisk == isFallRisk ? "e-primary" : "e-outline";
            
        }
        #endregion

        #region FIZIKSEL MUAYENE
        private PhysicalExaminationDto physicalExaminationDto= new PhysicalExaminationDto();
        private PhysicalExaminationCreateDto physicalExaminationCreateDto = new PhysicalExaminationCreateDto();
        private bool isExistingPhysicalExamination;
        private async Task LoadPhysicalExaminationsAsync()
        {
            try
            {
                // API isteði (ProtocolId ile)
                var protocolId = ProtocolId; // ProtocolId burada ilgili kaydýn ID'si olacak
                var result = await PhysicalExaminationAppService.GetWithProtocolIdAsync(protocolId);

                if (result != null && result.Id != Guid.Empty)
                {
                    // Gelen DTO'yu kullan
                    physicalExaminationDto = result;

                    // Gerekli alanlarý doldur
                    physicalExaminationCreateDto = new PhysicalExaminationCreateDto
                    {
                        ProtocolId = protocolId,
                        Height = result.Height,
                        Weight = result.Weight,
                        BMI = result.BMI,
                        VYA = result.VYA,
                        Temperature = result.Temperature,
                        Pulse = result.Pulse,
                        SystolicBP = result.SystolicBP,
                        DiastolicBP = result.DiastolicBP,
                        SPO2 = result.SPO2,
                        Note = result.Note
                    };
                }
                else
                {
                    // Yeni kayýt oluþturma durumu
                    physicalExaminationDto = new PhysicalExaminationDto();
                    physicalExaminationCreateDto = new PhysicalExaminationCreateDto { ProtocolId = protocolId };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluþtu: {ex.Message}");
            }
        }

        private async Task LoadPhysicalExaminationAsync()
        {
            try
            {
                var protocolId = ProtocolId; 
                var result = await PhysicalExaminationAppService.GetWithProtocolIdAsync(protocolId);

                if (result != null && result.Id != Guid.Empty)
                {
                    // Kayýt bulundu, DTO'yu güncelle
                    physicalExaminationDto = result;
                    isExistingPhysicalExamination = true; // Kayýt bulundu
                }
                else
                {
                    // Kayýt bulunamadý, yeni DTO oluþtur
                    physicalExaminationDto = new PhysicalExaminationDto();
                    isExistingPhysicalExamination = false; // Yeni kayýt oluþturulacak
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluþtu: {ex.Message}");
            }
        }

        private void CalculateBMIandVYA()
        {
            if (physicalExaminationDto.Weight.HasValue && physicalExaminationDto.Height.HasValue)
            {
                // BMI Hesaplama (Boy cm'den metreye çevriliyor)
                decimal heightInMeters = physicalExaminationDto.Height.Value / 100;
                physicalExaminationDto.BMI = physicalExaminationDto.Weight.Value / (heightInMeters * heightInMeters);

                // VYA Hesaplama (Örnek formül, ihtiyaca göre deðiþtirilir)
                physicalExaminationDto.VYA = (physicalExaminationDto.BMI > 25) ? (physicalExaminationDto.BMI - 25) * 1.5m : 0;
            }
            else
            {
                physicalExaminationDto.BMI = null;
                physicalExaminationDto.VYA = null;
            }

            StateHasChanged();
        }

        private async Task SavePhysicalExaminationAsync()
        {
            try
            {
                // BMI ve VYA hesapla
                CalculateBMIandVYA();

                // Kayýt oluþturma iþlemi
                if (!isExistingPhysicalExamination)
                {
                    // Yeni kayýt oluþturulacak
                    var createDto = new PhysicalExaminationCreateDto
                    {
                        ProtocolId = ProtocolId,
                        Weight = physicalExaminationDto.Weight,
                        Height = physicalExaminationDto.Height,
                        BMI = physicalExaminationDto.BMI,
                        VYA = physicalExaminationDto.VYA,
                        Temperature = physicalExaminationDto.Temperature,
                        Pulse = physicalExaminationDto.Pulse,
                        SystolicBP = physicalExaminationDto.SystolicBP,
                        DiastolicBP = physicalExaminationDto.DiastolicBP,
                        SPO2 = physicalExaminationDto.SPO2,
                        Note = physicalExaminationDto.Note
                    };

                    // API çaðrýsý ile yeni kayýt oluþtur
                    var createdExamination = await PhysicalExaminationAppService.CreateAsync(createDto);

                    // Yeni kaydý DTO'ya aktar
                    physicalExaminationDto = new PhysicalExaminationDto
                    {
                        Id = createdExamination.Id,
                        ProtocolId = createdExamination.ProtocolId,
                        Height = createdExamination.Height,
                        Weight = createdExamination.Weight,
                        BMI = createdExamination.BMI,
                        VYA = createdExamination.VYA,
                        Temperature = createdExamination.Temperature,
                        Pulse = createdExamination.Pulse,
                        SystolicBP = createdExamination.SystolicBP,
                        DiastolicBP = createdExamination.DiastolicBP,
                        SPO2 = createdExamination.SPO2,
                        Note = createdExamination.Note
                    };


                    isExistingPhysicalExamination = true; // Artýk kayýt mevcut
                }
                else
                {
                    // Mevcut kaydý güncelle
                    var updateDto = new PhysicalExaminationUpdateDto
                    {
                        Id = physicalExaminationDto.Id,
                        ProtocolId = physicalExaminationDto.ProtocolId,
                        Weight = physicalExaminationDto.Weight,
                        Height = physicalExaminationDto.Height,
                        BMI = physicalExaminationDto.BMI,
                        VYA = physicalExaminationDto.VYA,
                        Temperature = physicalExaminationDto.Temperature,
                        Pulse = physicalExaminationDto.Pulse,
                        SystolicBP = physicalExaminationDto.SystolicBP,
                        DiastolicBP = physicalExaminationDto.DiastolicBP,
                        SPO2 = physicalExaminationDto.SPO2,
                        Note = physicalExaminationDto.Note
                    };

                    // API çaðrýsý ile mevcut kaydý güncelle
                    var updatedExamination = await PhysicalExaminationAppService.UpdateAsync(updateDto);

                    // Güncellenen kaydý DTO'ya aktar
                    physicalExaminationDto = new PhysicalExaminationDto
                    {
                        Id = updatedExamination.Id,
                        ProtocolId = updatedExamination.ProtocolId,
                        Height = updatedExamination.Height,
                        Weight = updatedExamination.Weight,
                        BMI = updatedExamination.BMI,
                        VYA = updatedExamination.VYA,
                        Temperature = updatedExamination.Temperature,
                        Pulse = updatedExamination.Pulse,
                        SystolicBP = updatedExamination.SystolicBP,
                        DiastolicBP = updatedExamination.DiastolicBP,
                        SPO2 = updatedExamination.SPO2,
                        Note = updatedExamination.Note
                    };
                }

                StateHasChanged(); // UI'yi güncelle
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluþtu: {ex.Message}");
            }
        }


        #endregion

        #region PAIN DETAILS
        private bool isPain = false;  // Baþlangýçta aðrý yok
        private bool isExistingPain; 

        private List<LookupDto<Guid>> painTypes = new List<LookupDto<Guid>>();
        private int painDurationValue = 0;                   
        private List<string> timeUnits = new List<string> { "Saat", "Gün", "Hafta", "Ay", "Yýl" };
        private string painSelectedUnit = "Gün";  
        private DateTime painStartDate = DateTime.Now;
        private PainRhythm selectedPainRhythm;
        private PainDetailDto painDetailDto = new PainDetailDto();
        private PainDetailCreateDto painDetailCreateDto = new PainDetailCreateDto();
        private PainDetailUpdateDto painDetailUpdateDto = new PainDetailUpdateDto();
        private Guid selectedPainTypeId;


        private async Task SavePainDetailAsync()
        {
            try
            {
                // Kayýt oluþturma iþlemi
                if (!isExistingPain)
                {
                    // Yeni kayýt oluþturulacak
                    var createDto = new PainDetailCreateDto
                    {
                        Area = painDetailDto.Area,
                        ProtocolId = ProtocolId,
                        Score = painDetailDto.Score,
                        PainTypeId = selectedPainTypeId,
                        Description = painDetailDto.Description,
                        PainRhythm = painDetailDto.PainRhythm,
                        OtherPain = painDetailDto.OtherPain,
                        StartDate = painDetailDto.StartDate
                      
                    };

                    // API çaðrýsý ile yeni kayýt oluþtur
                    var createdPainDetail = await PainDetailAppService.CreateAsync(createDto);

                    // Yeni kaydý DTO'ya aktar
                    painDetailDto = new PainDetailDto
                    {
                        Id = createdPainDetail.Id,
                        Area = createdPainDetail.Area,
                        ProtocolId = ProtocolId,
                        Score = createdPainDetail.Score,
                        PainTypeId = createdPainDetail.PainTypeId,
                        Description = createdPainDetail.Description,
                        PainRhythm = createdPainDetail.PainRhythm,
                        OtherPain = createdPainDetail.OtherPain,
                        StartDate = createdPainDetail.StartDate
                    };


                    isExistingPain = true; // Artýk kayýt mevcut
                }
                else
                {
                    // Mevcut kaydý güncelle
                    var updateDto = new PainDetailUpdateDto
                    {
                        Id = painDetailDto.Id,
                        Area = painDetailDto.Area,
                        ProtocolId = ProtocolId,
                        Score = painDetailDto.Score,
                        PainTypeId = painDetailDto.PainTypeId,
                        Description = painDetailDto.Description,
                        PainRhythm = painDetailDto.PainRhythm,
                        OtherPain = painDetailDto.OtherPain,
                        StartDate = painDetailDto.StartDate
                    };

                    // API çaðrýsý ile mevcut kaydý güncelle
                    var updatedPainDetail = await PainDetailAppService.UpdateAsync(updateDto);

                    // Güncellenen kaydý DTO'ya aktar
                    painDetailDto = new PainDetailDto
                    {
                        Id = updatedPainDetail.Id,
                        Area = updatedPainDetail.Area,
                        ProtocolId = ProtocolId,
                        Score = updatedPainDetail.Score,
                        PainTypeId = updatedPainDetail.PainTypeId,
                        Description = updatedPainDetail.Description,
                        PainRhythm = updatedPainDetail.PainRhythm,
                        OtherPain = updatedPainDetail.OtherPain,
                        StartDate = updatedPainDetail.StartDate
                    };
                }

                StateHasChanged(); // UI'yi güncelle
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluþtu: {ex.Message}");
            }
        }
        private async Task LoadPainDetailAsync()
        {
            try
            {
                var protocolId = ProtocolId;
                var result = await PainDetailAppService.GetWithNavigationPropertiesByProtocolIdAsync(protocolId);

                if (result != null && result.Id != Guid.Empty)
                {
                    // Kayýt bulundu, DTO'yu güncelle
                    painDetailDto = result;
                    isExistingPain = true; // Kayýt bulundu
                    isPain = true;
                }
                else
                {
                    // Kayýt bulunamadý, yeni DTO oluþtur
                    painDetailDto = new PainDetailDto();
                    isExistingPain = false; // Yeni kayýt oluþturulacak
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluþtu: {ex.Message}");
            }
        }

        private void UpdatePainStartDate()
        {
            var now = DateTime.Now;
            painStartDate = painSelectedUnit switch
            {
                "Saat" => now.AddHours(-painDurationValue),
                "Gün" => now.AddDays(-painDurationValue),
                "Hafta" => now.AddDays(-painDurationValue * 7),
                "Ay" => now.AddMonths(-painDurationValue),
                "Yýl" => now.AddYears(-painDurationValue),
                _ => now
            };
            StateHasChanged(); // UI'yi güncelle
        }

        private void SetPain(bool value)
        {
            isPain = value;
            StateHasChanged();
        }

        private async Task LoadPainTypesAsync()
        {
            try
            {
                var input = new LookupRequestDto { MaxResultCount = 10 }; // Gerekirse Filter eklenebilir
                var result = await PainDetailAppService.GetPainTypeLookupAsync(input);
                painTypes = result.Items.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluþtu: {ex.Message}");
            }
        }
        private void SetPainType(Guid painTypeId)
        {
            selectedPainTypeId = painTypeId;
          
        }
        private void SetPainRhythm(PainRhythm painRhythm)
        {
            selectedPainRhythm = painRhythm;
        }
        private string GetPainTypeButtonClass(Guid painTypeId)
        {
            return painTypeId == selectedPainTypeId ? "e-primary" : "e-outline"; // Seçili olan için farklý bir sýnýf
        }
        private string GetPainRhythmButtonClass(PainRhythm painRhythm)
        {
            return painRhythm == selectedPainRhythm ? "e-primary" : "e-outline";
        }
        #endregion


        #region TANIKART
        //private List<LookupDto<Guid>> IcdList { get; set; } = new();
        private List<Pusula.Training.HealthCare.Shared.LookupDto<Guid>> IcdList { get; set; } = new();
        private Guid SelectedDiagnosisId { get; set; }
        private DateTime DiagnosisDate { get; set; } 
        private string Description { get; set; }
        protected int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        protected int CurrentPage { get; set; } = 1;
        protected long TotalCount { get; set; }

        private ExaminationDiagnosisCreateDto examinationDiagnosisCreateDto { get; set; }

        private List<ExaminationDiagnosisWithNavigationPropertiesDto> ExaminationDiagnoses { get; set; } = new();
        private DiagnosisType SelectedDiagnosisType { get; set; }

        //protected async Task LoadIcdListAsync()
        //{
        //    var input = new GetDiagnosisInput
        //    {
        //        FilterText = null,
        //        Name = null,
        //        Code = null,
        //        GroupId = null,
        //        MaxResultCount = PageSize,
        //        SkipCount = (CurrentPage - 1) * PageSize
        //    };

        //    var result = await DiagnosisAppService.GetListAsync(input);
        //    IcdList = result.Items.ToList();
        //    TotalCount = result.TotalCount;
        //}
        private async Task DeleteExaminationDiagnosis(Guid id)
        {
            try
            {
                // DeleteAsync metodu ile silme iþlemi
                await ExaminationDiagnosisAppService.DeleteAsync(id);

                // Silme iþleminden sonra listeyi yenile
                await LoadExaminationDiagnosesAsync();
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Silme sýrasýnda bir hata oluþtu: {ex.Message}");
            }
        }
        private async Task LoadExaminationDiagnosesAsync()
        {
           
            var input = new GetExaminationDiagnosisInput
            {
                ProtocolId = ProtocolId, // URL'den aldýðýnýz Protokol ID
                FilterText = null,       // Filtre yok
                DiagnosisType = null,    // Tüm taný tipleri
                InitialDate = null,      // Tarih filtresi yok
                Note = null,             // Not filtresi yok
                DiagnosisId = null,      // Taný ID filtresi yok
                //MaxResultCount = 100,    // Maksimum sayfa boyutu
                //SkipCount = 0,           // Ýlk sayfa
                //Sorting = "InitialDate DESC" // Tarihe göre azalan sýralama
            };

            // API çaðrýsý
            var result = await ExaminationDiagnosisAppService.GetListAsync(input);

            // Gelen veriyi listeye at
            ExaminationDiagnoses = result.Items.ToList();

            StateHasChanged(); // Sayfayý güncelle
        }
        private async Task CreateExaminationDiagnosis()
        {
            var createDto = new ExaminationDiagnosisCreateDto
            {
                DiagnosisType = SelectedDiagnosisType,
                InitialDate = DiagnosisDate ,
                Note = Description,
                ProtocolId = ProtocolId,
                DiagnosisId = SelectedDiagnosisId,
             
            };

            // API çaðrýsý ile yeni kayýt oluþtur
            var createdExaminationDiagnosis = await ExaminationDiagnosisAppService.CreateAsync(createDto);
            await LoadExaminationDiagnosesAsync();
           
            DiagnosisDate = DateTime.Now;
            Description = string.Empty;
            SelectedDiagnosisId = Guid.Empty;
            //StateHasChanged();
        }
        private void SetDiagnosisType(DiagnosisType type)
        {
            SelectedDiagnosisType = type;
            StateHasChanged();
        }

        private string GetDiagnosisTypeButtonClass(DiagnosisType type)
        {
            return type == SelectedDiagnosisType ? "e-primary" : "e-outline";
        }
        protected async Task LoadIcdListAsync()
        {
            var input = new LookupRequestDto
            {
                Filter = null, // Eðer bir filtreleme gerekiyorsa buraya eklenebilir
                MaxResultCount = 250,
                SkipCount = 0
            };

            var result = await ExaminationDiagnosisAppService.GetDiagnosisLookupAsync(input);
            IcdList = result.Items.ToList(); // Servisten gelen verileri IcdList'e atýyoruz
        }

        #endregion

        #region SISTEM SORGUSU

        private SystemCheckDto systemCheckDto = new SystemCheckDto();
        private SystemCheckCreateDto systemCheckCreateDto = new SystemCheckCreateDto();
        private SystemCheckUpdateDto systemCheckUpdateDto = new SystemCheckUpdateDto();


        private bool isSystemModalOpen = false;
        private bool isExistingSystemCheck = false;
       

      
        private void ToggleState(Func<bool?> getState, Action<bool?> setState, bool buttonState)
        {
            // Mevcut durumu kontrol et ve güncelle
            var currentState = getState();

            // Eðer mevcut durum týklanan butonun durumuyla aynýysa null yap yani seçimi kaldýr
            setState(currentState == buttonState ? null : buttonState);
        }
        private string GetSystemCheckButtonClass(bool? currentState, bool buttonState)
        {
            // Eðer buton seçiliyse primary, deðilse outline
           
            if (currentState == null)
            {
                // Null durumunda her iki buton da outline olur
                return "e-outline";
            }

            // Seçili durumu kontrol et
            return currentState == buttonState
                ? "e-primary"
                : "e-outline";
        }
        private async Task LoadSystemCheckAsync()
        {
            try
            {
               
                var result = await SystemCheckAppService.GetByProtocolIdAsync(ProtocolId);

                if (result != null)
                {
                    systemCheckDto = result;
                    isExistingSystemCheck = true;
                }
                else
                {
                    
                    systemCheckDto = new SystemCheckDto();
                    isExistingSystemCheck = false;
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Hata oluþtu: {ex.Message}");
                
            }
        }

        private async Task OnSaveSystemCheck()
        {
            try
            {
                if (isExistingSystemCheck)
                {
                    await UpdateSystemCheckAsync();
                }
                else
                {
                    await CreateSystemCheckAsync();
                }

              
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Kaydetme iþlemi sýrasýnda hata oluþtu: {ex.Message}");
            }
        }

        #region Create

        private async Task CreateSystemCheckAsync()
        {
            await HandleError(async () =>
            {
                var createDto = new SystemCheckCreateDto
                {
                    ProtocolId = ProtocolId,
                    GeneralSystemCheck = systemCheckDto.GeneralSystemCheck,
                    GenitoUrinary = systemCheckDto.GenitoUrinary,
                    Skin = systemCheckDto.Skin,
                    Respiratory = systemCheckDto.Respiratory,
                    Nervous = systemCheckDto.Nervous,
                    MusculoSkeletal = systemCheckDto.MusculoSkeletal,
                    Circulatory = systemCheckDto.Circulatory,
                    GastroIntestinal = systemCheckDto.GastroIntestinal,
                    Description = systemCheckDto.Description
                };


                await SystemCheckAppService.CreateAsync(createDto);
                isSystemModalOpen = false;
                await ShowToast("Ýþlem Baþarýlý", true);
            });

        }


        #endregion

        #region Update
        private async Task UpdateSystemCheckAsync()
        {
            await HandleError(async () =>
            {
                var updateDto = new SystemCheckUpdateDto
                {
                    Id = systemCheckDto.Id,
                    ProtocolId = ProtocolId,
                    GeneralSystemCheck = systemCheckDto.GeneralSystemCheck,
                    GenitoUrinary = systemCheckDto.GenitoUrinary,
                    Skin = systemCheckDto.Skin,
                    Respiratory = systemCheckDto.Respiratory,
                    Nervous = systemCheckDto.Nervous,
                    MusculoSkeletal = systemCheckDto.MusculoSkeletal,
                    Circulatory = systemCheckDto.Circulatory,
                    GastroIntestinal = systemCheckDto.GastroIntestinal,
                    Description = systemCheckDto.Description
                };


                await SystemCheckAppService.UpdateAsync(updateDto);
                isSystemModalOpen = false;
                await ShowToast("Ýþlem Baþarýlý", true);
            });
        }

        #endregion
        private void OpenSystemCheckModal()
        {
            isSystemModalOpen = true;
        }
        #endregion

        #region IZLEM VE PLAN
        private FollowUpType selectedFollowUpType;
        private bool isSurgeryPlanned;
        private FollowUpPlanDto followUpPlanDto = new FollowUpPlanDto();
        private bool isExistingFollowUpPlan;

        private async Task LoadFollowUpPlanAsync()
        {
            try
            {
                var result = await FollowUpPlanAppService.GetByProtocolIdAsync(ProtocolId);

                if (result != null)
                {
                    followUpPlanDto = result;
                    isExistingFollowUpPlan = true;
                    selectedFollowUpType = followUpPlanDto.FollowUpType;
                }
                else
                {

                    followUpPlanDto = new FollowUpPlanDto();
                    isExistingFollowUpPlan = false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Hata oluþtu: {ex.Message}");

            }
        }
        private async Task SaveFollowUpPlan()
        {
            try
            {
                if (isExistingFollowUpPlan)
                {
                    await UpdateFollowUpPlanAsync();
                   
                }
                else
                {
                    await CreateFollowUpPlanAsync();
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Kaydetme iþlemi sýrasýnda hata oluþtu: {ex.Message}");
            }
        }

        #region Create
        private async Task CreateFollowUpPlanAsync()
        {
            await HandleError(async () =>
            {
                var createDto = new FollowUpPlanCreateDto
                {
                    ProtocolId = ProtocolId,
                    Note = followUpPlanDto.Note,
                    FollowUpType = selectedFollowUpType,
                    IsSurgeryScheduled = isSurgeryPlanned,
                };

                await FollowUpPlanAppService.CreateAsync(createDto);
                await ShowToast("Ýþlem Baþarýlý", true);
            });
        }
        #endregion

        #region UPDATE
        private async Task UpdateFollowUpPlanAsync()
        {
            await HandleError(async () =>
            {
                var updateDto = new FollowUpPlanUpdateDto
                {
                    Id = followUpPlanDto.Id,
                    ProtocolId = ProtocolId,
                    Note = followUpPlanDto.Note,
                    FollowUpType = selectedFollowUpType,
                    IsSurgeryScheduled = isSurgeryPlanned,
                };

                await FollowUpPlanAppService.UpdateAsync(updateDto);
                await ShowToast("Ýþlem Baþarýlý", true);
            });
        }
        #endregion
      
        private string GetSurgeryButtonClass(bool value)
        {
            return isSurgeryPlanned == value ? "e-primary" : "e-outline";
        }

        // Ameliyat planýný ayarlayan metot
        private void SetSurgeryPlan(bool value)
        {
            isSurgeryPlanned = value;
        }
        private void SetFollowUpType(FollowUpType type)
        {
            selectedFollowUpType = type;
        }

        private string GetButtonClass(FollowUpType followUpType)
        {
            return followUpType == selectedFollowUpType ? "e-primary" : "e-outline";
        }
        #endregion

        #region SOYGECMIS
        public FamilyHistoryDto familyHistoryDto = new FamilyHistoryDto();
        private bool isExistingFamilyHistory;

        #region UPDATE
        private async Task UpdateFamilyHistoryAsync()
        {
            await HandleError(async () =>
            {
                var updateDto = new FamilyHistoryUpdateDto
                {
                    Id = familyHistoryDto.Id,
                    PatientId = PatientId,
                    Mother = familyHistoryDto.Mother,
                    Father = familyHistoryDto.Father,
                    Sister = familyHistoryDto.Sister,
                    Brother = familyHistoryDto.Brother,
                    Other = familyHistoryDto.Other,
                    IsParentsRelative = familyHistoryDto.IsParentsRelative,
                };

                await FamilyHistoriesAppService.UpdateAsync(updateDto);
                await ShowToast("Ýþlem Baþarýlý", true);
            });
        }
        #endregion

        #region Create
        private async Task CreateFamilyHistoryAsync()
        {
            await HandleError(async () =>
            {
                var createDto = new FamilyHistoryCreateDto
                {
                    PatientId = PatientId,
                    Mother = familyHistoryDto.Mother,
                    Father = familyHistoryDto.Father,
                    Sister = familyHistoryDto.Sister,
                    Brother = familyHistoryDto.Brother,
                    Other = familyHistoryDto.Other,
                    IsParentsRelative = familyHistoryDto.IsParentsRelative,
                };

                await FamilyHistoriesAppService.CreateAsync(createDto);
                await ShowToast("Ýþlem Baþarýlý", true);
            });
        }
        #endregion
        private async Task SaveFamilyHistory()
        {
            try
            {
                if (isExistingFamilyHistory)
                {
                    await UpdateFamilyHistoryAsync();

                }
                else
                {
                    await CreateFamilyHistoryAsync();
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Kaydetme iþlemi sýrasýnda hata oluþtu: {ex.Message}");
            }
        }
        private async Task LoadFamilyHistoryAsync()
        {
            try
            {
                var result = await FamilyHistoriesAppService.GetByPatientIdAsync(PatientId);

                if (result != null)
                {
                    familyHistoryDto = result;
                    isExistingFamilyHistory = true;
                   
                }
                else
                {

                    familyHistoryDto = new FamilyHistoryDto();
                    isExistingFamilyHistory = false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Hata oluþtu: {ex.Message}");

            }
        }


        #endregion

        #region Kontrol Notlari
        public ControlNoteDto controlNoteDto = new ControlNoteDto();
        public ControlNoteCreateDto controlNoteCreateDto = new ControlNoteCreateDto();
        private List<ControlNoteDto> controlNotes { get; set; } = new();

        private SfDialog? NoteDialog; // Modal referansý
        private string SelectedNote = string.Empty; // Seçilen notun içeriði
        private bool isGridVisible = true; // Grid'in görünürlük durumu
        private SfDialog? DeleteControlNoteDialog; // Silme modali referansý
        private Guid SelectedControlNoteId; // Silinecek notun ID'si

        // Modal açýlýrken çalýþacak
        private async Task OpenControlNoteDeleteModal(Guid id)
        {
            SelectedControlNoteId = id;
            await DeleteControlNoteDialog!.ShowAsync(); // Modalý göster
        }

        // Silme iþlemini onayla
        private async Task ConfirmControlNoteDelete()
        {
            await HandleError(async () =>
            {
                // Servis üzerinden silme iþlemi
                await ControlNotesAppService.DeleteAsync(SelectedControlNoteId);
                await ShowToast("Ýþlem Baþarýlý", true);
                await LoadControlNotesAsync();
            });
          
            await DeleteControlNoteDialog!.HideAsync();
            
        }

        // Modalý kapat (Silme iþlemi iptal)
        private async Task CloseControlNoteDeleteModal()
        {
            SelectedControlNoteId = Guid.Empty; // ID'yi sýfýrla
            await DeleteControlNoteDialog!.HideAsync();
        }
        private async Task UpdateControlNote()
        {
            // Düzenleme iþlemi burada yapýlacak
            Console.WriteLine($"Edit Note ID: ");
        }

        private async Task DeleteControlNote(Guid? id)
        {
            await HandleError(async () =>
            {
                var createDto = new ControlNoteCreateDto
                {
                    ProtocolId = ProtocolId,
                    NoteDate = controlNoteCreateDto.NoteDate,
                    Note = controlNoteCreateDto.Note
                };

                await ControlNotesAppService.CreateAsync(createDto);
                await ShowToast("Ýþlem Baþarýlý", true);
                await LoadControlNotesAsync();
            });
        }
        private async Task OpenNoteModal(string? note)
        {
            SelectedNote = note ?? "Açýklama mevcut deðil.";
            await NoteDialog!.ShowAsync(); // Modalý göster
        }

        private async Task CloseNoteModal()
        {
            SelectedNote = string.Empty; // Not içeriðini temizle
            await NoteDialog!.HideAsync(); // Modalý gizle
        }
        private string GetToggleIcon()
        {
            return isGridVisible ? "e-icons e-arrow-up" : "e-icons e-arrow-down";
        }
        private void ToggleGrid()
        {
            isGridVisible = !isGridVisible; // Grid görünürlüðünü deðiþtir
        }
        private async Task LoadControlNotesAsync()
        {
            var input = new GetControlsInput
            {
                ProtocolId = ProtocolId, // URL'den gelen veya baþka bir yerden alýnan Protokol ID
                FilterText = null,       // Filtre yok
                NoteDate = null,         // Tarih filtresi yok
                Note = null,             // Not filtresi yok
                Sorting = "NoteDate DESC", // Tarihe göre azalan sýralama
                MaxResultCount = 10,    // Maksimum sonuç boyutu
                SkipCount = 0          // Ýlk sayfa
            };

            // API çaðrýsý
            var result = await ControlNotesAppService.GetListAsync(input);

            // Gelen veriyi listeye at
            controlNotes = result.Items.ToList();

            StateHasChanged(); // Sayfayý güncelle
        }
        private async Task CreateControlNoteAsync()
        {
            await HandleError(async () =>
            {
                var createDto = new ControlNoteCreateDto
                {
                    ProtocolId = ProtocolId,
                    NoteDate = controlNoteCreateDto.NoteDate,
                    Note = controlNoteCreateDto.Note
                };

                await ControlNotesAppService.CreateAsync(createDto);
                await ShowToast("Ýþlem Baþarýlý", true);
                await LoadControlNotesAsync();
            });
        }

        #endregion

        #region Patient History
        private bool isExistingPatienthistory;
        private EducationLevel? selectedEducationLevel;
        private PatientHistoryDto patientHistoryDto = new PatientHistoryDto();
        private List<LookupDto<EducationLevel>> EducationLevelsCollection;
        private bool isDetailsModalOpen;

       
        private string GetMaritalStatusButtonClass(MaritalStatus status)
        {
            return patientHistoryDto.MaritalStatus == status
                ? "e-primary border-none" // Seçili durum için
                : "e-outline-primary border-none"; // Seçilmemiþ durum için
        }
        private void SetMaritalStatus(MaritalStatus status)
        {
            patientHistoryDto.MaritalStatus = status;
        }

        #region Create
        private async Task CreatePatientHistoryAsync()
        {
            await HandleError(async () =>
            {
                var createDto = new PatientHistoryCreateDto
                {
                    PatientId = PatientId,
                    Habit = patientHistoryDto.Habit,
                    Disease = patientHistoryDto.Disease,
                    Medicine = patientHistoryDto.Medicine,
                    Operation = patientHistoryDto.Operation,
                    Vaccination = patientHistoryDto.Vaccination,
                    Allergy = patientHistoryDto.Allergy,
                    SpecialCondition = patientHistoryDto.SpecialCondition,
                    Device = patientHistoryDto.Device,
                    Therapy = patientHistoryDto.Therapy,
                    Job = patientHistoryDto.Job,
                    EducationLevel = patientHistoryDto.EducationLevel,
                    MaritalStatus = patientHistoryDto.MaritalStatus,

                };

                await PatientHistoriesAppService.CreateAsync(createDto);
                await ShowToast("Ýþlem Baþarýlý", true);
            });
        }
        #endregion

        #region UPDATE
        private async Task UpdatePatientHistoryAsync()
        {
            await HandleError(async () =>
            {
                var updateDto = new PatientHistoryUpdateDto
                {
                    Id = patientHistoryDto.Id,
                    PatientId = PatientId,
                    Habit = patientHistoryDto.Habit,
                    Disease = patientHistoryDto.Disease,
                    Medicine = patientHistoryDto.Medicine,
                    Operation = patientHistoryDto.Operation,
                    Vaccination = patientHistoryDto.Vaccination,
                    Allergy = patientHistoryDto.Allergy,
                    SpecialCondition = patientHistoryDto.SpecialCondition,
                    Device = patientHistoryDto.Device,
                    Therapy = patientHistoryDto.Therapy,
                    Job = patientHistoryDto.Job,
                    EducationLevel = patientHistoryDto.EducationLevel,
                    MaritalStatus = patientHistoryDto.MaritalStatus,
                };

                await PatientHistoriesAppService.UpdateAsync(updateDto);
                await ShowToast("Ýþlem Baþarýlý", true);
            });
        }
        #endregion

        private async Task SavePatientHistoryAsync()
        {
            try
            {
                if (isExistingPatienthistory)
                {
                    await UpdatePatientHistoryAsync();

                }
                else
                {
                    await CreatePatientHistoryAsync();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Kaydetme iþlemi sýrasýnda hata oluþtu: {ex.Message}");
            }
        }
        private async Task LoadPatientHistoryAsync()
        {
            try
            {
                var result = await PatientHistoriesAppService.GetByPatientIdAsync(PatientId);

                if (result != null)
                {
                    patientHistoryDto = result;
                    isExistingPatienthistory = true;

                }
                else
                {

                    patientHistoryDto = new PatientHistoryDto();
                    isExistingPatienthistory = false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Hata oluþtu: {ex.Message}");

            }
        }
        private void OpenDetailsModal()
        {
            isDetailsModalOpen = true;
        }
        private void CloseDetailsModal()
        {
            isDetailsModalOpen = false;
        }

        #endregion

    }
}