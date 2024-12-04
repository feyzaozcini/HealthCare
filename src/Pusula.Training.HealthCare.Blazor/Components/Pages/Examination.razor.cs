using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.PshychologicalStates;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class Examination
    {
        [Parameter]
        public Guid ProtocolId { get; set; }

        [Parameter]
        public Guid PatientId { get; set; }

        private PatientDto Patient { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            // Hasta bilgilerini çek
            Patient = await PatientsAppService.GetAsync(PatientId);
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }

        #region ANAMNESIS
        private bool isCollapsed = false; // Baþlangýçta açýk
        private AnamnesisCreateDto anamnesisCreateDto = new();

        private int durationValue = 0; // Girilen süre (ör. 2)
        private string selectedUnit = "Gün"; // Varsayýlan birim (ör. Gün)
        private DateTime selectedDate = DateTime.Now; // Varsayýlan tarih (bugün)

        private void ToggleCollapse()
        {
            isCollapsed = !isCollapsed; // Açýk/Kapalý durumunu deðiþtir
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
                "Gün" => now.AddDays(-durationValue),
                "Hafta" => now.AddDays(-durationValue * 7),
                "Ay" => now.AddMonths(-durationValue),
                "Yýl" => now.AddYears(-durationValue),
                _ => now
            };
        }
        private async Task SubmitAnamnesisAsync()
        {
            
            await AnamnesisAppService.CreateAsync(anamnesisCreateDto);
            // Baþarý durumunda gerekli iþlemleri yap
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
    }
}