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
    [Route("images/")]
    public class ImageController(
        IWebHostEnvironment environment,
        IImageMap imageMap,
        IOverlaySettingMap overlaySettingMap
    ) : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private static readonly string CONTENT_TYPE = "image/png";
        private static readonly string RELATIVE_PATH = "Images";

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
        public ActionResult<List<ImageResponseModel>> Get()
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            List<ImageResponseModel> result = _imageMap
                .GetByOverlaySettingId(userId)
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
        public ActionResult<ImageResponseModel> GetByName(string name)
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            ImageViewModel? image = _imageMap.GetByOverlaySettingIdAndName(userId, name);

            // Verify it exists
            if (image == null)
            {
                return NotFound(new { error = $"Image with name '{name}' not found" });
            }

            // Try get image
            if (!TryGetImage(image.OverlaySettingId, image.Name, out byte[] imageData))
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { error = "Cannot get image from server" }
                );
            }

            // Create full response with image in body
            ImageResponseModel imageResponse =
                new()
                {
                    OverlaySettingId = image.OverlaySettingId,
                    Name = image.Name,
                    Image = imageData,
                    LocationX = image.Location.X,
                    LocationY = image.Location.Y,
                    Width = image.Width,
                    Height = image.Height,
                };

            return Ok(imageResponse);
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ImageResponseModel> Create([FromForm] ImageRequestModel imageRequest)
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();
            string userGuid = User.GetDiscordIdAsGuid().ToString();

            // Ensure both user ids are the same
            if (imageRequest.OverlaySettingId != userId)
            {
                return Forbid();
            }

            // Verify if the OverlaySetting exists
            if (_overlaySettingMap.GetByUserId(userId) == null)
            {
                return BadRequest(new { error = "Overlay setting not defined" });
            }

            // Verify if the Image already exists
            if (_imageMap.GetByOverlaySettingIdAndName(userId, imageRequest.Name) != null)
            {
                return Conflict(
                    new { error = $"Image with name '{imageRequest.Name}' already exists" }
                );
            }

            // Try to save the image in wwwroot folder
            if (!TrySaveImage(imageRequest.Image, userGuid, imageRequest.Name))
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { error = "Cannot save image into server" }
                );
            }

            // Create a new image viewmodel to save
            ImageViewModel image =
                new(imageRequest.OverlaySettingId, imageRequest.Name)
                {
                    Location = new(imageRequest.LocationX, imageRequest.LocationY),
                    Width = imageRequest.Width,
                    Height = imageRequest.Height,
                };

            // Get the created image
            ImageViewModel createdImage = _imageMap.Create(image);

            // Try to recover the image saved on wwwroot folder
            if (
                !TryGetImage(createdImage.OverlaySettingId, createdImage.Name, out byte[] imageData)
            )
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { error = "Cannot get image from server" }
                );
            }

            // Create full response with image in body
            ImageResponseModel imageResponse =
                new()
                {
                    OverlaySettingId = createdImage.OverlaySettingId,
                    Name = createdImage.Name,
                    Image = imageData,
                    LocationX = createdImage.Location.X,
                    LocationY = createdImage.Location.Y,
                    Width = createdImage.Width,
                    Height = createdImage.Height,
                };

            return Created($"~/images/{createdImage.Name}", imageResponse);
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> Update(string name, [FromForm] ImageRequestModel imageRequest)
        {
            // Get user id from JWT authorization
            string userId = User.GetDiscordId();

            // Ensure both user ids are the same
            if (imageRequest.OverlaySettingId != userId)
            {
                return Forbid();
            }

            ImageViewModel? anyImage = _imageMap.GetByOverlaySettingIdAndName(userId, name);

            // Verify if the image exists
            if (anyImage == null)
            {
                return NotFound(new { error = $"Image with name '{name}' not found" });
            }

            // Try to update the image in wwwroot folder
            if (!TrySaveImage(imageRequest.Image, anyImage.OverlaySettingId, anyImage.Name))
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { error = "Cannot save image into server" }
                );
            }

            // Update the image
            anyImage.Location = new(imageRequest.LocationX, imageRequest.LocationY);
            anyImage.Width = imageRequest.Width;
            anyImage.Height = imageRequest.Height;

            return Ok(_imageMap.Update(anyImage));
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

            ImageViewModel? image = _imageMap.GetByOverlaySettingIdAndName(userId, name);

            // Verify if the image exists
            if (image == null)
            {
                return NotFound(new { error = $"Image with name '{name}' not found" });
            }

            // Delete the image in wwwroot folder
            string path = $@"{_environment.WebRootPath}\{image.OverlaySettingId}\{RELATIVE_PATH}";

            if (System.IO.File.Exists($@"{path}\{image.Name}.png"))
            {
                System.IO.File.Delete($@"{path}\{image.Name}.png");
            }

            return Ok(_imageMap.Delete(image));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        public static byte[] GetImage(
            IWebHostEnvironment environment,
            string overlaySettingId,
            string name
        )
        {
            TryGetImage(environment, overlaySettingId, name, out byte[] image);

            return image;
        }

        public static bool TryGetImage(
            IWebHostEnvironment environment,
            string overlaySettingId,
            string name,
            out byte[] image
        )
        {
            // Default image
            image = null!;

            string path = $@"{environment.WebRootPath}\{overlaySettingId}\{RELATIVE_PATH}";

            if (!Directory.Exists(path))
            {
                return false;
            }

            try
            {
                using FileStream filestream = new($@"{path}\{name.ToUpper()}.png", FileMode.Open);
                using MemoryStream stream = new();

                filestream.CopyTo(stream);
                image = stream.ToArray();

                return true;
            }
            catch (FileNotFoundException) { }

            return false;
        }

        public static bool TrySaveImage(
            IWebHostEnvironment environment,
            IFormFile? file,
            string overlaySettingId,
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

            string path = $@"{environment.WebRootPath}\{overlaySettingId}\{RELATIVE_PATH}";

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

        private byte[] GetImage(string overlaySettingId, string name)
        {
            return GetImage(_environment, overlaySettingId, name);
        }

        private ImageResponseModel ToResponseModel(ImageViewModel image)
        {
            return new ImageResponseModel
            {
                OverlaySettingId = image.OverlaySettingId,
                Name = image.Name,
                Image = GetImage(image.OverlaySettingId, image.Name),
                LocationX = image.Location.X,
                LocationY = image.Location.Y,
                Width = image.Width,
                Height = image.Height
            };
        }

        private bool TryGetImage(string overlaySettingId, string name, out byte[] image)
        {
            return TryGetImage(_environment, overlaySettingId, name, out image);
        }

        private bool TrySaveImage(IFormFile? file, string overlaySettingId, string name)
        {
            return TrySaveImage(_environment, file, overlaySettingId, name);
        }
    }
}
