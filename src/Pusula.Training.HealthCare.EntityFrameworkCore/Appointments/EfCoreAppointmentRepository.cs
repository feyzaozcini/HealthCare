using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
            bool isBlock = false,
            Guid? patientId = null, 
            Guid? doctorId = null, 
            Guid? departmentId = null, 
            Guid? appointmentTypeId = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, startDate, endDate, note, appointmentStatus,isBlock,patientId, doctorId, departmentId, appointmentTypeId);

            var ids = query.Select(x => x.Appointment.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));

        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string? note = null, 
            AppointmentStatus? appointmentStatus = null, 
            bool? isBlock = null,
            Guid? patientId = null, 
            Guid? doctorId = null, 
            Guid? departmentId = null, 
            Guid? appointmentTypeId = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, startDate, endDate, note, appointmentStatus,isBlock,patientId, doctorId, departmentId, appointmentTypeId);

            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<Appointment>> GetListAsync(
            string? filterText = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string? note = null, 
            AppointmentStatus? appointmentStatus = null,
            bool? isBlock = null,
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Guid? appointmentTypeId = null,
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query=ApplyFilter(await GetQueryableAsync(), filterText, startDate, endDate, note, appointmentStatus, isBlock,patientId, doctorId, departmentId, appointmentTypeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentConst.GetDefaultSorting(false) : sorting);
            return await query.Page(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<List<AppointmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string? note = null, 
            AppointmentStatus? appointmentStatus = null,
            bool? isBlock = null,
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
            query = ApplyFilter(query, filterText, startDate, endDate, note, appointmentStatus, isBlock,patientId, doctorId, departmentId, appointmentTypeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentConst.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<AppointmentWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            var appointment = await query.FirstOrDefaultAsync(lr => lr.Appointment.Id == id, cancellationToken);
            HealthCareException.ThrowIf(appointment == null);
            return appointment!;
        }


        #region ApplyFilter and Queryable
        protected virtual IQueryable<Appointment> ApplyFilter(
            IQueryable<Appointment> query,
            string? filterText = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? note = null,
            AppointmentStatus? appointmentStatus = null,
            bool? isBlock = null,
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



        protected virtual async Task<IQueryable<AppointmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            var dbContext = await GetDbContextAsync();
            var blackList = await GetDbSetAsync();
            var query = blackList
                .Include(ar => ar.Patient)
                .Include(ar => ar.Doctor)
                    .ThenInclude(d => d.User)
                .Include(ar => ar.Doctor)
                    .ThenInclude(d => d.Title)
                .Include(ar => ar.Department)
                .Include(ar => ar.AppointmentType)
                .Select(ar => new AppointmentWithNavigationProperties
                {
                    Appointment = ar,
                    Patient = ar.Patient,
                    Doctor = ar.Doctor,
                    Department = ar.Department,
                    AppointmentType = ar.AppointmentType
                });
            return query;
        }
           
        protected virtual IQueryable<AppointmentWithNavigationProperties> ApplyFilter(
            IQueryable<AppointmentWithNavigationProperties> query,
            string? filterText = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? note = null,
            AppointmentStatus? appointmentStatus = null,
            bool? isBlock = null,
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
                    

