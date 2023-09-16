using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.RequestModels;
using WarStreamer.Web.API.ResponseModels;

namespace WarStreamer.Web.API.Controllers
{
    [Route("teamlogos/")]
    public class TeamLogoController : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public static readonly string CONTENT_TYPE = "image/png";
        public static readonly string RELATIVE_PATH = "TeamLogos";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWebHostEnvironment _environment;

        private readonly ITeamLogoMap _logoMap;
        private readonly IUserMap _userMap;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public TeamLogoController(IWebHostEnvironment environment, ITeamLogoMap logoMap, IUserMap userMap)
        {
            _environment = environment;

            _logoMap = logoMap;
            _userMap = userMap;
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
        public ActionResult<List<TeamLogoResponseModel>> GetAll()
        {
            List<TeamLogoResponseModel> result = _logoMap.GetAll()
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
        [Route("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<TeamLogoResponseModel>> GetAllByUserId(string userId)
        {
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

        /* * * * * * * * * * * * * * * * * *\
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TeamLogoResponseModel> Create([FromForm] TeamLogoRequestModel logoRequest)
        {
            // Verifies user id
            if (_userMap.GetById(logoRequest.UserId) == null) return BadRequest(new { error = $"User with id '{logoRequest.UserId}' not found" });

            // Verifies if user already exists
            if (_logoMap.GetByUserIdAndName(logoRequest.UserId, logoRequest.TeamName) != null) return Conflict(new { error = $"Team logo with name '{logoRequest.TeamName}' already exists" });

            if (!TrySaveLogo(logoRequest.Logo, logoRequest.UserId, logoRequest.TeamName))
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Cannot save team logo into server" });

            TeamLogoViewModel logo = new(logoRequest.TeamName, logoRequest.UserId)
            {
                Width = logoRequest.Width,
                Height = logoRequest.Height,
            };

            TeamLogoViewModel createdLogo = _logoMap.Create(logo);

            if (!TryGetLogo(createdLogo.UserId, createdLogo.TeamName, out byte[] imageData))
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Cannot get team logo from server" });

            // Creates full response with image in body
            TeamLogoResponseModel logoResponse = new()
            {
                UserId = createdLogo.UserId,
                TeamName = createdLogo.TeamName,
                Logo = imageData,
                Width = createdLogo.Width,
                Height = createdLogo.Height,
            };

            return Created($"~/teamlogos/{createdLogo.UserId}/{createdLogo.TeamName}", logoResponse);
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPut]
        [Route("{userId}/{name}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> Update(string userId, string name, [FromForm] TeamLogoRequestModel logoRequest)
        {
            // Verifies if logo exists
            if (_logoMap.GetByUserIdAndName(userId, name) == null) return NotFound(new { error = $"Team logo with user id '{userId}' and name '{name}' not found" });

            if (!TrySaveLogo(logoRequest.Logo, userId, name))
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Cannot save team logo into server" });

            // Creates team logo
            TeamLogoViewModel logo = new(name, userId)
            {
                Width = logoRequest.Width,
                Height = logoRequest.Height,
            };

            return Ok(_logoMap.Update(logo));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */

        [HttpDelete]
        [Route("{userId}/{name}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Delete(string userId, string name)
        {
            TeamLogoViewModel? logo = _logoMap.GetByUserIdAndName(userId, name);

            // Verifies if team logo exists
            if (logo == null) return NotFound(new { error = $"Team logo with user id '{userId}' and name '{name}' not found" });

            // Deletes image
            string path = $@"{_environment.WebRootPath}\{userId}\{RELATIVE_PATH}";
            if (System.IO.File.Exists($@"{path}\{name.ToUpper()}.png")) System.IO.File.Delete($@"{path}\{name.ToUpper()}.png");

            return Ok(_logoMap.Delete(logo));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private byte[] GetLogo(string userId, string name)
        {
            TryGetLogo(userId, name, out byte[] logo);

            return logo;
        }

        private bool TryGetLogo(string userId, string name, out byte[] logo)
        {
            // Default image
            logo = null!;

            string path = $@"{_environment.WebRootPath}\{userId}\{RELATIVE_PATH}";

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

        private bool TrySaveLogo(IFormFile? file, string userId, string name)
        {
            // OverlaySettingId = UserId in this case

            if (file == null || file.Length < 1) return false;

            // Checks that file is a PNG
            if (!file.ContentType.Equals(CONTENT_TYPE, StringComparison.InvariantCultureIgnoreCase)) return false;

            string path = $@"{_environment.WebRootPath}\{userId}\{RELATIVE_PATH}";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            using FileStream fileStream = System.IO.File.Create($@"{path}\{name.ToUpper()}.png");
            file.CopyTo(fileStream);
            fileStream.Flush();
            fileStream.Dispose();

            return true;
        }
    }
}
