using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.ControlNotes
{
    public interface IControlNoteRepository : IRepository<ControlNote,Guid>
    {

        Task<ControlNote> GetWithNavigationPropertiesByProtocolIdAsync(Guid protocolId,CancellationToken cancellationToken = default);

        Task<List<ControlNote>> GetListWithNavigationPropertiesAsync(string? filterText = null, Guid? protocolId = null,DateTime? 
            noteDate = null,string? note = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<List<ControlNote>> GetListAsync( string? filterText = null, Guid? protocolId = null, DateTime? noteDate = null, 
            string? note = null, string? sorting = null,int maxResultCount = int.MaxValue,int skipCount = 0, 
            CancellationToken cancellationToken = default );

        Task<long> GetCountAsync( string? filterText = null, Guid? protocolId = null,DateTime? noteDate = null, string? note = null,
            CancellationToken cancellationToken = default);
    }
}
