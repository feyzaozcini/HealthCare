using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
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
        var query = ApplyFilter((await GetDbSetAsync()), filterText, labRequestId, testGroupItemId, status, result, resultDate);
        return await query.LongCountAsync(cancellationToken);
    }


    public virtual async Task<List<TestProcess>> GetListAsync(
                string? filterText = null,
            Guid? labRequestId = null,
            Guid? testGroupItemId = null,
            TestProcessStates? status = null,
            decimal? result = null,
            DateTime? resultDate = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
    CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, labRequestId, testGroupItemId, status, result, resultDate);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestProcessConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }


    //Seçilen ID'ye göre bağlı olduğu entity'lerin verisini getirir.
    public async Task<TestProcessWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        var testProcess = await query
            .FirstOrDefaultAsync(tp => tp.TestProcess != null && tp.TestProcess.Id == id, cancellationToken);
        return testProcess!;
    }

    public virtual async Task<List<TestProcessWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
        string? filterText = null,
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
        query = ApplyFilterWithNavigation(query, filterText, status, result, resultDate, doctorName, doctorSurname, patientName, patientSurname, protocolNo);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestProcessConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }



    // LabRequestId'ye göre TestProcessWithNavigationProperties listeleme.
    public virtual async Task<List<TestProcessWithNavigationProperties>> GetByLabRequestIdAsync(
        Guid labRequestId,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();

        query = query.Where(tp => tp.TestProcess!.LabRequestId == labRequestId);

        return await query.ToListAsync(cancellationToken);
    }


    protected virtual async Task<IQueryable<TestProcessWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        var dbSet = await GetDbSetAsync();

        return dbSet
            .AsNoTracking() //Change Tracker devre dışı
            .AsSplitQuery() //Include'ları birden fazla sorguya böler
            .Include(tp => tp.LabRequest)
            .ThenInclude(tp => tp.Protocol.Patient)
            .Include(tp => tp.LabRequest.Doctor)
                .ThenInclude(d => d.User)
            .Include(tp => tp.LabRequest.Protocol)
            .Include(tp => tp.TestGroupItem)
                .ThenInclude(tgi => tgi.TestValueRange)
                            .Include(tp => tp.TestGroupItem.TestGroup)
            .Select(tp => new TestProcessWithNavigationProperties
            {
                TestProcess = tp,
                LabRequest = tp.LabRequest,
                TestGroupItem = tp.TestGroupItem,
                TestValueRange = tp.TestGroupItem!.TestValueRange
            });
    }

    protected virtual IQueryable<TestProcess> ApplyFilter(
        IQueryable<TestProcess> query,
        string? filterText = null,
        Guid? labRequestId = null,
        Guid? testGroupItemId = null,
        TestProcessStates? status = null,
        decimal? result = null,
        DateTime? resultDate = null
    )
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
                e.Id.ToString().Contains(filterText!))
            .WhereIf(labRequestId.HasValue, e => e.LabRequestId == labRequestId)
            .WhereIf(testGroupItemId.HasValue, e => e.TestGroupItemId == testGroupItemId)
            .WhereIf(status.HasValue, e => e.Status == status)
            .WhereIf(result.HasValue, e => e.Result == result)
            .WhereIf(resultDate.HasValue, e => e.ResultDate == resultDate);
    }

    protected virtual IQueryable<TestProcessWithNavigationProperties> ApplyFilterWithNavigation(
    IQueryable<TestProcessWithNavigationProperties> query,
    string? filterText = null,
    TestProcessStates? status = null,
    decimal? result = null,
    DateTime? resultDate = null,
    string? doctorName = null,
    string? doctorSurname = null,
    string? patientName = null,
    string? patientSurname = null,
    int? protocolNo = null)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
                e.LabRequest!.Doctor.User.Name!.Contains(filterText!) ||
                e.LabRequest.Doctor.User.Surname!.Contains(filterText!) ||
                e.LabRequest.Protocol.Patient.FirstName!.Contains(filterText!) ||
                e.LabRequest.Protocol.Patient.LastName!.Contains(filterText!))
            .WhereIf(status.HasValue, e => e.TestProcess!.Status == status)
            .WhereIf(result.HasValue, e => e.TestProcess!.Result == result)
            .WhereIf(resultDate.HasValue, e => e.TestProcess!.ResultDate == resultDate)
            .WhereIf(!string.IsNullOrWhiteSpace(doctorName), e => e.LabRequest!.Doctor.User.Name.Contains(doctorName!))
            .WhereIf(!string.IsNullOrWhiteSpace(doctorSurname), e => e.LabRequest!.Doctor.User.Surname.Contains(doctorSurname!))
            .WhereIf(!string.IsNullOrWhiteSpace(patientName), e => e.LabRequest!.Protocol.Patient.FirstName.Contains(patientName!))
            .WhereIf(!string.IsNullOrWhiteSpace(patientSurname), e => e.LabRequest!.Protocol.Patient.LastName.Contains(patientSurname!))
            .WhereIf(protocolNo.HasValue, e => e.LabRequest!.Protocol.No == protocolNo);
    }
}
