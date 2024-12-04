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
            // Hasta bilgilerini �ek
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
    }
}