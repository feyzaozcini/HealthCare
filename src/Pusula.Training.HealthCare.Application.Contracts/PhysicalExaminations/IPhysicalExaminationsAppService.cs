using Pusula.Training.HealthCare.Anamneses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.PhysicalExaminations
{
    public interface IPhysicalExaminationsAppService : IApplicationService
    {
        Task<PhysicalExaminationWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<PhysicalExaminationDto> GetAsync(Guid id);

        Task<PhysicalExaminationDto> GetWithProtocolIdAsync(Guid protocolId);

        Task DeleteAsync(Guid id);

        Task<PhysicalExaminationDto> CreateAsync(PhysicalExaminationCreateDto input);

        Task<PhysicalExaminationDto> UpdateAsync(PhysicalExaminationUpdateDto input);
    }
}
