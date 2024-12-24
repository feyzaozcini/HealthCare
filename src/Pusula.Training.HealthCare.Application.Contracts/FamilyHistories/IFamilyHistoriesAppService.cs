using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.FamilyHistories
{
    public interface IFamilyHistoriesAppService : IApplicationService
    {
        Task<FamilyHistoryDto> GetAsync(Guid id);
        Task<FamilyHistoryDto> GetByPatientIdAsync(Guid patientId);
        Task DeleteAsync(Guid id);
        Task<FamilyHistoryDto> CreateAsync(FamilyHistoryCreateDto input);
        Task<FamilyHistoryDto> UpdateAsync(FamilyHistoryUpdateDto input);

    }
}
