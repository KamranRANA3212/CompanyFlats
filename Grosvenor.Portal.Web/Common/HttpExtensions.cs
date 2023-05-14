using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Grosvenor.Portal.Web
{
    public static class HttpExtensions
    {
        public static void SetCacheControl(this HttpContext context)
        {
            context.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            context.Response.Headers.Add("Pragma", "no-cache");
            context.Response.Headers.Add("Expires", "-1");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
        }
    }
}
