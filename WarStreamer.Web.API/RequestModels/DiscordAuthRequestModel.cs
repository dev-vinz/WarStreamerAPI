namespace WarStreamer.Web.API.RequestModels
{
    public class DiscordAuthRequestModel
    {
        public string Code { get; set; } = null!;

        public string CodeVerifier { get; set; } = null!;

        public string State { get; set; } = null!;
    }
}
