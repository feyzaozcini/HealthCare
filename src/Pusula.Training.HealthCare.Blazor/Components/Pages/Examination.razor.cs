using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.FallRisks;
using Pusula.Training.HealthCare.PainDetails;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.PhysicalExaminations;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.PshychologicalStates;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;
using YamlDotNet.Core.Tokens;

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

        protected override async Task OnInitializedAsync()
        {
            // Hasta bilgilerini �ek
            Patient = await PatientsAppService.GetAsync(PatientId);
            await LoadFallRiskAsync();
            await LoadPhysicalExaminationAsync();
            await LoadPainTypesAsync();
            await LoadPainDetailAsync();
            await LoadIcdListAsync();
            await LoadExaminationDiagnosesAsync();
            await GetProtocolAsync();
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
        private bool isCollapsed = false; // Ba�lang��ta a��k
        private AnamnesisCreateDto anamnesisCreateDto = new();

        private int durationValue = 0; // Girilen s�re (�r. 2)
        private string selectedUnit = "G�n"; // Varsay�lan birim (�r. G�n)
        private DateTime selectedDate = DateTime.Now; // Varsay�lan tarih (bug�n)

        private void ToggleCollapse()
        {
            isCollapsed = !isCollapsed; // A��k/Kapal� durumunu de�i�tir
        }
        private void UpdateUnit(string unit)
        {
            selectedUnit = unit;
            UpdateDate();
        }

        private void UpdateDate()
        {
            var now = DateTime.Now;
            selectedDate = selectedUnit switch
            {
                "Saat" => now.AddHours(-durationValue),
                "G�n" => now.AddDays(-durationValue),
                "Hafta" => now.AddDays(-durationValue * 7),
                "Ay" => now.AddMonths(-durationValue),
                "Y�l" => now.AddYears(-durationValue),
                _ => now
            };
        }
        private async Task SubmitAnamnesisAsync()
        {
            
            await AnamnesisAppService.CreateAsync(anamnesisCreateDto);
            // Ba�ar� durumunda gerekli i�lemleri yap
        }
        private string GetButtonClass(string unit)
        {
            return unit == selectedUnit ? "e-primary" : "e-outline";
        }

        #endregion

        #region psychologicalState
        private MentalState selectedMentalState;
        private string description = string.Empty;

        private void SetMentalState(MentalState state)
        {
            selectedMentalState = state;
        }

        private string GetButtonClass(MentalState state)
        {
            return state == selectedMentalState ? "e-primary" : "e-outline";
        }
        #endregion

        #region FALLRISK
        //yarina not 3 method yaz create update ve load dursun sanirim save icinde kayit varsa updatei cagir yoksa create cagir uzatmadan
        //son olarakta eger yok isaretlenirsa kayit silinecek 
        //ince detay kayit olmasada default kayit geliyo oraya artik son sprintte bakarim herhal
        //private FallRiskCreateDto fallRiskCreateDto = new FallRiskCreateDto();
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
                    // G�ncelleme DTO'su haz�rla
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

                    // Backend'e g�ncelleme iste�i g�nder
                    await FallRiskAppService.UpdateAsync(updateDto);
                }
                else
                {
                   
                    Console.WriteLine("Kay�t bulunamad�, �nce bir kay�t olu�turun.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata olu�tu: {ex.Message}");
            }
        }
        private async Task LoadFallRiskAsync()
        {
            try
            {
                // API iste�i (ProtocolId ile)
                var protocolId = ProtocolId; 
                var result = await FallRiskAppService.GetWithProtocolIdAsync(protocolId);

                if (result != null)
                {
                    // Kay�t bulundu, DTO'yu doldur
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

                    isExistingRecord = true; // Kay�t bulundu
                }
                else
                {
                  
                    fallRiskDto = new FallRiskDto();
                    isFallRisk = false; // D��me riski yok
                    score = 0;
                    fallRiskDescription = string.Empty;
                    isExistingRecord = false;
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Hata olu�tu: {ex.Message}");
            }
        }

        private async Task SaveFallRiskAsync()
        {
            try
            {
                if (isExistingRecord)
                {
                    isFallRisk = true;
                    // G�ncelleme i�lemi
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

                    isExistingRecord = true; // Art�k kay�t mevcut
                    isFallRiskModalOpen = false;
                    StateHasChanged();
                }
                else
                {
                    // Yeni kay�t olu�turma
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
                   
                    // DTO'yu g�ncelle ve mevcut kay�t olarak i�aretle
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

                    isExistingRecord = true; // Art�k kay�t mevcut
                    isFallRiskModalOpen = false;
                    StateHasChanged();

                }

                // ��lem tamamland���nda modal� kapat
                isFallRiskModalOpen = false;
                StateHasChanged();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata olu�tu: {ex.Message}");
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
                // API iste�i (ProtocolId ile)
                var protocolId = ProtocolId; // ProtocolId burada ilgili kayd�n ID'si olacak
                var result = await PhysicalExaminationAppService.GetWithProtocolIdAsync(protocolId);

                if (result != null && result.Id != Guid.Empty)
                {
                    // Gelen DTO'yu kullan
                    physicalExaminationDto = result;

                    // Gerekli alanlar� doldur
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
                    // Yeni kay�t olu�turma durumu
                    physicalExaminationDto = new PhysicalExaminationDto();
                    physicalExaminationCreateDto = new PhysicalExaminationCreateDto { ProtocolId = protocolId };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata olu�tu: {ex.Message}");
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
                    // Kay�t bulundu, DTO'yu g�ncelle
                    physicalExaminationDto = result;
                    isExistingPhysicalExamination = true; // Kay�t bulundu
                }
                else
                {
                    // Kay�t bulunamad�, yeni DTO olu�tur
                    physicalExaminationDto = new PhysicalExaminationDto();
                    isExistingPhysicalExamination = false; // Yeni kay�t olu�turulacak
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata olu�tu: {ex.Message}");
            }
        }

        private void CalculateBMIandVYA()
        {
            if (physicalExaminationDto.Weight.HasValue && physicalExaminationDto.Height.HasValue)
            {
                // BMI Hesaplama (Boy cm'den metreye �evriliyor)
                decimal heightInMeters = physicalExaminationDto.Height.Value / 100;
                physicalExaminationDto.BMI = physicalExaminationDto.Weight.Value / (heightInMeters * heightInMeters);

                // VYA Hesaplama (�rnek form�l, ihtiyaca g�re de�i�tirilir)
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

                // Kay�t olu�turma i�lemi
                if (!isExistingPhysicalExamination)
                {
                    // Yeni kay�t olu�turulacak
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

                    // API �a�r�s� ile yeni kay�t olu�tur
                    var createdExamination = await PhysicalExaminationAppService.CreateAsync(createDto);

                    // Yeni kayd� DTO'ya aktar
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


                    isExistingPhysicalExamination = true; // Art�k kay�t mevcut
                }
                else
                {
                    // Mevcut kayd� g�ncelle
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

                    // API �a�r�s� ile mevcut kayd� g�ncelle
                    var updatedExamination = await PhysicalExaminationAppService.UpdateAsync(updateDto);

                    // G�ncellenen kayd� DTO'ya aktar
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

                StateHasChanged(); // UI'yi g�ncelle
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata olu�tu: {ex.Message}");
            }
        }


        #endregion

        #region PAIN DETAILS
        private bool isPain = false;  // Ba�lang��ta a�r� yok
        private bool isExistingPain; 

        private List<LookupDto<Guid>> painTypes = new List<LookupDto<Guid>>();
        private int painDurationValue = 0;                   
        private List<string> timeUnits = new List<string> { "Saat", "G�n", "Hafta", "Ay", "Y�l" };
        private string painSelectedUnit = "G�n";  
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
                // Kay�t olu�turma i�lemi
                if (!isExistingPain)
                {
                    // Yeni kay�t olu�turulacak
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

                    // API �a�r�s� ile yeni kay�t olu�tur
                    var createdPainDetail = await PainDetailAppService.CreateAsync(createDto);

                    // Yeni kayd� DTO'ya aktar
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


                    isExistingPain = true; // Art�k kay�t mevcut
                }
                else
                {
                    // Mevcut kayd� g�ncelle
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

                    // API �a�r�s� ile mevcut kayd� g�ncelle
                    var updatedPainDetail = await PainDetailAppService.UpdateAsync(updateDto);

                    // G�ncellenen kayd� DTO'ya aktar
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

                StateHasChanged(); // UI'yi g�ncelle
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata olu�tu: {ex.Message}");
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
                    // Kay�t bulundu, DTO'yu g�ncelle
                    painDetailDto = result;
                    isExistingPain = true; // Kay�t bulundu
                    isPain = true;
                }
                else
                {
                    // Kay�t bulunamad�, yeni DTO olu�tur
                    painDetailDto = new PainDetailDto();
                    isExistingPain = false; // Yeni kay�t olu�turulacak
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata olu�tu: {ex.Message}");
            }
        }

        private void UpdatePainStartDate()
        {
            var now = DateTime.Now;
            painStartDate = painSelectedUnit switch
            {
                "Saat" => now.AddHours(-painDurationValue),
                "G�n" => now.AddDays(-painDurationValue),
                "Hafta" => now.AddDays(-painDurationValue * 7),
                "Ay" => now.AddMonths(-painDurationValue),
                "Y�l" => now.AddYears(-painDurationValue),
                _ => now
            };
            StateHasChanged(); // UI'yi g�ncelle
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
                Console.WriteLine($"Hata olu�tu: {ex.Message}");
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
            return painTypeId == selectedPainTypeId ? "e-primary" : "e-outline"; // Se�ili olan i�in farkl� bir s�n�f
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
                // DeleteAsync metodu ile silme i�lemi
                await ExaminationDiagnosisAppService.DeleteAsync(id);

                // Silme i�leminden sonra listeyi yenile
                await LoadExaminationDiagnosesAsync();
            }
            catch (Exception ex)
            {
                // Hata y�netimi
                Console.WriteLine($"Silme s�ras�nda bir hata olu�tu: {ex.Message}");
            }
        }
        private async Task LoadExaminationDiagnosesAsync()
        {
           
            var input = new GetExaminationDiagnosisInput
            {
                ProtocolId = ProtocolId, // URL'den ald���n�z Protokol ID
                FilterText = null,       // Filtre yok
                DiagnosisType = null,    // T�m tan� tipleri
                InitialDate = null,      // Tarih filtresi yok
                Note = null,             // Not filtresi yok
                DiagnosisId = null,      // Tan� ID filtresi yok
                //MaxResultCount = 100,    // Maksimum sayfa boyutu
                //SkipCount = 0,           // �lk sayfa
                //Sorting = "InitialDate DESC" // Tarihe g�re azalan s�ralama
            };

            // API �a�r�s�
            var result = await ExaminationDiagnosisAppService.GetListAsync(input);

            // Gelen veriyi listeye at
            ExaminationDiagnoses = result.Items.ToList();

            StateHasChanged(); // Sayfay� g�ncelle
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

            // API �a�r�s� ile yeni kay�t olu�tur
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
                Filter = null, // E�er bir filtreleme gerekiyorsa buraya eklenebilir
                MaxResultCount = 250,
                SkipCount = 0
            };

            var result = await ExaminationDiagnosisAppService.GetDiagnosisLookupAsync(input);
            IcdList = result.Items.ToList(); // Servisten gelen verileri IcdList'e at�yoruz
        }

        #endregion
    }
}