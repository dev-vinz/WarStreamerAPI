using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.ResponseModels;

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
        public ActionResult<List<FontResponseModel>> GetAll()
        {
            List<FontResponseModel> result = _fontMap.GetAll().Select(ToResponseModel).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("{fontId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FontResponseModel?> GetById(Guid fontId)
        {
            FontViewModel? font = _fontMap.GetById(fontId);

            // Verify if the font exists
            if (font == null)
            {
                return NotFound(new { error = $"Font with id '{fontId}' not found" });
            }

            return Ok(ToResponseModel(font));
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


        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        public static byte[] GetFont(IWebHostEnvironment environment, string fileName)
        {
            TryGetFont(environment, fileName, out byte[] font);

            return font;
        }

        public static bool TryGetFont(
            IWebHostEnvironment environment,
            string fileName,
            out byte[] font
        )
        {
            // Default font
            font = null!;

            // Recover file from wwwroot folder
            string path = $@"{environment.WebRootPath}\{RELATIVE_PATH}";

            if (!Directory.Exists(path))
            {
                return false;
            }

            try
            {
                // Copy into a byte array
                using FileStream filestream = new($@"{path}\{fileName}", FileMode.Open);
                using MemoryStream stream = new();

                filestream.CopyTo(stream);
                font = stream.ToArray();

                return true;
            }
            catch (FileNotFoundException) { }

            return false;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private byte[] GetFont(string fileName)
        {
            return GetFont(_environment, fileName);
        }

        private FontResponseModel ToResponseModel(FontViewModel font)
        {
            return new FontResponseModel
            {
                Id = font.Id,
                DisplayName = font.DisplayName,
                FamilyName = font.FamilyName,
                File = GetFont(font.FileName)
            };
        }
    }
}
