namespace WarStreamer.Web.API.ResponseModels
{
    public class FontResponseModel
    {
        public string Id { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public string FamilyName { get; set; } = null!;

        public byte[] File { get; set; } = null!;
    }
}
