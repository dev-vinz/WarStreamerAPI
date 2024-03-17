using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.Extensions;
using WarStreamer.Web.API.RequestModels;
using WarStreamer.Web.API.ResponseModels;

namespace WarStreamer.Web.API.Controllers
{
    [Authorize]
    [Route("teamlogos/")]
    public class TeamLogoController(IWebHostEnvironment environment, ITeamLogoMap logoMap)
        : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private static readonly string CONTENT_TYPE = "image/png";
        private static readonly string RELATIVE_PATH = "TeamLogos";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWebHostEnvironment _environment = environment;

        private readonly ITeamLogoMap _logoMap = logoMap;

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
        public ActionResult<List<TeamLogoResponseModel>> Get()
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            List<TeamLogoResponseModel> result = _logoMap
                .GetByUserId(userId)
                .Select(ToResponseModel)
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("{name}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TeamLogoResponseModel> Get(string name)
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            TeamLogoViewModel? logo = _logoMap.GetByUserIdAndName(userId, name);

            if (logo == null)
            {
                return NotFound(new { error = $"Team logo with name '{name}' not found" });
            }

            // Try to recover the logo from wwwroot folder
            if (!TryGetLogo(logo.UserId, logo.TeamName, out byte[] logoData))
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { error = "Cannot get team logo from server" }
                );
            }

            // Create a full response with the logo in body
            TeamLogoResponseModel logoResponse =
                new()
                {
                    UserId = logo.UserId,
                    TeamName = logo.TeamName,
                    Logo = logoData,
                    ClanTags = logo.ClanTags
                };

            return Ok(logoResponse);
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TeamLogoResponseModel> Create(
            [FromForm] TeamLogoRequestModel logoRequest
        )
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();
            string userGuid = User.GetDiscordIdAsGuid().ToString();

            // Ensure both user ids are the same
            if (logoRequest.UserId != userId)
            {
                return Forbid();
            }

            // Verify if a logo already exists with this name
            if (_logoMap.GetByUserIdAndName(userId, logoRequest.TeamName) != null)
            {
                return Conflict(
                    new { error = $"Team logo with name '{logoRequest.TeamName}' already exists" }
                );
            }

            // Try to save the logo in wwwroot folder
            if (!TrySaveLogo(logoRequest.Logo, userGuid, logoRequest.TeamName))
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { error = "Cannot save team logo into server" }
                );
            }

            // Create a team logo viewmodel...
            TeamLogoViewModel logo =
                new(userId, logoRequest.TeamName) { ClanTags = logoRequest.ClanTags };

            // ... and get the created one
            TeamLogoViewModel createdLogo = _logoMap.Create(logo);

            // Try to recover the logo from wwwroot folder
            if (!TryGetLogo(createdLogo.UserId, createdLogo.TeamName, out byte[] logoData))
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { error = "Cannot get team logo from server" }
                );
            }

            // Create a full response with the logo in body
            TeamLogoResponseModel logoResponse =
                new()
                {
                    UserId = createdLogo.UserId,
                    TeamName = createdLogo.TeamName,
                    Logo = logoData,
                    ClanTags = createdLogo.ClanTags
                };

            return Created($"~/teamlogos/{createdLogo.TeamName}", logoResponse);
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPut]
        [Route("{name}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Update(string name, [FromForm] TeamLogoRequestModel logoRequest)
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();
            string userGuid = User.GetDiscordIdAsGuid().ToString();

            // Ensure both user ids are the same
            if (logoRequest.UserId != userGuid)
            {
                return Forbid();
            }

            TeamLogoViewModel? anyLogo = _logoMap.GetByUserIdAndName(userId, name);

            // Verify if the team logo exists
            if (anyLogo == null)
            {
                return NotFound(new { error = $"Team logo with name '{name}' not found" });
            }

            // Try to update the logo in wwwroot folder
            if (!TrySaveLogo(logoRequest.Logo, anyLogo.UserId, anyLogo.TeamName))
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { error = "Cannot save logo into server" }
                );
            }

            // Update team logo
            anyLogo.ClanTags = logoRequest.ClanTags;

            return Ok(_logoMap.Update(anyLogo));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */

        [HttpDelete]
        [Route("{name}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Delete(string name)
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            TeamLogoViewModel? logo = _logoMap.GetByUserIdAndName(userId, name);

            // Verify if the team logo exists
            if (logo == null)
            {
                return NotFound(new { error = $"Team logo with name '{name}' not found" });
            }

            // Delete the logo in wwwroot folder
            string path = $@"{_environment.WebRootPath}\{logo.UserId}\{RELATIVE_PATH}";

            if (System.IO.File.Exists($@"{path}\{logo.TeamName}.png"))
            {
                System.IO.File.Delete($@"{path}\{logo.TeamName}.png");
            }

            return Ok(_logoMap.Delete(logo));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        public static byte[] GetLogo(IWebHostEnvironment environment, string userId, string name)
        {
            TryGetLogo(environment, userId, name, out byte[] logo);

            return logo;
        }

        public static bool TryGetLogo(
            IWebHostEnvironment environment,
            string userId,
            string name,
            out byte[] logo
        )
        {
            // Default logo
            logo = null!;

            string path = $@"{environment.WebRootPath}\{userId}\{RELATIVE_PATH}";

            if (!Directory.Exists(path))
            {
                return false;
            }

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

        public static bool TrySaveLogo(
            IWebHostEnvironment environment,
            IFormFile? file,
            string userId,
            string name
        )
        {
            if (file == null || file.Length < 1)
            {
                return false;
            }

            // Check that the file is a PNG
            if (!file.ContentType.Equals(CONTENT_TYPE, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            string path = $@"{environment.WebRootPath}\{userId}\{RELATIVE_PATH}";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using FileStream fileStream = System.IO.File.Create($@"{path}\{name.ToUpper()}.png");

            file.CopyTo(fileStream);

            fileStream.Flush();
            fileStream.Dispose();

            return true;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private byte[] GetLogo(string userId, string name)
        {
            return GetLogo(_environment, userId, name);
        }

        private TeamLogoResponseModel ToResponseModel(TeamLogoViewModel logo)
        {
            return new TeamLogoResponseModel
            {
                TeamName = logo.TeamName,
                UserId = logo.UserId,
                Logo = GetLogo(logo.UserId, logo.TeamName),
                ClanTags = logo.ClanTags,
            };
        }

        private bool TryGetLogo(string userId, string name, out byte[] logo)
        {
            return TryGetLogo(_environment, userId, name, out logo);
        }

        private bool TrySaveLogo(IFormFile? file, string userId, string name)
        {
            return TrySaveLogo(_environment, file, userId, name);
        }
    }
}
