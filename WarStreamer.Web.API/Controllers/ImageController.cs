using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.RequestModels;
using WarStreamer.Web.API.ResponseModels;

namespace WarStreamer.Web.API.Controllers
{
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
        public ActionResult<List<ImageResponseModel>> GetAll()
        {
            List<ImageResponseModel> result = _imageMap
                .GetAll()
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

            return Ok(result);
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ImageResponseModel> Create([FromForm] ImageRequestModel imageRequest)
        {
            // Verify if the OverlaySetting exists
            if (_overlaySettingMap.GetByUserId(imageRequest.OverlaySettingId) == null)
            {
                return BadRequest(
                    new
                    {
                        error = $"Overlay setting with id '{imageRequest.OverlaySettingId}' not found"
                    }
                );
            }

            // Verify if the Image already exists
            if (
                _imageMap.GetByOverlaySettingIdAndName(
                    imageRequest.OverlaySettingId,
                    imageRequest.Name
                ) != null
            )
            {
                return Conflict(
                    new { error = $"Image with name '{imageRequest.Name}' already exists" }
                );
            }

            // Try to save the image in wwwroot folder
            if (!TrySaveImage(imageRequest.Image, imageRequest.OverlaySettingId, imageRequest.Name))
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

            return Created(
                $"~/images/{createdImage.OverlaySettingId}/{createdImage.Name}",
                imageResponse
            );
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPut]
        [Route("{overlaySettingId}/{name}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> Update(
            string overlaySettingId,
            string name,
            [FromForm] ImageRequestModel imageRequest
        )
        {
            // Verify if the image exists
            if (_imageMap.GetByOverlaySettingIdAndName(overlaySettingId, name) == null)
            {
                return NotFound(
                    new
                    {
                        error = $"Image with overlay setting id '{overlaySettingId}' and name '{name}' not found"
                    }
                );
            }

            // Try to update the image in wwwroot folder
            if (!TrySaveImage(imageRequest.Image, overlaySettingId, name))
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { error = "Cannot save image into server" }
                );
            }

            // Create a new image viewmodel
            ImageViewModel image =
                new(overlaySettingId, name)
                {
                    Location = new(imageRequest.LocationX, imageRequest.LocationY),
                    Width = imageRequest.Width,
                    Height = imageRequest.Height,
                };

            return Ok(_imageMap.Update(image));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */

        [HttpDelete]
        [Route("{overlaySettingId}/{name}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Delete(string overlaySettingId, string name)
        {
            ImageViewModel? image = _imageMap.GetByOverlaySettingIdAndName(overlaySettingId, name);

            // Verify if the image exists
            if (image == null)
            {
                return NotFound(
                    new
                    {
                        error = $"Image with overlay setting id '{overlaySettingId}' and name '{name}' not found"
                    }
                );
            }

            // Delete the image in wwwroot folder
            string path = $@"{_environment.WebRootPath}\{overlaySettingId}\{RELATIVE_PATH}";

            if (System.IO.File.Exists($@"{path}\{name.ToUpper()}.png"))
            {
                System.IO.File.Delete($@"{path}\{name.ToUpper()}.png");
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
