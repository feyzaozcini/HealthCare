using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System.ComponentModel.Design;
using Polly;
using Pusula.Training.HealthCare.DoctorDepartments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Titles;
using Volo.Abp.Identity;
using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.Appointments;

namespace Pusula.Training.HealthCare.Protocols;

public class EfCoreProtocolRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider) 
    : EfCoreRepository<HealthCareDbContext, Protocol, Guid>(dbContextProvider), IProtocolRepository
{
    public virtual async Task DeleteAllAsync(
        string? filterText = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        ProtocolStatus? protocolStatus = null,
        Guid? protocolTypeId = null,
        Guid? protocolNoteId = null,
        Guid? protocolInsuranceId = null,
        Guid? patientId = null,
        Guid? departmentId = null,
        Guid? doctorId = null,
        int? no = null,
        CancellationToken cancellationToken = default)
    {
        //var query = await GetQueryForNavigationPropertiesAsync();

        var query = await GetQueryableAsync();


        query = ApplyFilter(query, filterText, startTime, endTime, protocolStatus, protocolTypeId, protocolNoteId, protocolInsuranceId, patientId, departmentId, doctorId, no);

        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<long> GetCountAsync(
       string? filterText = null,
       DateTime? startTime = null,
       DateTime? endTime = null,
       ProtocolStatus? protocolStatus = null,
       Guid? protocolTypeId = null,
       Guid? protocolNoteId = null,
       Guid? protocolInsuranceId = null,
       Guid? patientId = null,
       Guid? departmentId = null,
       Guid? doctorId = null,
       int? no = null,
       CancellationToken cancellationToken = default)
    {
        //var query = await GetQueryForNavigationPropertiesAsync();

        var query = ApplyFilter(await GetDbSetAsync(), filterText, startTime, endTime, protocolStatus, protocolTypeId, protocolNoteId, protocolInsuranceId, patientId, departmentId, doctorId,no);

        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<Protocol>> GetListAsync(
       string? filterText = null,
       DateTime? startTime = null,
       DateTime? endTime = null,
       ProtocolStatus? protocolStatus = null,
       Guid? protocolTypeId = null,
       Guid? protocolNoteId = null,
       Guid? protocolInsuranceId = null,
       Guid? patientId = null,
       Guid? departmentId = null,
       Guid? doctorId = null,
       int? no = null,
       string? sorting = null,
       int maxResultCount = int.MaxValue,
       int skipCount = 0,
       CancellationToken cancellationToken = default)
    {
        
        var query = ApplyFilter(await GetQueryForNavigationPropertiesAsync(),filterText, startTime, endTime, protocolStatus, protocolTypeId, protocolNoteId, protocolInsuranceId, patientId, departmentId, doctorId, no);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProtocolConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<List<Protocol>> GetListWithNavigationPropertiesAsync(
       string? filterText = null,
       DateTime? startTime = null,
       DateTime? endTime = null,
       ProtocolStatus? protocolStatus = null,
       Guid? protocolTypeId = null,
       Guid? protocolNoteId = null,
       Guid? protocolInsuranceId = null,
       Guid? patientId = null,
       Guid? departmentId = null,
       Guid? doctorId = null,
       int? no = null,
       string? sorting = null,
       int maxResultCount = int.MaxValue,
       int skipCount = 0,
       CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, startTime, endTime, protocolStatus, protocolTypeId, protocolNoteId, protocolInsuranceId, patientId, departmentId, doctorId, no);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProtocolConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<Protocol> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        var protocol = await query.FirstOrDefaultAsync(pr => pr.Id == id, cancellationToken);
        return protocol!;
    }

    protected virtual async Task<IQueryable<Protocol>> GetQueryForNavigationPropertiesAsync()
    {

        var dbSet = await GetDbSetAsync();
        return dbSet
            .Include(pr => pr.ProtocolType)
            .Include(pr => pr.Note)
            .Include(pr => pr.Insurance)
            .Include(pr => pr.Patient)
            .Include(pr => pr.Department)
            .Include(pr => pr.Doctor)
                .ThenInclude(pr => pr.User)
            .Include(pr => pr.Doctor)
                .ThenInclude(pr => pr.Title);
    }

    protected virtual IQueryable<Protocol> ApplyFilter(
       IQueryable<Protocol> query,
       string? filterText = null,
       DateTime? startTime = null,
       DateTime? endTime = null,
       ProtocolStatus? protocolStatus = null,
       Guid? protocolTypeId = null,
       Guid? protocolNoteId = null,
       Guid? protocolInsuranceId = null,
       Guid? patientId = null,
       Guid? departmentId = null,
       Guid? doctorId = null,
       int? no = null)
    {
        return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
                e.No.ToString().Contains(filterText!) ||
                e.Patient.FirstName!.Contains(filterText!) ||
                e.Patient.LastName!.Contains(filterText!) ||
                e.Patient.No!.ToString().Contains(filterText!) ||
                e.ProtocolType.Name.Contains(filterText!) ||
                e.Note!.Text!.Contains(filterText!) ||
                e.Insurance.Name.Contains(filterText!) ||
                e.Department.Name.Contains(filterText!) ||
                e.Doctor.User.Name.Contains(filterText!) ||
                e.Doctor.User.Surname.Contains(filterText!) ||
                e.Doctor.Title.Name.Contains(filterText!))

                .WhereIf(startTime.HasValue, e => e.StartTime >= startTime!.Value)
                .WhereIf(endTime.HasValue, e => e.EndTime <= endTime!.Value)
                .WhereIf(no.HasValue, e => e.No.ToString().Contains(no!.Value.ToString()))
                .WhereIf(protocolStatus.HasValue, e => e.ProtocolStatus == protocolStatus!.Value)
                .WhereIf(protocolTypeId != null && protocolTypeId != Guid.Empty, e => e.ProtocolTypeId == protocolTypeId)
                .WhereIf(protocolNoteId != null && protocolNoteId != Guid.Empty, e => e.ProtocolNoteId == protocolNoteId)
                .WhereIf(protocolInsuranceId != null && protocolInsuranceId != Guid.Empty, e => e.ProtocolInsuranceId == protocolInsuranceId)
                .WhereIf(patientId != null && patientId != Guid.Empty, e => e.Patient.Id == patientId)
                .WhereIf(departmentId != null && departmentId != Guid.Empty, e => e.Department.Id == departmentId)
                .WhereIf(doctorId != null && doctorId != Guid.Empty, e => e.Doctor.Id == doctorId);
                
    }
}