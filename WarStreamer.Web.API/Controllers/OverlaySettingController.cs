using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.Extensions;
using WarStreamer.Web.API.ResponseModels;

namespace WarStreamer.Web.API.Controllers
{
    [Authorize]
    [Route("overlaysetting/")]
    public class OverlaySettingController(
        IWebHostEnvironment environment,
        IImageMap imageMap,
        IOverlaySettingMap overlaySettingMap
    ) : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWebHostEnvironment _environment = environment;

        private readonly IImageMap _imageMap = imageMap;
        private readonly IOverlaySettingMap _overlaySettingMap = overlaySettingMap;

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OverlaySettingViewModel> Get()
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            OverlaySettingViewModel? overlay = _overlaySettingMap.GetByUserId(userId);

            // Verify if the user exists
            if (overlay == null)
            {
                return NotFound(new { error = "Overlay setting not found" });
            }

            return Ok(overlay);
        }

        [HttpGet]
        [Route("defaults/{overlayId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OverlaySettingViewModel> GetDefaultById(string overlayId)
        {
            if (!Guid.TryParse(overlayId, out Guid id))
            {
                return BadRequest(new { error = "Overlay id format is not supported" });
            }

            OverlaySettingViewModel? overlay = _overlaySettingMap.GetDefaultById(id);

            // Verify if the overlay exists
            if (overlay == null)
            {
                return NotFound(
                    new { error = $"Default overlay setting with id '{overlayId}' not found" }
                );
            }

            return Ok(overlay);
        }

        [HttpGet]
        [Route("images")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ImageResponseModel> GetImages()
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            List<ImageResponseModel> result = _imageMap
                .GetUsedByUserId(userId)
                .Select(ToResponseModel)
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<OverlaySettingViewModel> Create(
            [FromBody] OverlaySettingViewModel setting
        )
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            // Ensure both user ids are the same
            if (setting.UserId != userId)
            {
                return Forbid();
            }

            // Verify if the overlay setting already exists
            if (_overlaySettingMap.GetByUserId(setting.UserId) != null)
            {
                return Conflict(new { error = "Overlay setting already exists" });
            }

            return Created($"~/overlaysetting", _overlaySettingMap.Create(setting));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPut]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Update([FromBody] OverlaySettingViewModel setting)
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            // Ensure both user ids are the same
            if (setting.UserId != userId)
            {
                return Forbid();
            }

            OverlaySettingViewModel? anySetting = _overlaySettingMap.GetByUserId(userId);

            // Verify if the overlay setting exists
            if (anySetting == null)
            {
                return NotFound(new { error = $"Overlay setting not found" });
            }

            // Update the overlay setting
            anySetting.FontId = setting.FontId;
            anySetting.TextColor = setting.TextColor;

            anySetting.LogoVisible = setting.LogoVisible;
            anySetting.LogoSize = setting.LogoSize;
            anySetting.LogoLocation = setting.LogoLocation;

            anySetting.ClanNameVisible = setting.ClanNameVisible;
            anySetting.ClanNameSize = setting.ClanNameSize;
            anySetting.ClanNameLocation = setting.ClanNameLocation;

            anySetting.TotalStarsVisible = setting.TotalStarsVisible;
            anySetting.TotalStarsSize = setting.TotalStarsSize;
            anySetting.TotalStarsLocation = setting.TotalStarsLocation;

            anySetting.TotalPercentageVisible = setting.TotalPercentageVisible;
            anySetting.TotalPercentageSize = setting.TotalPercentageSize;
            anySetting.TotalPercentageLocation = setting.TotalPercentageLocation;

            anySetting.AverageDurationVisible = setting.AverageDurationVisible;
            anySetting.AverageDurationSize = setting.AverageDurationSize;
            anySetting.AverageDurationLocation = setting.AverageDurationLocation;

            anySetting.PlayerDetailsVisible = setting.PlayerDetailsVisible;
            anySetting.PlayerDetailsSize = setting.PlayerDetailsSize;
            anySetting.PlayerDetailsLocation = setting.PlayerDetailsLocation;

            anySetting.LastAttackToWinVisible = setting.LastAttackToWinVisible;
            anySetting.LastAttackToWinSize = setting.LastAttackToWinSize;
            anySetting.LastAttackToWinLocation = setting.LastAttackToWinLocation;

            anySetting.HeroesEquipmentsVisible = setting.HeroesEquipmentsVisible;
            anySetting.HeroesEquipmentsSize = setting.HeroesEquipmentsSize;
            anySetting.HeroesEquipmentsLocation = setting.HeroesEquipmentsLocation;

            anySetting.MirrorReflection = setting.MirrorReflection;

            return Ok(_overlaySettingMap.Update(anySetting));
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

            OverlaySettingViewModel? setting = _overlaySettingMap.GetByUserId(userId);

            // Verify if the overlay setting exists
            if (setting == null)
            {
                return NotFound(new { error = $"Overlay setting not found" });
            }

            return Ok(_overlaySettingMap.Delete(setting));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private byte[] GetImage(string userId, string name)
        {
            return ImageController.GetImage(_environment, userId, name);
        }

        private ImageResponseModel ToResponseModel(ImageViewModel image)
        {
            return new ImageResponseModel
            {
                UserId = image.UserId,
                Name = image.Name,
                Image = GetImage(image.UserId, image.Name),
                LocationX = image.Location.X,
                LocationY = image.Location.Y,
                Width = image.Width,
                Height = image.Height,
                IsUsed = image.IsUsed,
            };
        }
    }
}
