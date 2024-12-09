using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.TestValueRanges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.TestProcesses;

public class EfCoreTestProcessRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
: EfCoreRepository<HealthCareDbContext, TestProcess, Guid>(dbContextProvider), ITestProcessRepository
{
    public virtual async Task<long> GetCountAsync(
        string? filterText = null,
        Guid? labRequestId = null,
        Guid? testGroupItemId = null,
        TestProcessStates? status = null,
        decimal? result = null,
        DateTime? resultDate = null,
        string? doctorName = null,
        string? doctorSurname = null,
        string? patientName = null,
        string? patientSurname = null,
        int? protocolNo = null,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, labRequestId, testGroupItemId, status, result, resultDate, doctorName, doctorSurname, patientName, patientSurname, protocolNo);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<TestProcess>> GetListAsync(
        string? filterText = null,
        Guid? labRequestId = null,
        Guid? testGroupItemId = null,
        TestProcessStates? status = null,
        decimal? result = null,
        DateTime? resultDate = null,
        string? doctorName = null,
        string? doctorSurname = null,
        string? patientName = null,
        string? patientSurname = null,
        int? protocolNo = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, labRequestId, testGroupItemId, status, result, resultDate, doctorName, doctorSurname, patientName, patientSurname, protocolNo);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestProcessConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    //Seçilen ID'ye göre bağlı olduğu entity'lerin verisini getirir.
    public async Task<TestProcess> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        var testProcess = await query.FirstOrDefaultAsync(tp => tp.Id == id, cancellationToken);
        HealthCareException.ThrowIf(testProcess == null);
        return testProcess!;
    }

    //Bağlı olduğu entity'ler ile beraber tüm verileri getirir.
    public async Task<List<TestProcess>> GetListWithNavigationPropertiesAsync(
        string? filterText = null,
        Guid? labRequestId = null,
        Guid? testGroupItemId = null,
        TestProcessStates? status = null,
        decimal? result = null,
        DateTime? resultDate = null,
        string? doctorName = null,
        string? doctorSurname = null,
        string? patientName = null,
        string? patientSurname = null,
        int? protocolNo = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, labRequestId, testGroupItemId, status, result, resultDate, doctorName, doctorSurname, patientName, patientSurname, protocolNo);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestProcessConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    //LabRequestId'ye göre TestProcess listeleme.
    public virtual async Task<List<TestProcess>> GetByLabRequestIdAsync(Guid labRequestId)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, labRequestId: labRequestId);
        return await query.ToListAsync();
    }


    protected virtual async Task<IQueryable<TestProcess>> GetQueryForNavigationPropertiesAsync()
    {
        var dbSet = await GetDbSetAsync();
        return dbSet
            .Include(tp => tp.LabRequest)
                .ThenInclude(lr => lr.Protocol)
                    .ThenInclude(p => p.Patient)
            .Include(tp => tp.LabRequest.Doctor)
                .ThenInclude(d => d.User)
            .Include(tp => tp.TestGroupItem)
                .ThenInclude(tgi => tgi.TestGroup)
            .Include(tp => tp.TestGroupItem.TestValueRange);
    }




    protected virtual IQueryable<TestProcess> ApplyFilter(
        IQueryable<TestProcess> query,
        string? filterText = null,
        Guid? labRequestId = null,
        Guid? testGroupItemId = null,
        TestProcessStates? status = null,
        decimal? result = null,
        DateTime? resultDate = null,
        string? doctorName = null,
        string? doctorSurname = null,
        string? patientName = null,
        string? patientSurname = null,
        int? protocolNo = null
    )
    {
        return query
           .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
               (e.LabRequest.Doctor.User.Name!.Contains(filterText!)) ||
               (e.LabRequest.Doctor.User.Surname!.Contains(filterText!)) ||
               (e.LabRequest.Protocol.Patient.FirstName!.Contains(filterText!)) ||
               (e.LabRequest.Protocol.Patient.LastName!.Contains(filterText!)) ||
               (e.LabRequest.Protocol.No!.ToString().Contains(filterText!))

           )
          .WhereIf(labRequestId.HasValue && labRequestId != Guid.Empty, e => e.LabRequestId == labRequestId!.Value)
            .WhereIf(testGroupItemId.HasValue && testGroupItemId != Guid.Empty, e => e.TestGroupItemId == testGroupItemId!.Value)
            .WhereIf(status.HasValue, e => e.Status == status!.Value)
            .WhereIf(result.HasValue, e => e.Result == result!.Value)
            .WhereIf(resultDate.HasValue, e => e.ResultDate == resultDate!.Value)
            .WhereIf(!string.IsNullOrWhiteSpace(doctorName), e => e.LabRequest.Doctor != null && e.LabRequest.Doctor.User.Name != null && e.LabRequest.Doctor.User.Name.Contains(doctorName!))
            .WhereIf(!string.IsNullOrWhiteSpace(doctorSurname), e => e.LabRequest.Doctor != null && e.LabRequest.Doctor.User.Surname != null && e.LabRequest.Doctor.User.Surname.Contains(doctorSurname!))
            .WhereIf(!string.IsNullOrWhiteSpace(patientName), e => e.LabRequest.Protocol != null && e.LabRequest.Protocol.Patient != null && e.LabRequest.Protocol.Patient.FirstName != null && e.LabRequest.Protocol.Patient.FirstName.Contains(patientName!))
            .WhereIf(!string.IsNullOrWhiteSpace(patientSurname), e => e.LabRequest.Protocol != null && e.LabRequest.Protocol.Patient != null && e.LabRequest.Protocol.Patient.LastName != null && e.LabRequest.Protocol.Patient.LastName.Contains(patientSurname!))
            .WhereIf(protocolNo.HasValue, e => e.LabRequest.Protocol.No == protocolNo!.Value);
    }


}
