namespace WarStreamer.Web.API.ResponseModels
{
    public class TeamLogoResponseModel
    {
        public string TeamName { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public byte[] Logo { get; set; } = null!;

        public HashSet<string> ClanTags { get; set; } = [];
    }
}
