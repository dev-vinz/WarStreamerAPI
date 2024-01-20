namespace WarStreamer.Web.API.ResponseModels
{
    public class TokenResponseModel
    {
        public string AccessToken { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;
    }
}
