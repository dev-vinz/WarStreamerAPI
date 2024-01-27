using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.Extensions;

namespace WarStreamer.Web.API.Controllers
{
    [Authorize]
    [Route("waroverlays/")]
    public class WarOverlayController(IWarOverlayMap overlayMap) : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<WarOverlayViewModel>> Get()
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            return Ok(_overlayMap.GetByUserId(userId));
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
        public ActionResult<WarOverlayViewModel> Create([FromBody] WarOverlayViewModel overlay)
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            // Ensure both user ids are the same
            if (overlay.UserId != userId)
            {
                return Forbid();
            }

            // Verify if the war overlay already exists
            if (_overlayMap.GetByUserIdAndId(overlay.UserId, overlay.Id) != null)
            {
                return Conflict(
                    new { error = $"War overlay with id '{overlay.Id}' already exists" }
                );
            }

            return Created($"~/waroverlays/{overlay.Id}", _overlayMap.Create(overlay));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPut]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Update(int id, [FromBody] WarOverlayViewModel overlay)
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            // Ensure both user ids are the same
            if (overlay.UserId != userId)
            {
                return Forbid();
            }

            WarOverlayViewModel? anyOverlay = _overlayMap.GetByUserIdAndId(
                overlay.UserId,
                overlay.Id
            );

            // Verify if the war overlay exists
            if (anyOverlay == null)
            {
                return NotFound(new { error = $"War overlay with id '{id}' not found" });
            }

            // Update the overlay
            anyOverlay.LastCheckout = overlay.LastCheckout;
            anyOverlay.IsEnded = overlay.IsEnded;

            return Ok(_overlayMap.Update(anyOverlay));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */

        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Delete(int id)
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            WarOverlayViewModel? overlay = _overlayMap.GetByUserIdAndId(userId, id);

            // Verify if the war overlay exists
            if (overlay == null)
            {
                return NotFound(new { error = $"War overlay with id '{id}' not found" });
            }

            return Ok(_overlayMap.Delete(overlay));
        }
    }
}
