using System.Security.Claims;
using IdentityModel;
using WarStreamer.Commons.Extensions;

namespace WarStreamer.Web.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /* * * * * * * * * * * * * * * * * *\
        |*               GUID              *|
        \* * * * * * * * * * * * * * * * * */

        public static Guid GetDiscordIdAsGuid(this ClaimsPrincipal principal)
        {
            string userId = principal.GetDiscordId();

            if (userId == string.Empty)
            {
                return Guid.Empty;
            }
            else
            {
                return Guid.Empty.ParseDiscordId(userId);
            }
        }

        /* * * * * * * * * * * * * * * * * *\
        |*              STRING             *|
        \* * * * * * * * * * * * * * * * * */

        public static string GetDiscordId(this ClaimsPrincipal principal)
        {
            ClaimsIdentity? identity = principal.Identity as ClaimsIdentity;

            return identity?.FindFirst(JwtClaimTypes.Subject)?.Value
                ?? identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? string.Empty;
        }
    }
}
