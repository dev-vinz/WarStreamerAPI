using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.RequestModels;
using WarStreamer.Web.API.ResponseModels;

namespace WarStreamer.Web.API.Controllers
{
    [Authorize]
    [Route("teamlogos/")]
    public class TeamLogoController(
        IWebHostEnvironment environment,
        ITeamLogoMap logoMap,
        IUserMap userMap
    ) : Controller
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
        public ActionResult<List<TeamLogoResponseModel>> GetAll()
        {
            List<TeamLogoResponseModel> result = _logoMap
                .GetAll()
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

        /* * * * * * * * * * * * * * * * * *\
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPost]
        [Route("")]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TeamLogoResponseModel> Create(
            [FromForm] TeamLogoRequestModel logoRequest
        )
        {
            // Verify that user id exists
            if (_userMap.GetById(logoRequest.UserId) == null)
            {
                return BadRequest(new { error = $"User with id '{logoRequest.UserId}' not found" });
            }

            // Verify if a logo already exists with this name
            if (_logoMap.GetByUserIdAndName(logoRequest.UserId, logoRequest.TeamName) != null)
            {
                return Conflict(
                    new { error = $"Team logo with name '{logoRequest.TeamName}' already exists" }
                );
            }

            // Try to save the logo in wwwroot folder
            if (!TrySaveLogo(logoRequest.Logo, logoRequest.UserId, logoRequest.TeamName))
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { error = "Cannot save team logo into server" }
                );
            }

            // Create a team logo viewmodel...
            TeamLogoViewModel logo = new(logoRequest.TeamName, logoRequest.UserId);

            // ... and get the created one
            TeamLogoViewModel createdLogo = _logoMap.Create(logo);

            // Try to recover the logo from wwwroot folder
            if (!TryGetLogo(createdLogo.UserId, createdLogo.TeamName, out byte[] imageData))
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
                    Logo = imageData,
                };

            return Created(
                $"~/teamlogos/{createdLogo.UserId}/{createdLogo.TeamName}",
                logoResponse
            );
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPut]
        [Route("{userId}/{name}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Update(
            string userId,
            string name,
            [FromForm] TeamLogoRequestModel logoRequest
        )
        {
            // Verify if the team logo exists
            if (_logoMap.GetByUserIdAndName(userId, name) == null)
            {
                return NotFound(
                    new { error = $"Team logo with user id '{userId}' and name '{name}' not found" }
                );
            }

            // Try to update the logo in wwwroot folder
            if (!TrySaveLogo(logoRequest.Logo, userId, name))
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { error = "Cannot save logo into server" }
                );
            }

            // Create a new team logo viewmodel
            TeamLogoViewModel logo = new(name, userId) { ClanTags = logoRequest.ClanTags };

            return Ok(_logoMap.Update(logo));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */

        [HttpDelete]
        [Route("{userId}/{name}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Delete(string userId, string name)
        {
            TeamLogoViewModel? logo = _logoMap.GetByUserIdAndName(userId, name);

            // Verify if the team logo exists
            if (logo == null)
            {
                return NotFound(
                    new { error = $"Team logo with user id '{userId}' and name '{name}' not found" }
                );
            }

            // Delete the logo in wwwroot folder
            string path = $@"{_environment.WebRootPath}\{userId}\{RELATIVE_PATH}";

            if (System.IO.File.Exists($@"{path}\{name.ToUpper()}.png"))
            {
                System.IO.File.Delete($@"{path}\{name.ToUpper()}.png");
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
            // OverlaySettingId = UserId in this case

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
