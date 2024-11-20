using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.PatientCompanies;
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
        Guid? countryId = null, 
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, firstName, lastName,birthDateMin,birthDateMax,identityNumber,passportNumber,email,mobilePhoneNumber,emergencyPhoneNumber,gender,no,motherName,fatherName,bloodType,type,companyId,countryId);

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
        Guid? countryId = null, 
        string? sorting = null, 
        int maxResultCount = int.MaxValue, 
        int skipCount = 0, 
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, firstName, lastName,birthDateMin,birthDateMax,identityNumber,passportNumber,email,mobilePhoneNumber,emergencyPhoneNumber,gender,no,motherName,fatherName,bloodType,type,companyId,countryId);
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
        Guid? countryId = null, 
        string? sorting = null, 
        int maxResultCount = int.MaxValue, 
        int skipCount = 0, 
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter(await GetQueryableAsync(), filterText, firstName, lastName,birthDateMin,birthDateMax,identityNumber,passportNumber,email,mobilePhoneNumber,emergencyPhoneNumber,gender,no,motherName,fatherName,bloodType,type,companyId,countryId);
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
                Country = dbContext.Set<Country>().FirstOrDefault(c => c.Id == patient.CountryId)!
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
        Guid? countryId = null, 
        string? sorting = null, 
        int maxResultCount = int.MaxValue, 
        int skipCount = 0, 
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, firstName, lastName,birthDateMin,birthDateMax,identityNumber,passportNumber,email,mobilePhoneNumber,emergencyPhoneNumber,gender,no,motherName,fatherName,bloodType,type,companyId,countryId);
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
        Guid? companyId = null,
        Guid? countryId = null) =>
            query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.No.ToString().StartsWith(filterText!) || e.FirstName.StartsWith(filterText!) || e.LastName.StartsWith(filterText!) || e.MobilePhoneNumber.StartsWith(filterText!) || e.IdentityNumber.StartsWith(filterText!) || e.Email.StartsWith(filterText!) || e.PassportNumber.StartsWith(filterText!))
                .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.FirstName.StartsWith(firstName!))
                .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.LastName.StartsWith(lastName!))
                .WhereIf(birthDateMin.HasValue, e => e.BirthDate >= birthDateMin!.Value)
                .WhereIf(birthDateMax.HasValue, e => e.BirthDate <= birthDateMax!.Value)
                .WhereIf(!string.IsNullOrWhiteSpace(mobilePhoneNumber), e => e.MobilePhoneNumber.StartsWith(mobilePhoneNumber!))
                .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.IdentityNumber.StartsWith(identityNumber!))
                .WhereIf(!string.IsNullOrWhiteSpace(passportNumber), e => e.PassportNumber.StartsWith(passportNumber!))
                .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Email.StartsWith(email!))
                .WhereIf(!string.IsNullOrWhiteSpace(emergencyPhoneNumber), e => e.EmergencyPhoneNumber.StartsWith(emergencyPhoneNumber!))
                .WhereIf(no.HasValue, e => e.No.ToString().StartsWith(no!.Value.ToString()))
                .WhereIf(!string.IsNullOrWhiteSpace(motherName), e => e.MotherName.StartsWith(motherName!))
                .WhereIf(!string.IsNullOrWhiteSpace(fatherName), e => e.FatherName.StartsWith(fatherName!))
                .WhereIf(gender.HasValue, e => e.Gender == gender)
                .WhereIf(bloodType.HasValue, e => e.BloodType == bloodType)
                .WhereIf(companyId.HasValue, e => e.CompanyId == companyId)
                .WhereIf(countryId.HasValue, e => e.CountryId == countryId);


    //Company ve Country tablolarý ile join iþlemi yapýlýr. Entityler yok þu an önemli
    protected virtual async Task<IQueryable<PatientWithNavigationProperties>> GetQueryForNavigationPropertiesAsync() =>
        from patient in (await GetDbSetAsync())
        join company in (await GetDbContextAsync()).Set<PatientCompany>() on patient.CompanyId equals company.Id into companies
        from company in companies.DefaultIfEmpty()
        join country in (await GetDbContextAsync()).Set<Country>() on patient.CountryId equals country.Id into countries
        from country in countries.DefaultIfEmpty()
        select new PatientWithNavigationProperties
        {
            Patient = patient,
            PatientCompany = company,
            Country= country
        };


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
        Guid? companyId = null,
        Guid? countryId = null
        ) =>
            query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Patient.No.ToString().StartsWith(filterText!) || e.Patient.FirstName.StartsWith(filterText!) || e.Patient.LastName.StartsWith(filterText!) || e.Patient.MobilePhoneNumber.StartsWith(filterText!) || e.Patient.IdentityNumber.StartsWith(filterText!) || e.Patient.Email.StartsWith(filterText!) || e.Patient.PassportNumber.StartsWith(filterText!))
                .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.Patient.FirstName.StartsWith(firstName!))
                .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.Patient.LastName.StartsWith(lastName!))
                .WhereIf(birthDateMin.HasValue, e => e.Patient.BirthDate >= birthDateMin!.Value)
                .WhereIf(birthDateMax.HasValue, e => e.Patient.BirthDate <= birthDateMax!.Value)
                .WhereIf(!string.IsNullOrWhiteSpace(mobilePhoneNumber), e => e.Patient.MobilePhoneNumber.StartsWith(mobilePhoneNumber!))
                .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.Patient.IdentityNumber.StartsWith(identityNumber!))
                .WhereIf(!string.IsNullOrWhiteSpace(passportNumber), e => e.Patient.PassportNumber.StartsWith(passportNumber!))
                .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Patient.Email.StartsWith(email!))
                .WhereIf(!string.IsNullOrWhiteSpace(emergencyPhoneNumber), e => e.Patient.EmergencyPhoneNumber.StartsWith(emergencyPhoneNumber!))
                .WhereIf(no.HasValue, e => e.Patient.No.ToString().StartsWith(no!.Value.ToString()))
                .WhereIf(!string.IsNullOrWhiteSpace(motherName), e => e.Patient.MotherName.StartsWith(motherName!))
                .WhereIf(!string.IsNullOrWhiteSpace(fatherName), e => e.Patient.FatherName.StartsWith(fatherName!))
                .WhereIf(gender.HasValue, e => e.Patient.Gender == gender)
                .WhereIf(bloodType.HasValue, e => e.Patient.BloodType == bloodType)
                .WhereIf(companyId.HasValue, e => e.Patient.CompanyId == companyId)
                .WhereIf(countryId.HasValue, e => e.Patient.CountryId == countryId);

  
    #endregion
}