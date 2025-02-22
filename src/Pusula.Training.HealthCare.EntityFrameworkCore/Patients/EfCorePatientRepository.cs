using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Patients;

public class EfCorePatientRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider) 
    : EfCoreRepository<HealthCareDbContext, Patient, Guid>(dbContextProvider), IPatientRepository
{
    public virtual async Task DeleteAllAsync(
        string? filterText = null, 
        string? firstName = null, 
        string? lastName = null, 
        DateTime? birthDateMin = null, 
        DateTime? birthDateMax = null, 
        string? identityNumber = null, 
        string? passportNumber = null, 
        string? email = null, string? 
        mobilePhoneNumber = null, 
        string? emergencyPhoneNumber = null, 
        Gender? gender = null, 
        int? no = null, 
        string? motherName = null, 
        string? fatherName = null, 
        BloodType? bloodType = null, 
        Type? type = null, 
        Guid? companyId = null,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, firstName, lastName, birthDateMin, birthDateMax, identityNumber, passportNumber, email,
                             mobilePhoneNumber, emergencyPhoneNumber, gender, no, motherName, fatherName, bloodType, type, companyId
                              );
        var ids = query.Select(x => x.Patient.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<long> GetCountAsync(
        string? filterText = null, 
        string? firstName = null, 
        string? lastName = null, 
        DateTime? birthDateMin = null, 
        DateTime? birthDateMax = null, 
        string? identityNumber = null, 
        string? passportNumber = null, 
        string? email = null, 
        string? mobilePhoneNumber = null, 
        string? emergencyPhoneNumber = null, 
        Gender? gender = null, 
        int? no = null, 
        string? motherName = null, 
        string? fatherName = null, 
        BloodType? bloodType = null, 
        Type? type = null, 
        Guid? companyId = null,
        string? sorting = null, 
        int maxResultCount = int.MaxValue, 
        int skipCount = 0, 
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, firstName, lastName,birthDateMin,birthDateMax,identityNumber,passportNumber,email,mobilePhoneNumber,emergencyPhoneNumber,
            gender,no,motherName,fatherName,bloodType,type,companyId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<Patient>> GetListAsync(
        string? filterText = null, 
        string? firstName = null, 
        string? lastName = null, 
        DateTime? birthDateMin = null, 
        DateTime? birthDateMax = null, 
        string? identityNumber = null, 
        string? passportNumber = null, 
        string? email = null, 
        string? mobilePhoneNumber = null, 
        string? emergencyPhoneNumber = null, 
        Gender? gender = null, 
        int? no = null,
        string? motherName = null, 
        string? fatherName = null, 
        BloodType? bloodType = null, 
        Type? type = null, 
        Guid? companyId = null,
        string? sorting = null, 
        int maxResultCount = int.MaxValue, 
        int skipCount = 0, 
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter(await GetQueryableAsync(), filterText, firstName, lastName,birthDateMin,birthDateMax,identityNumber,passportNumber,email,mobilePhoneNumber,
            emergencyPhoneNumber,gender,no,motherName,fatherName,bloodType,type,companyId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PatientConsts.GetDefaultSorting(false) : sorting);
        return await query.Page(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<PatientWithNavigationProperties> GetWithNavigationPropertiesAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();

        return (await GetDbSetAsync()).Where(b => b.Id == id)
            .Select(patient => new PatientWithNavigationProperties
            {
                Patient = patient,
                PatientCompany = dbContext.Set<PatientCompany>().FirstOrDefault(c => c.Id == patient.CompanyId)!,
                Address = dbContext.Set<Address>().FirstOrDefault(c =>c.Id == patient.Id)!,
                
            })
            .FirstOrDefault()!;
    }

    public virtual async Task<List<PatientWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
        string? filterText = null, 
        string? firstName = null, 
        string? lastName = null, 
        DateTime? birthDateMin = null, 
        DateTime? birthDateMax = null, 
        string? identityNumber = null, 
        string? passportNumber = null, 
        string? email = null, 
        string? mobilePhoneNumber = null, 
        string? emergencyPhoneNumber = null, 
        Gender? gender = null, 
        int? no = null, 
        string? motherName = null, 
        string? fatherName = null, 
        BloodType? bloodType = null, 
        Type? type = null, 
        Guid? companyId = null,
        string? sorting = null, 
        int maxResultCount = int.MaxValue, 
        int skipCount = 0, 
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, firstName, lastName,birthDateMin,birthDateMax,identityNumber,passportNumber,email,mobilePhoneNumber,emergencyPhoneNumber,
            gender,no,motherName,fatherName,bloodType,type,companyId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PatientConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    #region ApplyFilter and Queryable
    protected virtual IQueryable<Patient> ApplyFilter(
        IQueryable<Patient> query,
        string? filterText = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? passportNumber = null,
        string? email = null,
        string? mobilePhoneNumber = null,
        string? emergencyPhoneNumber = null,
        Gender? gender = null,
        int? no = null,
        string? motherName = null,
        string? fatherName = null,
        BloodType? bloodType = null,
        Type? type = null,
        Guid? companyId = null
        ) =>
            query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
                e.No.ToString().Contains(filterText!) ||
                e.FirstName.Contains(filterText!) ||
                e.LastName.Contains(filterText!) ||
                e.MobilePhoneNumber.Contains(filterText!) ||
                e.IdentityNumber.Contains(filterText!) ||
                e.Email!.Contains(filterText!) ||
                e.PassportNumber!.Contains(filterText!))

                .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.FirstName.Contains(firstName!))
                .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.LastName.Contains(lastName!))
                .WhereIf(birthDateMin.HasValue, e => e.BirthDate >= birthDateMin!.Value)
                .WhereIf(birthDateMax.HasValue, e => e.BirthDate <= birthDateMax!.Value)
                .WhereIf(!string.IsNullOrWhiteSpace(mobilePhoneNumber), e => e.MobilePhoneNumber.Contains(mobilePhoneNumber!))
                .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.IdentityNumber.Contains(identityNumber!))
                .WhereIf(!string.IsNullOrWhiteSpace(passportNumber), e => e.PassportNumber!.Contains(passportNumber!))
                .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Email!.Contains(email!))
                .WhereIf(!string.IsNullOrWhiteSpace(emergencyPhoneNumber), e => e.EmergencyPhoneNumber!.Contains(emergencyPhoneNumber!))
                .WhereIf(no.HasValue, e => e.No.ToString().Contains(no!.Value.ToString()))
                .WhereIf(!string.IsNullOrWhiteSpace(motherName), e => e.MotherName!.Contains(motherName!))
                .WhereIf(!string.IsNullOrWhiteSpace(fatherName), e => e.FatherName!.Contains(fatherName!))
                .WhereIf(gender.HasValue, e => e.Gender == gender)
                .WhereIf(bloodType.HasValue, e => e.BloodType == bloodType)
                .WhereIf(companyId.HasValue, e => e.CompanyId == companyId);


    
    //Company ve Country tablolar� ile join i�lemi yap�l�r. Entityler yok �u an �nemli
    protected virtual async Task<IQueryable<PatientWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        var dbContext = await GetDbContextAsync();
        var dbSet = await GetDbSetAsync();

        return from patient in dbSet
               join company in dbContext.Set<PatientCompany>()
                    on patient.CompanyId equals company.Id
                    into companies
               from company in companies.DefaultIfEmpty()
               select new PatientWithNavigationProperties
               {
                   Patient = patient,
                   PatientCompany = company,
                   Addresses = dbContext.Set<Address>()
                       .Include(addr => addr.Country)
                       .Include(addr => addr.City)
                       .Include(addr => addr.District)
                       .Include(addr => addr.Village)
                       .Where(addr => addr.PatientId == patient.Id)
                       .ToList()
               };
    }


    protected virtual IQueryable<PatientWithNavigationProperties> ApplyFilter(
            IQueryable<PatientWithNavigationProperties> query,
            string? filterText = null,
            string? firstName = null,
            string? lastName = null,
            DateTime? birthDateMin = null,
            DateTime? birthDateMax = null,
            string? identityNumber = null,
            string? passportNumber = null,
            string? email = null,
            string? mobilePhoneNumber = null,
            string? emergencyPhoneNumber = null,
            Gender? gender = null,
            int? no = null,
            string? motherName = null,
            string? fatherName = null,
            BloodType? bloodType = null,
            Type? type = null,
            Guid? companyId = null
            ) =>

                query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
                    e.Patient!.No.ToString().Contains(filterText!) ||
                    e.Patient.FirstName.Contains(filterText!) ||
                    e.Patient.LastName.Contains(filterText!) ||
                    e.Patient.MobilePhoneNumber.Contains(filterText!) ||
                    e.Patient.IdentityNumber.Contains(filterText!) ||
                    e.Patient.Email!.Contains(filterText!) ||
                    e.Patient.PassportNumber!.Contains(filterText!))

                    .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.Patient!.FirstName.Contains(firstName!))
                    .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.Patient!.LastName.Contains(lastName!))
                    .WhereIf(birthDateMin.HasValue, e => e.Patient!.BirthDate >= birthDateMin!.Value)
                    .WhereIf(birthDateMax.HasValue, e => e.Patient!.BirthDate <= birthDateMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(mobilePhoneNumber), e => e.Patient!.MobilePhoneNumber.Contains(mobilePhoneNumber!))
                    .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.Patient!.IdentityNumber.Contains(identityNumber!))
                    .WhereIf(!string.IsNullOrWhiteSpace(passportNumber), e => e.Patient!.PassportNumber!.Contains(passportNumber!))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Patient!.Email!.Contains(email!))
                    .WhereIf(!string.IsNullOrWhiteSpace(emergencyPhoneNumber), e => e.Patient!.EmergencyPhoneNumber!.Contains(emergencyPhoneNumber!))
                    .WhereIf(no.HasValue, e => e.Patient!.No.ToString().Contains(no!.Value.ToString()))
                    .WhereIf(!string.IsNullOrWhiteSpace(motherName), e => e.Patient!.MotherName!.Contains(motherName!))
                    .WhereIf(!string.IsNullOrWhiteSpace(fatherName), e => e.Patient!.FatherName!.Contains(fatherName!))
                    .WhereIf(gender.HasValue, e => e.Patient!.Gender == gender)
                    .WhereIf(bloodType.HasValue, e => e.Patient!.BloodType == bloodType)
                    .WhereIf(companyId.HasValue, e => e.Patient!.CompanyId == companyId);
    #endregion
}