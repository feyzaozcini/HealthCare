using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MiniExcelLibs;
using Polly;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.DoctorDepartments;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.Titles;
using Pusula.Training.HealthCare.UserProfiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;

namespace Pusula.Training.HealthCare.Doctors
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Doctors.Default)]
    public class DoctorsAppService(IDoctorRepository doctorRepository, DoctorManager doctorManager,
        UserProfileManager userProfileManager,
        UserManager<IdentityUser> userManager,
        IDistributedCache<DoctorDownloadTokenCacheItem, string> downloadTokenCache,
        ITitleRepository titleRepository,
        IDepartmentRepository departmentRepository
        ) : HealthCareAppService, IDoctorsAppService
    {

        public virtual async Task<DoctorWithDepartmentDto> GetWithDepartmentsAsync(Guid id)
        {
            var doctor = await doctorRepository.GetWithDepartmentsAsync(id);
            return ObjectMapper.Map<Doctor, DoctorWithDepartmentDto>(doctor);
        }

        [Authorize(HealthCarePermissions.Doctors.Create)]
        public async Task<DoctorDto> CreateAsync(DoctorCreateDto input)
        {
            var user = await userProfileManager.CreateUserWithPropertiesAsync(input.UserName, input.Name, input.SurName, input.Email, input.Password, input.Role, input.PhoneNumber, true);

            var doctor = await doctorManager.CreateAsync(user.Id,input.TitleId,input.IdentityNumber, input.BirthDate, input.Gender);

            // DoctorDepartment ilişkisi kuruluyor
            foreach (var departmentId in input.DepartmentIds)
            {
                var doctorDepartment = new DoctorDepartment
                {
                    DoctorId = doctor.Id,
                    DepartmentId = departmentId
                };

                doctor.DoctorDepartments.Add(doctorDepartment);
            }

            var doctorDto = ObjectMapper.Map<Doctor, DoctorDto>(doctor);
            ObjectMapper.Map(user, doctorDto);

            return doctorDto;

        }

        [Authorize(HealthCarePermissions.Doctors.Delete)]
        public async Task DeleteAllAsync(GetDoctorsInput input) => await doctorRepository.DeleteAllAsync(input.FilterText, input.UserId, input.TitleId, input.IdentityNumber, input.BirthDateMax, input.BirthDateMax, input.Gender);


        [Authorize(HealthCarePermissions.Doctors.Delete)]
        public async Task DeleteAsync(Guid id) => await doctorRepository.DeleteAsync(id);


        [Authorize(HealthCarePermissions.Doctors.Delete)]
        public async Task DeleteByIdsAsync(List<Guid> doctorIds) => await doctorRepository.DeleteManyAsync(doctorIds);



        public async Task<DoctorDto> GetAsync(Guid id)
        {
            // Doktorun navigasyon verileriyle birlikte bilgisini al
            var doctorWithNavProps = await doctorRepository.GetWithNavigationProperties(id);

            if (doctorWithNavProps == null)
            {
                throw new EntityNotFoundException($"Doctor with id '{id}' not found.");
            }

            var doctorDto = ObjectMapper.Map<DoctorWithNavigationProperties, DoctorDto>(doctorWithNavProps);

            
doctorDto.DoctorDepartments = doctorWithNavProps.DoctorDepartments
    .Select(dd => dd.DepartmentId)
    .ToList();

            return doctorDto;
        }



    public async Task<DoctorDto> GetDoctorWithUserIdAsync(Guid userId)
        {
            // İlk olarak UserId ile tek bir Doctor kaydı bul
            var doctor = await doctorRepository.GetAsync(d => d.UserId == userId);

            if (doctor == null) // Eğer Doctor bulunamazsa hata fırlat
            {
                throw new EntityNotFoundException($"Doctor not found for UserId '{userId}'.");
            }

            // DoctorId'yi kullanarak navigasyon verilerini getir
            var doctorWithNavProps = await doctorRepository.GetWithNavigationProperties(doctor.Id);

            if (doctorWithNavProps == null) // Eğer navigasyon verileri bulunamazsa hata fırlat
            {
                throw new EntityNotFoundException($"Doctor with id '{doctor.Id}' not found.");
            }

            // DoctorWithNavigationProperties'i DoctorDto'ya mapliyoruz
            var doctorDto = ObjectMapper.Map<DoctorWithNavigationProperties, DoctorDto>(doctorWithNavProps);

            // DoctorDepartments'ı da mapliyoruz
            doctorDto.DoctorDepartments = doctorWithNavProps.DoctorDepartments
                .Select(dd => dd.DepartmentId)
                .ToList();

            return doctorDto;
        }


        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new DoctorDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new Shared.DownloadTokenResultDto
            {
                Token = token
            };
        }

        [AllowAnonymous]

        public async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DoctorExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var doctors = await doctorRepository.GetListWithNavigationPropertiesAsync(input.FilterText,input.UserId,input.TitleId,input.IdentityNumber,input.BirthDateMin,input.BirthDateMax,input.Gender);
            var items = doctors.Select(item => new DoctorExcelDto
            {
                IdentityNumber = item.Doctor.IdentityNumber,
                BirthDate = item.Doctor.BirthDate,
                Gender = item.Doctor.Gender,
                Title = item.Title?.Name ?? string.Empty
            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Doctors.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<PagedResultDto<DoctorWithNavigationPropertiesDto>> GetListAsync(GetDoctorsInput input)
        {
            var totalCount = await doctorRepository.GetCountAsync(input.FilterText, input.UserId, input.TitleId, input.IdentityNumber, input.BirthDateMax, input.BirthDateMax, input.Gender);

            var items = await doctorRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.TitleId, input.IdentityNumber, input.BirthDateMin, input.BirthDateMax, input.Gender, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DoctorWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DoctorWithNavigationProperties>, List<DoctorWithNavigationPropertiesDto>>(items)
            };
        }


        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetTitleLookupAsync(LookupRequestDto input)
        {
            var query = (await titleRepository.GetQueryableAsync())
                           .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                               x => x.Name != null && x.Name.Contains(input.Filter!));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Title>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Title>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
        {
            var query = (await departmentRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null && x.Name.Contains(input.Filter!));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Department>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Department>, List<LookupDto<Guid>>>(lookupData)
            };
        }

       /* public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input)
        {
            
        }*/

        public virtual async Task<DoctorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var doctor = await doctorRepository.GetWithNavigationProperties(id);
            
            return ObjectMapper.Map<DoctorWithNavigationProperties, DoctorWithNavigationPropertiesDto>(doctor);
        }

        public async Task<DoctorDto> UpdateAsync(DoctorUpdateDto input)
        {
            // Doctor kaydını alıyoruz
            var doctor = await doctorRepository.GetAsync(input.Id);
            if (doctor == null)
            {
                throw new EntityNotFoundException($"Doctor with id '{input.Id}' not found.");
            }

            // Kullanıcı (IdentityUser) bilgilerini alıyoruz
            var user = await userManager.FindByIdAsync(doctor.UserId.ToString());
            if (user == null)
            {
                throw new EntityNotFoundException($"User not found for Doctor with id '{input.Id}'.");
            }

            // Doctor bilgilerini güncelliyoruz
            doctor.TitleId = input.TitleId;
            doctor.SetIdentityNumber(input.IdentityNumber);
            doctor.SetBirthDate(input.BirthDate);
            doctor.SetGender(input.Gender);

            // Kullanıcı bilgilerini güncelliyoruz
            user.Name = input.Name;
            user.Surname = input.SurName;
            await userManager.SetEmailAsync(user, input.Email);
            await userManager.SetPhoneNumberAsync(user, input.PhoneNumber);
          

            // Değişiklikleri kaydediyoruz
            await doctorRepository.UpdateAsync(doctor);
            await userManager.UpdateAsync(user);

            // Güncellenmiş DoctorWithNavigationProperties nesnesini getiriyoruz
            var updatedDoctorWithNavProps = await doctorRepository.GetWithNavigationProperties(doctor.Id);

            // DTO'ya mapliyoruz
            var doctorDto = ObjectMapper.Map<DoctorWithNavigationProperties, DoctorDto>(updatedDoctorWithNavProps);

            return doctorDto;
        }

     
    }
}
