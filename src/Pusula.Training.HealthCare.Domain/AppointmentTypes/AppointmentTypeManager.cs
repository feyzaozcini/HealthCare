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

            var appointmentType = new AppointmentType(
             GuidGenerator.Create(),
            name
            );

            return await appointmentTypeRepository.InsertAsync(appointmentType);
        }

        public virtual async Task<AppointmentType> UpdateAsync(
            Guid id,
        string name
            )
        {
            var appointmentType = await appointmentTypeRepository.GetAsync(id);
            appointmentType.SetName(name);
            return await appointmentTypeRepository.UpdateAsync(appointmentType);
        }
    }
}
