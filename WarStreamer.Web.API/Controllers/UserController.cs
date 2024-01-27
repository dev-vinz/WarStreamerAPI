using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.Extensions;

namespace WarStreamer.Web.API.Controllers
{
    [Authorize]
    [Route("user/")]
    public class UserController(
        IWebHostEnvironment environment,
        ILanguageMap languageMap,
        IUserMap userMap
    ) : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWebHostEnvironment _environment = environment;

        private readonly ILanguageMap _languageMap = languageMap;
        private readonly IUserMap _userMap = userMap;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*               GET               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<UserViewModel?> Get()
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            return Ok(_userMap.GetById(userId));
        }

        [HttpGet]
        [Route("language")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LanguageViewModel> GetLanguage()
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            UserViewModel user = _userMap.GetById(userId)!;
            LanguageViewModel? language = _languageMap.GetById(Guid.Parse(user.LanguageId));

            // Verify that the language still exists
            if (language == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        error = $"Language with id '{user.LanguageId}' has been deleted from the server"
                    }
                );
            }

            return Ok(language);
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPut]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<bool> Update([FromBody] UserViewModel user)
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            // Ensure both user ids are the same
            if (user.Id != userId)
            {
                return Forbid();
            }

            UserViewModel anyUser = _userMap.GetById(userId)!;

            // Verify the languageId
            if (_languageMap.GetById(Guid.Parse(user.LanguageId)) == null)
            {
                return BadRequest(
                    new { error = $"Language with id '{user.LanguageId}' not found" }
                );
            }

            // Update the user
            anyUser.LanguageId = user.LanguageId;
            anyUser.TierLevel = user.TierLevel;
            anyUser.NewsLetter = user.NewsLetter;

            return Ok(_userMap.Update(anyUser));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */

        [HttpDelete]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Delete()
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            UserViewModel user = _userMap.GetById(userId)!;

            // Delete the user folder, with existing images inside
            if (Directory.Exists($@"{_environment.WebRootPath}\{user.Id}"))
            {
                Directory.Delete($@"{_environment.WebRootPath}\{user.Id}", true);
            }

            return Ok(_userMap.Delete(user));
        }
    }
}
