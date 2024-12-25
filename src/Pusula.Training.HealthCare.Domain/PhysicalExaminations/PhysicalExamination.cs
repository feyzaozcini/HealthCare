using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PhysicalExaminations
{
    public class PhysicalExamination :Entity<Guid>
    {
        public Guid ProtocolId { get; private set; }

        public decimal? Weight { get; private set; } // Kilo (kg)
        public decimal? Height { get; private set; } // Boy (cm)
        public decimal? BMI { get; private set; } // Vücut Kitle İndeksi
        public decimal? VYA { get; private set; } // Vücut Yağ Oranı
        public decimal? Temperature { get; private set; } // Ateş (°C)
        public int? Pulse { get; private set; } // Nabız
        public int? SystolicBP { get; private set; } // Sistolik Kan Basıncı
        public int? DiastolicBP { get; private set; } // Diyastolik Kan Basıncı
        public int? SPO2 { get; private set; } // Oksijen Doyumu

        public string Note { get; private set; } = string.Empty;    // Muayene notu


        protected PhysicalExamination()
        {
            Note=string.Empty;
        }


        public PhysicalExamination(Guid id,Guid protocolId,decimal? weight,decimal? height, decimal? bmi,decimal? vya,decimal? temperature,int? pulse,int? systolicBP, int? diastolicBP,int? spo2, string note)
        {
            Id = id;
            SetProtocolId(protocolId);
            SetWeight(weight);
            SetHeight(height);
            SetBMI(bmi);
            SetVYA(vya);
            SetTemperature(temperature);
            SetPulse(pulse);
            SetSystolicBP(systolicBP);
            SetDiastolicBP(diastolicBP);
            SetSPO2(spo2);
            SetNote(note);
        }

        public void SetProtocolId(Guid protocolId)
        {
            Check.NotNull(protocolId, nameof(protocolId));
            ProtocolId = protocolId;
        }
   
        public void SetWeight(decimal? weight)
        {
            Weight = weight;
        }

        public void SetHeight(decimal? height)
        {
            Height = height;
        }

        public void SetBMI(decimal? bmi)
        {
            BMI = bmi;
        }

        public void SetVYA(decimal? vya)
        {
            VYA = vya;
        }

        public void SetTemperature(decimal? temperature)
        {
            Temperature = temperature;
        }

        public void SetPulse(int? pulse)
        {
            Pulse = pulse;
        }

        public void SetSystolicBP(int? systolicBP)
        {
            SystolicBP = systolicBP;
        }

        public void SetDiastolicBP(int? diastolicBP)
        {
            DiastolicBP = diastolicBP;
        }

        public void SetSPO2(int? spo2)
        {
            SPO2 = spo2;
        }

        public void SetNote(string note)
        {
            Note = note;
        }
    }
}
