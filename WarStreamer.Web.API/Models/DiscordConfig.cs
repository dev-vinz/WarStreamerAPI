namespace WarStreamer.Web.API.Models
{
    public class DiscordConfig
    {
        public string ClientId { get; set; } = null!;

        public string ClientSecret { get; set; } = null!;

        public string RedirectUri { get; set; } = null!;
    }
}
