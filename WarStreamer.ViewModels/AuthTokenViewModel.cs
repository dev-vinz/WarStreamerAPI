namespace WarStreamer.ViewModels
{
    public class AuthTokenViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _userId;
        private string _accessToken;
        private string _accessIV;
        private string _discordToken;
        private string _discordIV;
        private readonly DateTimeOffset _issuedAt;
        private readonly DateTimeOffset _expiresAt;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string UserId
        {
            get => _userId;
        }

        public string AccessToken
        {
            get => _accessToken;
            set => _accessToken = value;
        }

        public string AccessIV
        {
            get => _accessIV;
            set => _accessIV = value;
        }

        public string DiscordToken
        {
            get => _discordToken;
            set => _discordToken = value;
        }

        public string DiscordIV
        {
            get => _discordIV;
            set => _discordIV = value;
        }

        public DateTimeOffset IssuedAt
        {
            get => _issuedAt;
        }

        public DateTimeOffset ExpiresAt
        {
            get => _expiresAt;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AuthTokenViewModel(string userId, DateTimeOffset issuedAt, DateTimeOffset expiresAt)
        {
            // Inputs
            {
                _userId = userId;
                _issuedAt = issuedAt;
                _expiresAt = expiresAt;
            }

            // Outputs
            {
                _accessToken = string.Empty;
                _accessIV = string.Empty;
                _discordToken = string.Empty;
                _discordIV = string.Empty;
            }
        }

        public AuthTokenViewModel(string userId)
            : this(userId, DateTimeOffset.MinValue, DateTimeOffset.MinValue) { }
    }
}
