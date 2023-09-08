using System.ComponentModel.DataAnnotations;

namespace WarStreamer.Web.API.RequestModels
{
    public class TeamLogoRequestModel
    {
        [Required]
        public string TeamName { get; set; } = null!;

        [Required]
        public decimal UserId { get; set; }

        [Required]
        public IFormFile Logo { get; set; } = null!;

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }
    }
}
