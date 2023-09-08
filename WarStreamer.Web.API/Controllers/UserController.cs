using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.ResponseModels;

namespace WarStreamer.Web.API.Controllers
{
    [Route("users/")]
    public class UserController : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWebHostEnvironment _environment;

        private readonly IAccountMap _accountMap;
        private readonly ILanguageMap _languageMap;
        private readonly ITeamLogoMap _logoMap;
        private readonly IUserMap _userMap;
        private readonly IWarOverlayMap _overlayMap;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public UserController(IWebHostEnvironment environment, IAccountMap accountMap, ILanguageMap languageMap, ITeamLogoMap logoMap, IUserMap userMap, IWarOverlayMap overlayMap)
        {
            _environment = environment;

            _accountMap = accountMap;
            _languageMap = languageMap;
            _logoMap = logoMap;
            _userMap = userMap;
            _overlayMap = overlayMap;
        }

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
        public ActionResult<UserViewModel?> GetById(decimal userId)
        {
            UserViewModel? user = _userMap.GetById(userId);

            // Verifies user exists
            if (user == null) return NotFound(new { error = $"User with id '{userId}' not found" });

            return Ok(user);
        }

        [HttpGet]
        [Route("{userId}/accounts")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string[]> GetAccounts(decimal userId)
        {
            UserViewModel? user = _userMap.GetById(userId);

            // Verifies user exists
            if (user == null) return NotFound(new { error = $"User with id '{userId}' not found" });

            return Ok(_accountMap.GetByUserId(userId));
        }

        [HttpGet]
        [Route("{userId}/language")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LanguageViewModel> GetLanguage(decimal userId)
        {
            UserViewModel? user = _userMap.GetById(userId);

            // Verifies user exists
            if (user == null) return NotFound(new { error = $"User with id '{userId}' not found" });

            LanguageViewModel? language = _languageMap.GetById(user.LanguageId);

            // Verifies that language still exists
            if (language == null) return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"Language with id '{user.LanguageId}' has been deleted from the server" });

            return Ok(language);
        }

        [HttpGet]
        [Route("{userId}/teamlogos")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<TeamLogoResponseModel>> GetTeamLogos(decimal userId)
        {
            // Verifies if user exists
            if (_userMap.GetById(userId) == null) return NotFound(new { error = $"User with id '{userId}' not found" });

            List<TeamLogoResponseModel> result = _logoMap.GetByUserId(userId)
                .Select(i => new TeamLogoResponseModel
                {
                    TeamName = i.TeamName,
                    UserId = i.UserId,
                    Logo = GetLogo(i.UserId, i.TeamName),
                    Width = i.Width,
                    Height = i.Height
                })
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("{userId}/waroverlays")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<WarOverlayViewModel>> GetWarOverlays(decimal userId)
        {
            // Verifies if user exists
            if (_userMap.GetById(userId) == null) return NotFound(new { error = $"User with id '{userId}' not found" });

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
            // Verifies if user already exists
            if (_userMap.GetById(user.Id) != null) return Conflict(new { error = $"User with id '{user.Id}' already exists" });

            // Verifies languageId
            if (_languageMap.GetById(user.LanguageId) == null) return BadRequest(new { error = $"Language with id '{user.LanguageId}' not found" });

            // Creates user folder, for future images
            if (!Directory.Exists($@"{_environment.WebRootPath}\{user.Id}")) Directory.CreateDirectory($@"{_environment.WebRootPath}\{user.Id}");

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
        public ActionResult<bool> Update(decimal userId, [FromBody] UserViewModel user)
        {
            // Verifies if user exists
            if (_userMap.GetById(userId) == null) return NotFound(new { error = $"User with id '{userId}' not found" });

            // Verifies languageId
            if (_languageMap.GetById(user.LanguageId) == null) return BadRequest(new { error = $"Language with id '{user.LanguageId}' not found" });

            // Creates a new user with id
            user = new(userId)
            {
                LanguageId = user.LanguageId,
                TierLevel = user.TierLevel,
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
        public ActionResult<bool> Delete(decimal userId)
        {
            UserViewModel? user = _userMap.GetById(userId);

            // Verifies user exists
            if (user == null) return NotFound(new { error = $"User with id '{userId}' not found" });

            // Deletes user folder, with existing images
            if (Directory.Exists($@"{_environment.WebRootPath}\{user.Id}")) Directory.Delete($@"{_environment.WebRootPath}\{user.Id}", true);

            return Ok(_userMap.Delete(user));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private byte[] GetLogo(decimal userId, string name)
        {
            TryGetLogo(userId, name, out byte[] logo);

            return logo;
        }

        private bool TryGetLogo(decimal userId, string name, out byte[] logo)
        {
            // Default image
            logo = null!;

            string path = $@"{_environment.WebRootPath}\{userId}\{TeamLogoController.RELATIVE_PATH}";

            if (!Directory.Exists(path)) return false;

            try
            {
                using FileStream filestream = new($@"{path}\{name.ToUpper()}.png", FileMode.Open);
                using MemoryStream stream = new();

                filestream.CopyTo(stream);
                logo = stream.ToArray();

                return true;
            }
            catch (FileNotFoundException) { }

            return false;
        }
    }
}
