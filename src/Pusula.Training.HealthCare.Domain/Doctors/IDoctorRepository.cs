using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Doctors
{
    public interface IDoctorRepository : IRepository<Doctor,Guid>
    {
        Task<DoctorWithNavigationProperties> GetWithNavigationProperties(
            Guid id,
            CancellationToken cancellationToken = default);
        Task<List<DoctorWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
          string? filterText = null,
          Guid? userId = null, 
          Guid? titleId = null,
          string? identityNumber = null,
          DateTime? birthDateMin = null,
          DateTime? birthDateMax = null,
          Gender? gender = null,
          string? sorting = null,
          int maxResultCount = int.MaxValue,
          int skipCount = 0,
          CancellationToken cancellationToken = default);

        Task<List<Doctor>> GetListAsync(
            string? filterText = null,
            Guid? userId = null,
            Guid? titleId = null,
            string? identityNumber = null,
            DateTime? birthDateMin = null,
            DateTime? birthDateMax = null,
            Gender? gender = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string? filterText = null,
            Guid? userId = null,
            Guid? titleId = null,
            string? identityNumber = null,
            DateTime? birthDateMin = null,
            DateTime? birthDateMax = null,
            Gender? gender = null,
            CancellationToken cancellationToken = default);

        Task DeleteAllAsync(
            string? filterText = null,
            Guid? userId = null,
            Guid? titleId = null,
            string? identityNumber = null,
            DateTime? birthDateMin = null,
            DateTime? birthDateMax = null,
            Gender? gender = null,
            CancellationToken cancellationToken = default);

        
    }
}
