using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Web.API.App_Start;
using WarStreamer.Web.API.Authentication;
using WarStreamer.Web.API.Models;

namespace WarStreamer.Web.API.Controllers
{
    [Route("auth/")]
    public class AuthController(AuthenticationService authService) : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly AuthenticationService _authService = authService;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*               GET               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpGet("login")]
        public async Task<ActionResult> LoginWithDiscord(string code)
        {
            try
            {
                // Get the access token
                AuthenticationToken authToken = await _authService.GetAccessToken(code);

                // Get the user informations with the access token
                DiscordUser user = await _authService.GetUserInformations(authToken.AccessToken);

                // Build JWT token
                string jwtToken = _authService.GetJwtToken(user);

                return Ok(new { AccessToken = jwtToken });
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpGet("secure")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult SecureResource()
        {
            return Ok();
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */
    }
}
