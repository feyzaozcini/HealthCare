using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp;


namespace Pusula.Training.HealthCare.Notes
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Notes.Default)]
    public class NotesAppService(INoteRepository noteRepository,
        NoteManager noteManager, IDistributedCache<NoteDownloadTokenCacheItem, string> downloadTokenCache)
        : HealthCareAppService, INotesAppService
    {
        public virtual async Task<PagedResultDto<NoteDto>> GetListAsync(GetNotesInput input)
        {
            var totalCount = await noteRepository.GetCountAsync(input.FilterText, input.Text);
            var items = await noteRepository.GetListAsync(input.FilterText, input.Text, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<NoteDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Note>, List<NoteDto>>(items)
            };
        }

        public virtual async Task<NoteDto> GetAsync(Guid id) => ObjectMapper.Map<Note, NoteDto>(await noteRepository.GetAsync(id));


        [Authorize(HealthCarePermissions.Notes.Delete)]
        public virtual async Task DeleteAsync(Guid id) => await noteRepository.DeleteAsync(id);


        [Authorize(HealthCarePermissions.Notes.Create)]
        public virtual async Task<NoteDto> CreateAsync(NoteCreateDto input) => ObjectMapper.Map<Note, NoteDto>(await noteManager.CreateAsync(input.Text));


        [Authorize(HealthCarePermissions.Notes.Edit)]
        public virtual async Task<NoteDto> UpdateAsync(NoteUpdateDto input) => ObjectMapper.Map<Note, NoteDto>(await noteManager.UpdateAsync(input.Id, input.Text, input.ConcurrencyStamp));


        [Authorize(HealthCarePermissions.Notes.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> noteIds) => await noteRepository.DeleteManyAsync(noteIds);


        [Authorize(HealthCarePermissions.Notes.Delete)]
        public virtual async Task DeleteAllAsync(GetNotesInput input) => await noteRepository.DeleteAllAsync(input.FilterText, input.Text);


        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new NoteDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}
