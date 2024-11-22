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
        // Foreign Key
        public Guid ProtocolId { get; set; }

        // Physical Examination Properties
        public decimal? Weight { get; set; } // Kilo (kg)
        public decimal? Height { get; set; } // Boy (cm)
        public decimal? BMI { get; set; } // Vücut Kitle İndeksi
        public decimal? VYA { get; set; } // Vücut Yağ Oranı
        public decimal? Temperature { get; set; } // Ateş (°C)
        public int? Pulse { get; set; } // Nabız
        public int? SystolicBP { get; set; } // Sistolik Kan Basıncı
        public int? DiastolicBP { get; set; } // Diyastolik Kan Basıncı
        public int? SPO2 { get; set; } // Oksijen Doyumu

        public string Note { get; set; } // Muayene notu

        // Protected Constructor for EF Core
        protected PhysicalExamination()
        {
            Note=string.Empty;
        }


        public PhysicalExamination(Guid id,Guid protocolId,decimal? weight,decimal? height, decimal? bmi,decimal? vya,decimal? temperature,int? pulse,int? systolicBP, int? diastolicBP,int? spo2, string note)
        {
            Id = id;
            ProtocolId = protocolId;
            Weight = weight;
            Height = height;
            BMI = bmi;
            VYA = vya;
            Temperature = temperature;
            Pulse = pulse;
            SystolicBP = systolicBP;
            DiastolicBP = diastolicBP;
            SPO2 = spo2;
            Note = note;
        }
    }
}
