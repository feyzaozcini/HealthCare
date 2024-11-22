using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public class ExaminationDiagnosis : Entity<Guid>, ISoftDelete
    {
        [NotNull]
        public DiagnosisType DiagnosisType { get; set; }

        [NotNull]
        public DateTime InitialDate { get; set; }
        public string Note { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ProtocolId { get; set; }

        public Guid DiagnosisId { get; set; }

       

        protected ExaminationDiagnosis()
        {
            Note = string.Empty;
            InitialDate = DateTime.Now;
        }

        public ExaminationDiagnosis(Guid id,DiagnosisType diagnosisType, Guid protocolId, DateTime initialDate, Guid diagnosisId, string note)
        {
            Id = id;
            DiagnosisType = diagnosisType;
            ProtocolId = protocolId;
            InitialDate = initialDate;
            DiagnosisId = diagnosisId;
            Note = note;
          
            
        }
    }
}
