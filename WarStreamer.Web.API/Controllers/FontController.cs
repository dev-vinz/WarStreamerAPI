using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;

namespace WarStreamer.Web.API.Controllers
{
    [Authorize]
    [Route("fonts/")]
    public class FontController(IWebHostEnvironment environment, IFontMap fontMap) : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private static readonly string RELATIVE_PATH = "Fonts";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWebHostEnvironment _environment = environment;

        private readonly IFontMap _fontMap = fontMap;

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
        public ActionResult<List<FontViewModel>> GetAll()
        {
            return Ok(_fontMap.GetAll());
        }

        [HttpGet]
        [Route("{fontId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FontViewModel?> GetById(Guid fontId)
        {
            FontViewModel? font = _fontMap.GetById(fontId);

            // Verify if the font exists
            if (font == null)
            {
                return NotFound(new { error = $"Font with id '{fontId}' not found" });
            }

            return Ok(font);
        }

        [HttpGet]
        [Route("{fontId}/file")]
        [Produces("application/octet-stream")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetFontById(Guid fontId)
        {
            FontViewModel? font = _fontMap.GetById(fontId);

            // Verify if the font exists
            if (font == null)
            {
                return NotFound(new { error = $"Font with id '{fontId}' not found" });
            }

            // Recover file from wwwroot folder
            string path = $@"{_environment.WebRootPath}\{RELATIVE_PATH}";

            // Copy into a byte array
            using FileStream filestream = new($@"{path}\{font.FileName}", FileMode.Open);
            using MemoryStream stream = new();

            filestream.CopyTo(stream);
            byte[] bytes = stream.ToArray();

            return new FileContentResult(bytes, "application/octet-stream")
            {
                FileDownloadName = font.FileName
            };
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */
    }
}
