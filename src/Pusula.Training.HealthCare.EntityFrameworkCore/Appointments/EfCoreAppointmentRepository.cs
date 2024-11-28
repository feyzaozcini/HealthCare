using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Appointments
{
    public class EfCoreAppointmentRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
        : EfCoreRepository<HealthCareDbContext, Appointment, Guid>(dbContextProvider), IAppointmentRepository
    {
        public virtual async Task DeleteAllAsync(
            string? filterText = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string? note = null, 
            AppointmentStatus? appointmentStatus = null, 
            Guid? patientId = null, 
            Guid? doctorId = null, 
            Guid? departmentId = null, 
            Guid? appointmentTypeId = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, startDate, endDate, note, appointmentStatus, patientId, doctorId, departmentId, appointmentTypeId);

            var ids = query.Select(x => x.Appointment.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));

        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string? note = null, 
            AppointmentStatus? appointmentStatus = null, 
            Guid? patientId = null, 
            Guid? doctorId = null, 
            Guid? departmentId = null, 
            Guid? appointmentTypeId = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, startDate, endDate, note, appointmentStatus, patientId, doctorId, departmentId, appointmentTypeId);

            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<Appointment>> GetListAsync(
            string? filterText = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string? note = null, 
            AppointmentStatus? appointmentStatus = null,
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Guid? appointmentTypeId = null,
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query=ApplyFilter(await GetQueryableAsync(), filterText, startDate, endDate, note, appointmentStatus, patientId, doctorId, departmentId, appointmentTypeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentConst.GetDefaultSorting(false) : sorting);
            return await query.Page(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<List<AppointmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string? note = null, 
            AppointmentStatus? appointmentStatus = null, 
            Guid? patientId = null, 
            Guid? doctorId = null, 
            Guid? departmentId = null, 
            Guid? appointmentTypeId = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, startDate, endDate, note, appointmentStatus, patientId, doctorId, departmentId, appointmentTypeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentConst.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<AppointmentWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(appointment => new AppointmentWithNavigationProperties
                {
                    Appointment = appointment,
                    Patient = dbContext.Set<Patient>().FirstOrDefault(c => c.Id == appointment.PatientId)!,
                    Doctor = dbContext.Set<Doctor>().FirstOrDefault(c => c.Id == appointment.DoctorId)!,
                    Department = dbContext.Set<Department>().FirstOrDefault(c => c.Id == appointment.DepartmentId)!,
                    AppointmentType = dbContext.Set<AppointmentType>().FirstOrDefault(c => c.Id == appointment.AppointmentTypeId)!
                })
                .FirstOrDefault()!;
        }


        #region ApplyFilter and Queryable
        protected virtual IQueryable<Appointment> ApplyFilter(
            IQueryable<Appointment> query,
            string? filterText = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? note = null,
            AppointmentStatus? appointmentStatus = null,
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Guid? appointmentTypeId = null) =>
                query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.PatientId.ToString()!.Contains(filterText!) || e.DoctorId.ToString()!.Contains(filterText!) || e.DepartmentId.ToString()!.Contains(filterText!) || e.AppointmentTypeId.ToString()!.Contains(filterText!))
                    .WhereIf(startDate.HasValue, e => e.StartDate >= startDate!.Value)
                    .WhereIf(endDate.HasValue, e => e.EndDate <= endDate!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note!.Contains(note!))
                    .WhereIf(appointmentStatus.HasValue, e => e.AppointmentStatus == appointmentStatus)
                    .WhereIf(patientId.HasValue, e => e.PatientId == patientId)
                    .WhereIf(doctorId.HasValue, e => e.DoctorId == doctorId)
                    .WhereIf(departmentId.HasValue, e => e.DepartmentId == departmentId)
                    .WhereIf(appointmentTypeId.HasValue, e => e.AppointmentTypeId== appointmentTypeId);



        protected virtual async Task<IQueryable<AppointmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync() =>
            from appointment in (await GetDbSetAsync())
            join patient in (await GetDbContextAsync()).Set<Patient>() on appointment.PatientId equals patient.Id into patients
            from patient in patients.DefaultIfEmpty()
            join doctor in (await GetDbContextAsync()).Set<Doctor>()
            .Include(d => d.User)
            .Include(d => d.Title)
            on appointment.DoctorId equals doctor.Id into doctors
            from doctor in doctors.DefaultIfEmpty()
            join department in (await GetDbContextAsync()).Set<Department>() on appointment.DepartmentId equals department.Id into departments
            from department in departments.DefaultIfEmpty()
            join appointmentType in (await GetDbContextAsync()).Set<AppointmentType>() on appointment.AppointmentTypeId equals appointmentType.Id into appointmentTypes
            from appointmentType in appointmentTypes.DefaultIfEmpty()
            select new AppointmentWithNavigationProperties
            {
                Appointment = appointment,
                Patient = patient,
                Doctor = doctor,
                Department = department,
                AppointmentType = appointmentType
            };


        protected virtual IQueryable<AppointmentWithNavigationProperties> ApplyFilter(
            IQueryable<AppointmentWithNavigationProperties> query,
            string? filterText = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? note = null,
            AppointmentStatus? appointmentStatus = null,
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Guid? appointmentTypeId = null) =>
                query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Patient.Id.ToString()!.Contains(filterText!) || e.Doctor.Id.ToString()!.Contains(filterText!) || e.Department.Id.ToString()!.Contains(filterText!) || e.AppointmentType.Id.ToString()!.Contains(filterText!))
                    .WhereIf(startDate.HasValue, e => e.Appointment.StartDate >= startDate!.Value)
                    .WhereIf(endDate.HasValue, e => e.Appointment.EndDate <= endDate!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Appointment.Note!.Contains(note!))
                    .WhereIf(appointmentStatus.HasValue, e => e.Appointment.AppointmentStatus == appointmentStatus)
                    .WhereIf(patientId.HasValue, e => e.Patient.Id == patientId)
                    .WhereIf(doctorId.HasValue, e => e.Doctor.Id == doctorId)
                    .WhereIf(departmentId.HasValue, e => e.Department.Id == departmentId)
                    .WhereIf(appointmentTypeId.HasValue, e => e.AppointmentType.Id == appointmentTypeId);
        #endregion
    }
}
