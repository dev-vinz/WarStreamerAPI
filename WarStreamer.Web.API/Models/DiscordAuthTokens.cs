using Newtonsoft.Json;

namespace WarStreamer.Web.API.Authentication
{
    public class DiscordAuthTokens
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [JsonProperty("access_token")]
        public string AccessToken { get; private set; } = null!;

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; private set; } = null!;

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; private set; }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private DiscordAuthTokens() { }
    }
}
