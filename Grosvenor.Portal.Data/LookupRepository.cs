using Grosvenor.Portal.Data.Context;
using Grosvenor.Portal.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Data
{
    public class LookupRepository : ILookupRepository
    {
        public readonly PortalContext portalContext;
        public LookupRepository(PortalContext portalContext)
        {
            this.portalContext = portalContext;
        }
        public async Task<List<LookUp>> GetLookupByCategoryAsync(string category)
        {
               return await portalContext.LookUps.Where(x => x.Category == category).ToListAsync();
            
        }
    }
}
