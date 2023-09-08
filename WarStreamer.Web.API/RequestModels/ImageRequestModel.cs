using System.ComponentModel.DataAnnotations;

namespace WarStreamer.Web.API.RequestModels
{
    public class ImageRequestModel
    {
        [Required]
        public decimal OverlaySettingId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public IFormFile Image { get; set; } = null!;

        [Required]
        public int LocationX { get; set; }

        [Required]
        public int LocationY { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }
    }
}
