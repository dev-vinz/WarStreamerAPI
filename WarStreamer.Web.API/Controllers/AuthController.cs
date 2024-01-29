using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.App_Start;
using WarStreamer.Web.API.Authentication;
using WarStreamer.Web.API.Extensions;
using WarStreamer.Web.API.Models;
using WarStreamer.Web.API.RequestModels;
using WarStreamer.Web.API.ResponseModels;

namespace WarStreamer.Web.API.Controllers
{
    [Authorize]
    [Route("auth/")]
    public class AuthController(
        AuthenticationService authService,
        IWebHostEnvironment environment,
        IAuthTokenMap authTokenMap,
        ILanguageMap languageMap,
        IUserMap userMap
    ) : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly AuthenticationService _authService = authService;

        private readonly IWebHostEnvironment _environment = environment;

        private readonly IAuthTokenMap _authTokenMap = authTokenMap;
        private readonly ILanguageMap _languageMap = languageMap;
        private readonly IUserMap _userMap = userMap;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*               GET               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpGet("@me")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DiscordUser>> GetUserInformations()
        {
            try
            {
                // Get the user id
                string userId = User.GetDiscordId();

                // Get the authentication token associated to the user
                AuthTokenViewModel? authToken = _authTokenMap.GetByUserId(userId);

                // Verify it exists
                if (authToken == null)
                {
                    return BadRequest(
                        new { error = $"AuthToken not found for user with id '{userId}'" }
                    );
                }

                // Fetch discord refresh token
                string discordRefreshToken = _authService.DecryptToken(
                    authToken.DiscordToken,
                    authToken.DiscordIV
                );

                // Refresh discord tokens
                DiscordAuthTokens discordTokens = await _authService.GetDiscordTokens(
                    discordRefreshToken
                );

                // Fetch user informations
                DiscordUser user = await _authService.GetUserInformations(
                    discordTokens.AccessToken
                );

                // Encrypt discord refresh token
                string cipherDiscord = _authService.EncryptToken(
                    discordTokens.RefreshToken,
                    out string initVectorDiscord
                );

                // Update the auth token
                authToken.DiscordToken = cipherDiscord;
                authToken.DiscordIV = initVectorDiscord;

                _authTokenMap.Update(authToken);

                // Return discord user
                return Ok(user);
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
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        [AllowAnonymous]
        [HttpPost("login")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TokenResponseModel>> LoginWithDiscord(
            [FromBody] DiscordAuthRequestModel discordAuth
        )
        {
            try
            {
                // Get the access token
                DiscordAuthTokens discordTokens = await _authService.GetDiscordTokens(
                    discordAuth.Code,
                    discordAuth.CodeVerifier
                );

                // Get the user informations with the access token
                DiscordUser user = await _authService.GetUserInformations(
                    discordTokens.AccessToken
                );

                // Check if user is already logged in
                AuthTokenViewModel? anyAuthToken = _authTokenMap.GetByUserId(user.Id);

                // If token is still valid, refresh them
                if (anyAuthToken?.ExpiresAt >= DateTime.UtcNow)
                {
                    return await RefreshTokens(anyAuthToken);
                }
                else if (anyAuthToken != null)
                {
                    // Token isn't valid anymore, but old one hasn't been deleted
                    _authTokenMap.Delete(anyAuthToken);
                }

                UserViewModel? anyUser = _userMap.GetById(user.Id);

                // Create user if he doesn't exists
                if (anyUser == null)
                {
                    anyUser = new UserViewModel(user.Id)
                    {
                        LanguageId = _languageMap.GetAll().First().Id,
                        TierLevel = 0,
                        NewsLetter = false,
                    };

                    UserViewModel userCreated = _userMap.Create(anyUser);

                    // Create the user folder, for future images and logos
                    if (!Directory.Exists($@"{_environment.WebRootPath}\{userCreated.Id}"))
                    {
                        Directory.CreateDirectory($@"{_environment.WebRootPath}\{userCreated.Id}");
                    }
                }

                // Build the JWT token
                string jwtToken = _authService.BuildJwtToken(user, discordAuth.State);

                // Encrypt the tokens
                string cipherAccess = _authService.EncryptToken(
                    jwtToken,
                    out string initVectorAccess
                );

                string cipherDiscord = _authService.EncryptToken(
                    discordTokens.RefreshToken,
                    out string initVectorDiscord
                );

                // Build a AuthRefreshToken viewmodel
                AuthTokenViewModel authToken =
                    new(user.Id)
                    {
                        AccessToken = cipherAccess,
                        AccessIV = initVectorAccess,
                        DiscordToken = cipherDiscord,
                        DiscordIV = initVectorDiscord,
                    };

                // Save to database
                _authTokenMap.Create(authToken);

                // Send token
                return Ok(new TokenResponseModel() { Token = jwtToken });
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

        [HttpPost("logout")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<bool> Logout()
        {
            // Fetch any token
            string userId = User.GetDiscordId();
            AuthTokenViewModel? authToken = _authTokenMap.GetByUserId(userId);

            // Verify if the token exists
            if (authToken == null)
            {
                return BadRequest(
                    new { error = $"AuthToken not found for user with id '{userId}'" }
                );
            }

            return Ok(_authTokenMap.Delete(authToken));
        }

        [HttpPost("refresh")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TokenResponseModel>> RefreshTokens()
        {
            try
            {
                // Get the user id
                string userId = User.GetDiscordId();

                // Get the authentication token associated to the user
                AuthTokenViewModel? authToken = _authTokenMap.GetByUserId(userId);

                // Verify token exists
                if (authToken == null)
                {
                    return Unauthorized(new { error = "No authentication token registered" });
                }

                // Verify that authentication token is still valid
                if (authToken.ExpiresAt < DateTime.UtcNow)
                {
                    _authTokenMap.Delete(authToken);
                    return Unauthorized(new { error = "Authentication token has expired" });
                }

                return await RefreshTokens(authToken);
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

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private async Task<ActionResult<TokenResponseModel>> RefreshTokens(
            AuthTokenViewModel authToken
        )
        {
            try
            {
                // Decrypt the discord refresh token
                string discordRefreshToken = _authService.DecryptToken(
                    authToken.DiscordToken,
                    authToken.DiscordIV
                );

                // Refresh the discord tokens
                DiscordAuthTokens discordTokens = await _authService.GetDiscordTokens(
                    discordRefreshToken
                );

                // Get the user informations with the access token
                DiscordUser user = await _authService.GetUserInformations(
                    discordTokens.AccessToken
                );

                // Build the JWT token
                string jwtToken = _authService.BuildJwtToken(user, string.Empty);

                // Encrypt the new tokens
                string cipherAccess = _authService.EncryptToken(
                    jwtToken,
                    out string initVectorAccess
                );

                string cipherDiscord = _authService.EncryptToken(
                    discordTokens.RefreshToken,
                    out string initVectorDiscord
                );

                // Update the auth token
                authToken.AccessToken = cipherAccess;
                authToken.AccessIV = initVectorAccess;
                authToken.DiscordToken = cipherDiscord;
                authToken.DiscordIV = initVectorDiscord;

                _authTokenMap.Update(authToken);

                // Send token
                return Ok(new TokenResponseModel() { Token = jwtToken });
            }
            catch (HttpRequestException)
            {
                return BadRequest(new { error = "Discord refresh token invalid" });
            }
        }
    }
}
