namespace WarStreamer.ViewModels
{
    public class AuthRefreshTokenViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _userId;
        private string _tokenValue;
        private string _aesInitializationVector;
        private readonly DateTimeOffset _issuedAt;
        private readonly DateTimeOffset _expiresAt;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string UserId
        {
            get => _userId;
        }

        public string TokenValue
        {
            get => _tokenValue;
            set => _tokenValue = value;
        }

        public string AesInitializationVector
        {
            get => _aesInitializationVector;
            set => _aesInitializationVector = value;
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

        public AuthRefreshTokenViewModel(
            string userId,
            DateTimeOffset issuedAt,
            DateTimeOffset expiresAt
        )
        {
            // Inputs
            {
                _userId = userId;
                _issuedAt = issuedAt;
                _expiresAt = expiresAt;
            }

            // Outputs
            {
                _tokenValue = string.Empty;
                _aesInitializationVector = string.Empty;
            }
        }

        public AuthRefreshTokenViewModel(string userId)
            : this(userId, DateTimeOffset.MinValue, DateTimeOffset.MinValue) { }
    }
}
