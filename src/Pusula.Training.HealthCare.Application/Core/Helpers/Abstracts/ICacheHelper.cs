using Pusula.Training.HealthCare.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Pusula.Training.HealthCare.Core.Helpers.Abstracts;

public interface ICacheHelper<TDto, TInput> : IScopedDependency
{
    //Nil Birlik ve Feyza Özçini Pair Programming ile Yapıldı.
    Task<PagedResultDto<TDto>> GetOrAddAsync(
        Func<Task<PagedResultDto<TDto>>> fetchFromDb,
        TInput input,
        string cacheKeyBase,
        TimeSpan cacheDuration);

    Task<PagedResultDto<LookupDto<TKey>>> GetOrAddLookupAsync<TKey>(
        Func<Task<PagedResultDto<LookupDto<TKey>>>> fetchFromDb,
        TInput input,
        string cacheKeyBase,
        TimeSpan cacheDuration);
}

