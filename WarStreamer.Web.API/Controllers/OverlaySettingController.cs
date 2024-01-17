using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.ResponseModels;

namespace WarStreamer.Web.API.Controllers
{
    [Route("overlaysettings/")]
    public class OverlaySettingController(
        IWebHostEnvironment environment,
        IImageMap imageMap,
        IOverlaySettingMap overlaySettingMap,
        IUserMap userMap
    ) : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWebHostEnvironment _environment = environment;

        private readonly IImageMap _imageMap = imageMap;
        private readonly IOverlaySettingMap _overlaySettingMap = overlaySettingMap;
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
        public ActionResult<List<OverlaySettingViewModel>> GetAll()
        {
            return Ok(_overlaySettingMap.GetAll());
        }

        [HttpGet]
        [Route("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OverlaySettingViewModel> GetByUserId(string userId)
        {
            OverlaySettingViewModel? setting = _overlaySettingMap.GetByUserId(userId);

            // Verify if the overlay setting exists
            if (setting == null)
            {
                return NotFound(
                    new { error = $"Overlay setting with user id '{userId}' not found" }
                );
            }

            return Ok(setting);
        }

        [HttpGet]
        [Route("{userId}/images")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<ImageResponseModel>> GetImages(string userId)
        {
            OverlaySettingViewModel? setting = _overlaySettingMap.GetByUserId(userId);

            // Verify if the overlay setting exists
            if (setting == null)
            {
                return NotFound(
                    new { error = $"Overlay setting with user id '{userId}' not found" }
                );
            }

            List<ImageResponseModel> images = _imageMap
                .GetByOverlaySettingId(userId)
                .Select(
                    i =>
                        new ImageResponseModel
                        {
                            OverlaySettingId = i.OverlaySettingId,
                            Name = i.Name,
                            Image = GetImage(i.OverlaySettingId, i.Name),
                            LocationX = i.Location.X,
                            LocationY = i.Location.Y,
                            Width = i.Width,
                            Height = i.Height
                        }
                )
                .ToList();

            return Ok(images);
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
        public ActionResult<OverlaySettingViewModel> Create(
            [FromBody] OverlaySettingViewModel setting
        )
        {
            // Verify if the overlay setting already exists
            if (_overlaySettingMap.GetByUserId(setting.UserId) != null)
            {
                return Conflict(
                    new
                    {
                        error = $"Overlay setting with user id '{setting.UserId}' already exists"
                    }
                );
            }

            // Verify if the user exists
            if (_userMap.GetById(setting.UserId) == null)
            {
                return BadRequest(new { error = $"User with id '{setting.UserId}' not found" });
            }

            return Created(
                $"~/overlaysettings/{setting.UserId}",
                _overlaySettingMap.Create(setting)
            );
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPut]
        [Route("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Update(string userId, [FromBody] OverlaySettingViewModel setting)
        {
            // Verify if the overlay setting exists
            if (_overlaySettingMap.GetByUserId(userId) == null)
            {
                return NotFound(
                    new { error = $"Overlay setting with user id '{userId}' not found" }
                );
            }

            // Create a new overlay setting with id
            setting = new(userId)
            {
                FontId = setting.FontId,
                TextColor = setting.TextColor,
                LogoVisible = setting.LogoVisible,
                LogoSize = setting.LogoSize,
                LogoLocation = setting.LogoLocation,
                ClanNameVisible = setting.ClanNameVisible,
                ClanNameSize = setting.ClanNameSize,
                ClanNameLocation = setting.ClanNameLocation,
                TotalStarsVisible = setting.TotalStarsVisible,
                TotalStarsSize = setting.TotalStarsSize,
                TotalStarsLocation = setting.TotalStarsLocation,
                TotalPercentageVisible = setting.TotalPercentageVisible,
                TotalPercentageSize = setting.TotalPercentageSize,
                TotalPercentageLocation = setting.TotalPercentageLocation,
                AverageDurationVisible = setting.AverageDurationVisible,
                AverageDurationSize = setting.AverageDurationSize,
                AverageDurationLocation = setting.AverageDurationLocation,
                PlayerDetailsVisible = setting.PlayerDetailsVisible,
                PlayerDetailsSize = setting.PlayerDetailsSize,
                PlayerDetailsLocation = setting.PlayerDetailsLocation,
                LastAttackToWinVisible = setting.LastAttackToWinVisible,
                LastAttackToWinSize = setting.LastAttackToWinSize,
                LastAttackToWinLocation = setting.LastAttackToWinLocation,
                MirrorReflection = setting.MirrorReflection,
            };

            return Ok(_overlaySettingMap.Update(setting));
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
            OverlaySettingViewModel? setting = _overlaySettingMap.GetByUserId(userId);

            // Verify if the overlay setting exists
            if (setting == null)
            {
                return NotFound(
                    new { error = $"Overlay setting with user id '{userId}' not found" }
                );
            }

            return Ok(_overlaySettingMap.Delete(setting));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private byte[] GetImage(string overlaySettingId, string name)
        {
            return ImageController.GetImage(_environment, overlaySettingId, name);
        }
    }
}
