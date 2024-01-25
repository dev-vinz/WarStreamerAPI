namespace WarStreamer.Web.API.Models
{
    public class JwtConfig
    {
        public string PrivateKey { get; set; } = null!;

        public string PublicKey { get; set; } = null!;

        public string KeyId { get; set; } = null!;

        public string Domain { get; set; } = null!;

        public string Audience { get; set; } = null!;
    }
}
