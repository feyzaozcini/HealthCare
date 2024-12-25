using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.PatientHistories
{
    public class PatientHistory : FullAuditedAggregateRoot<Guid>
    {
        public Guid PatientId { get; private set; }

        public Patient Patient { get; private set; } = null!;

        public string Habit { get; private set; } = string.Empty;

        public string Disease { get; private set; } = string.Empty;

        public string Medicine { get; private set; } = string.Empty;

        public string Operation { get; private set; } = string.Empty;

        public string Vaccination { get; private set; } = string.Empty;

        public string Allergy { get; private set; } = string.Empty;

        public string SpecialCondition { get; private set; } = string.Empty;

        public string Device { get; private set; } = string.Empty;

        public string Therapy { get; private set; } = string.Empty;

        public string Job { get; private set; } = string.Empty;

        public EducationLevel EducationLevel { get; private set; }

        public MaritalStatus MaritalStatus { get; private set; }

        protected PatientHistory()
        {
            Habit = string.Empty;
            Disease = string.Empty;
            Medicine = string.Empty;
            Operation = string.Empty;
            Vaccination = string.Empty;
            Allergy = string.Empty;
            SpecialCondition = string.Empty;
            Device = string.Empty;
            Therapy = string.Empty;
            Job = string.Empty;
            EducationLevel = EducationLevel.Unspecified; //Belirtilmemiş
            MaritalStatus = MaritalStatus.Unspecified; //Belirtilmemiş
        }

        public PatientHistory(Guid id,Guid patientId, string habit, string disease, string medicine, string operation, string vaccination, 
            string allergy, string specialCondition, string device, string therapy, string job, EducationLevel educationLevel,
            MaritalStatus maritalStatus)
        {
            Id = id;
            SetPatientId(patientId);
            SetHabit(habit);
            SetDisease(disease);
            SetMedicine(medicine);
            SetOperation(operation);
            SetVaccination(vaccination);
            SetAllergy(allergy);
            SetSpecialCondition(specialCondition);
            SetDevice(device);
            SetTherapy(therapy);
            SetJob(job);
            SetEducationLevel(educationLevel);
            SetMaritalStatus(maritalStatus);
        }

        public void SetPatientId(Guid patientId)
        {
            PatientId = Check.NotNull(patientId, nameof(patientId));
        }

        public void SetHabit(string habit)
        {
            Habit = habit; // Null olabilir, bu yüzden kontrol eklenmedi.
        }

        public void SetDisease(string disease)
        {
            Disease = disease; // Null olabilir.
        }

        public void SetMedicine(string medicine)
        {
            Medicine = medicine; // Null olabilir.
        }

        public void SetOperation(string operation)
        {
            Operation = operation; // Null olabilir.
        }

        public void SetVaccination(string vaccination)
        {
            Vaccination = vaccination; // Null olabilir.
        }

        public void SetAllergy(string allergy)
        {
            Allergy = allergy; // Null olabilir.
        }

        public void SetSpecialCondition(string specialCondition)
        {
            SpecialCondition = specialCondition; // Null olabilir.
        }

        public void SetDevice(string device)
        {
            Device = device; // Null olabilir.
        }

        public void SetTherapy(string therapy)
        {
            Therapy = therapy; // Null olabilir.
        }

        public void SetJob(string job)
        {
            Job = Check.NotNullOrWhiteSpace(job, nameof(job));
        }

        public void SetEducationLevel(EducationLevel? educationLevel)
        {
            EducationLevel = educationLevel ?? EducationLevel.Unspecified; 
        }

        public void SetMaritalStatus(MaritalStatus? maritalStatus)
        {
            MaritalStatus = maritalStatus ?? MaritalStatus.Unspecified;
        }
    }
}
