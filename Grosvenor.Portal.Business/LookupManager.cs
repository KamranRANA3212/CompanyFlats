using Grosvenor.Portal.Data;
using Grosvenor.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Business
{
    public class LookupManager : ILookupManager
    {
        public readonly ILookupRepository lookupRepository;
        public LookupManager(ILookupRepository lookupRepository)
        {
            this.lookupRepository = lookupRepository;
        }

        public async Task<List<LookUp>> GetByCategoryAsync(string category)
        {
            return await lookupRepository.GetLookupByCategoryAsync(category);
        }
    }
}
