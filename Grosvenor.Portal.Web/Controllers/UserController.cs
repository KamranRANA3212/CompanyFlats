using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Grosvenor.Portal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var model = new
            {
                Name = User.GetName(),
                IsAdmin = User.IsAdmin()
            };

            return Ok(model);
        }
    }
}
