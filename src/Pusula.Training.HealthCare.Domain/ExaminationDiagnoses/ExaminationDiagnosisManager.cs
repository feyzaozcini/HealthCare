using Pusula.Training.HealthCare.Diagnoses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public class ExaminationDiagnosisManager(IExaminationDiagnosisRepository examinationDiagnosisRepository) :DomainService
    {
        public virtual async Task<ExaminationDiagnosis> CreateAsync(DiagnosisType diagnosisType, Guid protocolId, DateTime initialDate,
            Guid diagnosisId, string note)
        {
            var examinationDiagnosis = new ExaminationDiagnosis(Guid.NewGuid(), diagnosisType, protocolId, 
                initialDate, diagnosisId, note);

            return await examinationDiagnosisRepository.InsertAsync(examinationDiagnosis);

        }


        public virtual async Task<ExaminationDiagnosis> UpdateAsync(Guid id, DiagnosisType diagnosisType, Guid protocolId, DateTime initialDate, Guid diagnosisId, string note)
        {

            var examinationDiagnosis = await examinationDiagnosisRepository.GetAsync(id);

            examinationDiagnosis.DiagnosisType = diagnosisType;
            examinationDiagnosis.ProtocolId = protocolId;
            examinationDiagnosis.InitialDate = initialDate;
            examinationDiagnosis.DiagnosisId = diagnosisId;
            examinationDiagnosis.Note = note;

            return await examinationDiagnosisRepository.UpdateAsync(examinationDiagnosis);

        }
    }
}
