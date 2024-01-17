using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;

namespace WarStreamer.Web.API.Controllers
{
    [Route("waroverlays/")]
    public class WarOverlayController(IUserMap userMap, IWarOverlayMap overlayMap) : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

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
        public ActionResult<List<WarOverlayViewModel>> GetAll()
        {
            return Ok(_overlayMap.GetAll());
        }

        [HttpGet]
        [Route("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<WarOverlayViewModel>> GetAllByUserId(string userId)
        {
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
        public ActionResult<WarOverlayViewModel> Create([FromBody] WarOverlayViewModel overlay)
        {
            // Verify if the user exists
            if (_userMap.GetById(overlay.UserId) == null)
            {
                return BadRequest(new { error = $"User with id '{overlay.UserId}' not found" });
            }

            // Verify if the war overlay already exists
            if (_overlayMap.GetByUserIdAndId(overlay.UserId, overlay.Id) != null)
            {
                return Conflict(
                    new
                    {
                        error = $"War overlay with user id '{overlay.UserId}' and id '{overlay.Id}' already exists"
                    }
                );
            }

            return Created(
                $"~/waroverlays/{overlay.UserId}/{overlay.Id}",
                _overlayMap.Create(overlay)
            );
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPut]
        [Route("{userId}/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Update(
            string userId,
            int id,
            [FromBody] WarOverlayViewModel overlay
        )
        {
            // Verify if the war overlay exists
            if (_overlayMap.GetByUserIdAndId(userId, id) == null)
            {
                return NotFound(
                    new { error = $"War overlay with user id '{userId}' and id '{id}' not found" }
                );
            }

            // Create a new war overlay with the same ids
            overlay = new(userId, id, overlay.ClanTag)
            {
                LastCheckout = overlay.LastCheckout,
                IsEnded = overlay.IsEnded,
            };

            return Ok(_overlayMap.Update(overlay));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */

        [HttpDelete]
        [Route("{userId}/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Delete(string userId, int id)
        {
            WarOverlayViewModel? overlay = _overlayMap.GetByUserIdAndId(userId, id);

            // Verify if the war overlay exists
            if (overlay == null)
            {
                return NotFound(
                    new { error = $"War overlay with user id '{userId}' and id '{id}' not found" }
                );
            }

            return Ok(_overlayMap.Delete(overlay));
        }
    }
}
