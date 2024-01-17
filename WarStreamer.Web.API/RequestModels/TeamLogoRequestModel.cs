using System.ComponentModel.DataAnnotations;

namespace WarStreamer.Web.API.RequestModels
{
    public class TeamLogoRequestModel
    {
        [Required]
        public string TeamName { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public IFormFile Logo { get; set; } = null!;

        [Required]
        public HashSet<string> ClanTags { get; set; } = [];
    }
}
