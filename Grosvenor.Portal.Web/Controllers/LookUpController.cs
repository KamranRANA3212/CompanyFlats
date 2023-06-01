using Grosvenor.Portal.Business;
using Grosvenor.Portal.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookUpController : ControllerBase
    {
        public readonly ILookupManager lookupManager;
        public LookUpController(ILookupManager lookupManager)
        {
            this.lookupManager = lookupManager;
        }
    
        [HttpGet]
        public async Task<List<LookUp>>  GetLookupByCategory(string category)
        {
           return await lookupManager.GetByCategoryAsync(category);
        }

    }
}
