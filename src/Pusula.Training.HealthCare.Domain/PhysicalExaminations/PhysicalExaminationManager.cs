using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.PhysicalExaminations
{
    public class PhysicalExaminationManager(IPhysicalExaminationRepository physicalExaminationRepository) : DomainService
    {
      public virtual async Task<PhysicalExamination> CreateAsync(
         Guid protocolId, 
         decimal? weight, 
         decimal? height,
         decimal? bmi,
         decimal? vya,
         decimal? temperature, 
         int? pulse, 
         int? systolicBP, 
         int? diastolicBP, 
         int? spo2,
        string note)
        {
            var physicalExamination = new PhysicalExamination(GuidGenerator.Create(),protocolId,weight,height,bmi,vya,temperature,pulse,systolicBP,diastolicBP,spo2,note);

            return await physicalExaminationRepository.InsertAsync(physicalExamination);
        }


        public virtual async Task<PhysicalExamination> UpdateAsync(
         Guid id,
         Guid protocolId,
         decimal? weight,
         decimal? height,
         decimal? bmi,
         decimal? vya,
         decimal? temperature,
         int? pulse,
         int? systolicBP,
         int? diastolicBP,
         int? spo2,
        string note)
        {
            var physicalExamination = await physicalExaminationRepository.GetAsync(id); //veya protocol Id de olabilir
            physicalExamination.SetProtocolId(protocolId);
            physicalExamination.SetWeight(weight);
            physicalExamination.SetHeight(height);
            physicalExamination.SetBMI(bmi);
            physicalExamination.SetVYA(vya);
            physicalExamination.SetTemperature(temperature);
            physicalExamination.SetPulse(pulse);
            physicalExamination.SetSystolicBP(systolicBP);
            physicalExamination.SetDiastolicBP(diastolicBP);
            physicalExamination.SetSPO2(spo2);
            physicalExamination.SetNote(note);
            return await physicalExaminationRepository.UpdateAsync(physicalExamination);

        }
    }
}
