using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp;
using Pusula.Training.HealthCare.Cities;

namespace Pusula.Training.HealthCare.Titles
{
    public class TitleManager(ITitleRepository titleRepository) : DomainService
    {
        public virtual async Task<Title> CreateAsync(string name)
        {

            var title = new Title(
                Guid.NewGuid(),
                name
            );

            title.SetName(name);

            return await titleRepository.InsertAsync(title);
        }

        public virtual async Task<Title> UpdateAsync(Guid id, string name)
        {
            var title = await titleRepository.GetAsync(id);
            title.SetName(name);

            return await titleRepository.UpdateAsync(title);
        }

        public virtual async void DeleteAsync(Guid id)
        {
            var title = await titleRepository.GetAsync(id);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(title?.Name, nameof(title));
            await titleRepository.DeleteAsync(id);
        }
    }
}
