using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeManager(IAppointmentTypeRepository appointmentTypeRepository) : DomainService
    {
        public virtual async Task<AppointmentType> CreateAsync(
        string name
        )
        {
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), AppointmentTypeConst.NameMaxLength, AppointmentTypeConst.NameMinLength);

            var testGroup = new AppointmentType(
             GuidGenerator.Create(),
            name
            );

            return await appointmentTypeRepository.InsertAsync(testGroup);
        }

        public virtual async Task<AppointmentType> UpdateAsync(
            Guid id,
        string name
            )
        {
            var testGroup = await appointmentTypeRepository.GetAsync(id);
            testGroup.SetName(name);
            return await appointmentTypeRepository.UpdateAsync(testGroup);
        }
    }
}
