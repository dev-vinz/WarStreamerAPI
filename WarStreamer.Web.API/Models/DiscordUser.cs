using Newtonsoft.Json;

namespace WarStreamer.Web.API.Models
{
    public class DiscordUser
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [JsonProperty("id")]
        public string Id { get; private set; } = null!;

        [JsonProperty("username")]
        public string Username { get; private set; } = null!;

        [JsonProperty("global_name")]
        public string? GlobalName { get; private set; }

        [JsonProperty("avatar")]
        private string AvatarHash { get; set; } = null!;

        [JsonProperty("email")]
        public string? Email { get; private set; }

        /* * * * * * * * * * * * * * * * * *\
        |*            SHORTCUTS            *|
        \* * * * * * * * * * * * * * * * * */

        public string AvatarUrl => $"https://cdn.discordapp.com/avatars/{Id}/{AvatarHash}.png";
    }
}
