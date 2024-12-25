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
        public DiagnosisType DiagnosisType { get; private set; }

        [NotNull]
        public DateTime InitialDate { get; private set; }
        public string Note { get; private set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public Guid ProtocolId { get; private set; }

        public Guid DiagnosisId { get; private set; }

       

        protected ExaminationDiagnosis()
        {
            Note = string.Empty;
            InitialDate = DateTime.Now;
        }

        public ExaminationDiagnosis(Guid id,DiagnosisType diagnosisType, Guid protocolId, DateTime initialDate, Guid diagnosisId, string note)
        {

            Id = id;
            SetDiagnosisType(diagnosisType);
            SetProtocolId(protocolId);
            SetInitialDate(initialDate);
            SetDiagnosisId(diagnosisId);
            SetNote(note);
        }

       
        public void SetDiagnosisType(DiagnosisType diagnosisType)
        {
            Check.NotNull(diagnosisType, nameof(diagnosisType));
            DiagnosisType = diagnosisType;
        }

      
        public void SetProtocolId(Guid protocolId)
        {
            Check.NotNull(protocolId, nameof(protocolId));
            ProtocolId = protocolId;
        }

        
        public void SetInitialDate(DateTime initialDate)
        {
            Check.NotNull(initialDate, nameof(initialDate));
            InitialDate = initialDate;
        }

     
        public void SetDiagnosisId(Guid diagnosisId)
        {
            Check.NotNull(diagnosisId, nameof(diagnosisId));
            DiagnosisId = diagnosisId;
        }

       
        public void SetNote(string note)
        {
            Note = note;
        }
    }
}
