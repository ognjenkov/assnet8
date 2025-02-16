using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Services.Entries
{
    public interface IEntriesService
    {
        public Task<List<Entry>> GetEntriesFromGameId(Guid gameId);
    }
}