using System;
using System.Security.Claims;

namespace Grosvenor.Portal.Web
{
    public static class UserExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var claim = principal.FindFirstValue(MagicStrings.ClaimName.ObjectId);
            return Guid.Parse(claim);
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            var claim = principal.FindFirstValue(MagicStrings.ClaimName.Email);
            return claim;
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            var claim = principal.FindFirstValue(MagicStrings.ClaimName.Name);
            return claim;
        }

        public static bool IsAdmin(this ClaimsPrincipal principal)
        {
            return principal.IsInRole(MagicStrings.UserRole.Admin);
        }
    }
}
