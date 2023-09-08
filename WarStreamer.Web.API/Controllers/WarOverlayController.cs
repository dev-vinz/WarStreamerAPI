using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;

namespace WarStreamer.Web.API.Controllers
{
    [Route("waroverlays/")]
    public class WarOverlayController : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IUserMap _userMap;
        private readonly IWarOverlayMap _overlayMap;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public WarOverlayController(IUserMap userMap, IWarOverlayMap overlayMap)
        {
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
        public ActionResult<List<WarOverlayViewModel>> GetAll()
        {
            return Ok(_overlayMap.GetAll());
        }

        [HttpGet]
        [Route("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<WarOverlayViewModel>> GetAllByUserId(decimal userId)
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
            // Verifies if user exists
            if (_userMap.GetById(overlay.UserId) == null) return BadRequest(new { error = $"User with id '{overlay.UserId}' not found" });

            // Verifies if war overlay already exists
            if (_overlayMap.GetByUserIdAndId(overlay.UserId, overlay.Id) != null) return Conflict(new { error = $"War overlay with user id '{overlay.UserId}' and id '{overlay.Id}' already exists" });

            return Created($"~/waroverlays/{overlay.UserId}/{overlay.Id}", _overlayMap.Create(overlay));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPut]
        [Route("{userId}/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Update(decimal userId, int id, [FromBody] WarOverlayViewModel overlay)
        {
            // Verifies if war overlay exists
            if (_overlayMap.GetByUserIdAndId(userId, id) == null) return NotFound(new { error = $"War overlay with user id '{userId}' and id '{id}' not found" });

            // Creates a new user with id
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
        public ActionResult<bool> Delete(decimal userId, int id)
        {
            WarOverlayViewModel? overlay = _overlayMap.GetByUserIdAndId(userId, id);

            // Verifies overlay exists
            if (overlay == null) return NotFound(new { error = $"War overlay with user id '{userId}' and id '{id}' not found" });

            return Ok(_overlayMap.Delete(overlay));
        }
    }
}
