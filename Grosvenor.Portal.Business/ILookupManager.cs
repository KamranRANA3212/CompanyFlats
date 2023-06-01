using Grosvenor.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Business
{
    public interface ILookupManager
    {
        Task<List<LookUp>> GetByCategoryAsync(string category);
    }
}
