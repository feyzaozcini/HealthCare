using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.PatientHistories
{
    public interface IPatientHistoriesAppService : IApplicationService
    {
        Task<PatientHistoryDto> GetAsync(Guid id);
        Task<PatientHistoryDto> GetByPatientIdAsync(Guid patientId);
        Task DeleteAsync(Guid id);
        Task<PatientHistoryDto> CreateAsync(PatientHistoryCreateDto input);
        Task<PatientHistoryDto> UpdateAsync(PatientHistoryUpdateDto input);
    }
}
