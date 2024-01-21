using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.App_Start;
using WarStreamer.Web.API.Authentication;
using WarStreamer.Web.API.Models;
using WarStreamer.Web.API.ResponseModels;

namespace WarStreamer.Web.API.Controllers
{
    [Route("auth/")]
    public class AuthController(
        AuthenticationService authService,
        IAuthRefreshTokenMap authTokenMap
    ) : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly AuthenticationService _authService = authService;

        private readonly IAuthRefreshTokenMap _authRefreshTokenMap = authTokenMap;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*               GET               *|
        \* * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPost("login")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TokenResponseModel>> LoginWithDiscord([FromBody] string code)
        {
            try
            {
                // Get the access token
                AuthenticationToken authToken = await _authService.GetAccessToken(code);

                // Get the user informations with the access token
                DiscordUser user = await _authService.GetUserInformations(authToken.AccessToken);

                // Build JWT token and refresh token
                string jwtToken = _authService.GetJwtToken(user);
                string refreshToken = _authService.BuildRefreshTokenFromDiscord(
                    authToken.RefreshToken,
                    out string initializationVector
                );

                // Build a AuthRefreshToken viewmodel
                AuthRefreshTokenViewModel authRefreshToken =
                    new(user.Id)
                    {
                        TokenValue = refreshToken,
                        AesInitializationVector = initializationVector
                    };

                // Save to database
                _authRefreshTokenMap.Create(authRefreshToken);

                return Ok(
                    new TokenResponseModel() { AccessToken = jwtToken, RefreshToken = refreshToken }
                );
            }
            catch (HttpRequestException)
            {
                return BadRequest(new { error = "Discord code invalid" });
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPost("logout")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<bool> Logout([FromBody] string userId)
        {
            AuthRefreshTokenViewModel? authRefreshToken = _authRefreshTokenMap.GetByUserId(userId);

            // Verify if the token exists
            if (authRefreshToken == null)
            {
                return BadRequest(
                    new { error = $"AuthRefreshToken not found for user with id '{userId}'" }
                );
            }

            return Ok(_authRefreshTokenMap.Delete(authRefreshToken));
        }

        [HttpPost("refresh")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TokenResponseModel>> RefreshTokens(
            [FromForm, Required] string userId,
            [FromForm, Required] string refreshToken
        )
        {
            try
            {
                AuthRefreshTokenViewModel? authRefreshToken = _authRefreshTokenMap.GetByUserId(
                    userId
                );

                // Verify token exists
                if (authRefreshToken == null)
                {
                    return Unauthorized(
                        new { error = $"No refresh token registered for user with id '{userId}'" }
                    );
                }

                // Verify if refreshToken is equal to the one that is registered
                if (authRefreshToken.TokenValue != refreshToken)
                {
                    return Unauthorized(new { error = "The refresh token is invalid" });
                }

                // Verify that refresh token is still valid
                if (authRefreshToken.ExpiresAt < DateTime.UtcNow)
                {
                    _authRefreshTokenMap.Delete(authRefreshToken);
                    return Unauthorized(new { error = "Refresh token has expired" });
                }

                // Recover the discord refresh token
                string discordRefresh = _authService.GetDiscordRefreshToken(
                    authRefreshToken.TokenValue,
                    authRefreshToken.AesInitializationVector
                );

                // Refresh the access token
                AuthenticationToken authToken = await _authService.RefreshAccessToken(
                    discordRefresh
                );

                // Get the user informations with the access token
                DiscordUser user = await _authService.GetUserInformations(authToken.AccessToken);

                // Build JWT token and refresh token
                string jwtToken = _authService.GetJwtToken(user);
                string newRefreshToken = _authService.BuildRefreshTokenFromDiscord(
                    authToken.RefreshToken,
                    out string initializationVector
                );

                // Update the token from the database
                authRefreshToken.TokenValue = newRefreshToken;
                authRefreshToken.AesInitializationVector = initializationVector;

                _authRefreshTokenMap.Update(authRefreshToken);

                return Ok(
                    new TokenResponseModel()
                    {
                        AccessToken = jwtToken,
                        RefreshToken = newRefreshToken
                    }
                );
            }
            catch (HttpRequestException)
            {
                return BadRequest(new { error = "Discord refresh token invalid" });
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */
    }
}
