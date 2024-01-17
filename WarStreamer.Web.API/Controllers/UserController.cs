using Microsoft.AspNetCore.Mvc;
using WarStreamer.Commons.Extensions;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.ResponseModels;

namespace WarStreamer.Web.API.Controllers
{
    [Route("users/")]
    public class UserController(
        IWebHostEnvironment environment,
        IAccountMap accountMap,
        ILanguageMap languageMap,
        ITeamLogoMap logoMap,
        IUserMap userMap,
        IWarOverlayMap overlayMap
    ) : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWebHostEnvironment _environment = environment;

        private readonly IAccountMap _accountMap = accountMap;
        private readonly ILanguageMap _languageMap = languageMap;
        private readonly ITeamLogoMap _logoMap = logoMap;
        private readonly IUserMap _userMap = userMap;
        private readonly IWarOverlayMap _overlayMap = overlayMap;

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
        public ActionResult<List<UserViewModel>> GetAll()
        {
            return Ok(_userMap.GetAll());
        }

        [HttpGet]
        [Route("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserViewModel?> GetById(string userId)
        {
            UserViewModel? user = _userMap.GetById(userId);

            // Verify if the user exists
            if (user == null)
            {
                return NotFound(new { error = $"User with id '{userId}' not found" });
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("{userId}/accounts")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string[]> GetAccounts(string userId)
        {
            UserViewModel? user = _userMap.GetById(userId);

            // Verify if the user exists
            if (user == null)
            {
                return NotFound(new { error = $"User with id '{userId}' not found" });
            }

            return Ok(_accountMap.GetByUserId(userId).Select(a => a.Tag));
        }

        [HttpGet]
        [Route("{userId}/language")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LanguageViewModel> GetLanguage(string userId)
        {
            UserViewModel? user = _userMap.GetById(userId);

            // Verify if the user exists
            if (user == null)
            {
                return NotFound(new { error = $"User with id '{userId}' not found" });
            }

            LanguageViewModel? language = _languageMap.GetById(
                Guid.Empty.ParseDiscordId(user.LanguageId)
            );

            // Verify that tje language still exists
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

        [HttpGet]
        [Route("{userId}/teamlogos")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<TeamLogoResponseModel>> GetTeamLogos(string userId)
        {
            // Verify if the user exists
            if (_userMap.GetById(userId) == null)
            {
                return NotFound(new { error = $"User with id '{userId}' not found" });
            }

            List<TeamLogoResponseModel> result = _logoMap
                .GetByUserId(userId)
                .Select(
                    i =>
                        new TeamLogoResponseModel
                        {
                            TeamName = i.TeamName,
                            UserId = i.UserId,
                            Logo = GetLogo(i.UserId, i.TeamName),
                        }
                )
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("{userId}/waroverlays")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<WarOverlayViewModel>> GetWarOverlays(string userId)
        {
            // Verify if the user exists
            if (_userMap.GetById(userId) == null)
            {
                return NotFound(new { error = $"User with id '{userId}' not found" });
            }

            return Ok(_overlayMap.GetByUserId(userId));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<UserViewModel> Create([FromBody] UserViewModel user)
        {
            // Verify if the user already exists
            if (_userMap.GetById(user.Id) != null)
            {
                return Conflict(new { error = $"User with id '{user.Id}' already exists" });
            }

            // Verify the languageId
            if (_languageMap.GetById(Guid.Empty.ParseDiscordId(user.LanguageId)) == null)
            {
                return BadRequest(
                    new { error = $"Language with id '{user.LanguageId}' not found" }
                );
            }

            // Create the user folder, for future images and logos
            if (!Directory.Exists($@"{_environment.WebRootPath}\{user.Id}"))
            {
                Directory.CreateDirectory($@"{_environment.WebRootPath}\{user.Id}");
            }

            return Created($"~/users/{user.Id}", _userMap.Create(user));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPut]
        [Route("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Update(string userId, [FromBody] UserViewModel user)
        {
            // Verify if the user exists
            if (_userMap.GetById(userId) == null)
            {
                return NotFound(new { error = $"User with id '{userId}' not found" });
            }

            // Verify the languageId
            if (_languageMap.GetById(Guid.Empty.ParseDiscordId(user.LanguageId)) == null)
            {
                return BadRequest(
                    new { error = $"Language with id '{user.LanguageId}' not found" }
                );
            }

            // Create a new user with the same id
            user = new(userId)
            {
                LanguageId = user.LanguageId,
                TierLevel = user.TierLevel,
                NewsLetter = user.NewsLetter,
            };

            return Ok(_userMap.Update(user));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */

        [HttpDelete]
        [Route("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Delete(string userId)
        {
            UserViewModel? user = _userMap.GetById(userId);

            // Verify if the user exists
            if (user == null)
            {
                return NotFound(new { error = $"User with id '{userId}' not found" });
            }

            // Delete the user folder, with existing images inside
            if (Directory.Exists($@"{_environment.WebRootPath}\{user.Id}"))
            {
                Directory.Delete($@"{_environment.WebRootPath}\{user.Id}", true);
            }

            return Ok(_userMap.Delete(user));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private byte[] GetLogo(string userId, string name)
        {
            return TeamLogoController.GetLogo(_environment, userId, name);
        }
    }
}
