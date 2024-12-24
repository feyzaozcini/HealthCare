using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.PatientHistories
{
    public class PatientHistoryManager(IPatientHistoryRepository patientHistoryRepository) : DomainService
    {
        public virtual async Task<PatientHistory> CreateAsync(Guid patientId, string habit, string disease, string medicine, 
            string operation, string vaccination,string allergy, string specialCondition, string device, string therapy, string job,
            EducationLevel educationLevel,MaritalStatus maritalStatus)
        {
            var patientHistory = new PatientHistory(GuidGenerator.Create(), patientId, habit, disease, medicine, operation,
            vaccination, allergy, specialCondition, device, therapy, job, educationLevel, maritalStatus);

            return await patientHistoryRepository.InsertAsync(patientHistory);
        }

        public virtual async Task<PatientHistory> UpdateAsync(Guid id,Guid patientId, string habit, string disease, string medicine,
           string operation, string vaccination, string allergy, string specialCondition, string device, string therapy, string job,
           EducationLevel educationLevel, MaritalStatus maritalStatus)
        {
            var patientHistory = await patientHistoryRepository.GetAsync(id);
            patientHistory.SetPatientId(patientId);
            patientHistory.SetHabit(habit);
            patientHistory.SetDisease(disease);
            patientHistory.SetMedicine(medicine);
            patientHistory.SetVaccination(vaccination);
            patientHistory.SetAllergy(allergy);
            patientHistory.SetSpecialCondition(specialCondition);
            patientHistory.SetDevice(device);
            patientHistory.SetTherapy(therapy);
            patientHistory.SetJob(job);
            patientHistory.SetEducationLevel(educationLevel);
            patientHistory.SetMaritalStatus(maritalStatus);


            return await patientHistoryRepository.UpdateAsync(patientHistory);
        }
    }
}
