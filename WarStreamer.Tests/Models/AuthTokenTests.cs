using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class AuthTokenTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private static readonly Guid USER_ID_ONE = Guid.Parse(
            "01e75c83-c6f5-4192-b57e-7427cec5560d"
        );
        private static readonly Guid USER_ID_TWO = Guid.Parse(
            "01e75c83-c6f5-4192-b57e-7427cec5560c"
        );
        private const string ACCESS_VALUE = "lkajevflu_wjrefhiuzg345-fwiurhv/=";
        private const string ACCESS_IV = "ahjber*çRFCER_EçFJNW=)%/(*";
        private const string DISCORD_VALUE = "lkajevflu_wjrefhiuzg345-faecferfw rtgvwiurhv/=";
        private const string DISCORD_IV = "ahjber*çRFCER_EçFJNç&%/*WHTBSFVW=)%/(*";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        public void WhenCreatingAuthToken_ThenAuthTokenReturned()
        {
            AuthToken authToken = CreateAuthTokenOne();

            Assert.NotNull(authToken);
            Assert.Equal(USER_ID_ONE, authToken.UserId);
            Assert.Equal(ACCESS_VALUE, authToken.AccessToken);
            Assert.Equal(ACCESS_IV, authToken.AccessIV);
            Assert.Equal(DISCORD_VALUE, authToken.DiscordToken);
            Assert.Equal(DISCORD_IV, authToken.DiscordIV);
            Assert.Equal(authToken.ExpiresAt, authToken.IssuedAt.AddMonths(4));
        }

        [Fact]
        public void WhenComparingSameAuthTokens_ThenAuthTokensAreTheSame()
        {
            AuthToken authTokenOne = CreateAuthTokenOne();
            AuthToken authTokenTwo = CreateAuthTokenOne();

            Assert.NotNull(authTokenOne);
            Assert.NotNull(authTokenTwo);

            Assert.True(authTokenOne == authTokenTwo);
            Assert.True(authTokenOne.Equals(authTokenTwo));
            Assert.True(authTokenTwo.Equals(authTokenOne));
            Assert.False(authTokenOne != authTokenTwo);
            Assert.Equal(authTokenOne.GetHashCode(), authTokenTwo.GetHashCode());
        }

        [Fact]
        public void WhenComparingDifferentAuthTokens_ThenAuthTokensAreDifferent()
        {
            AuthToken authTokenOne = CreateAuthTokenOne();
            AuthToken authTokenTwo = CreateAuthTokenTwo();

            Assert.NotNull(authTokenOne);
            Assert.NotNull(authTokenTwo);

            Assert.False(authTokenOne == authTokenTwo);
            Assert.False(authTokenOne.Equals(authTokenTwo));
            Assert.False(authTokenTwo.Equals(authTokenOne));
            Assert.True(authTokenOne != authTokenTwo);
            Assert.NotEqual(authTokenOne.GetHashCode(), authTokenTwo.GetHashCode());
        }

        [Fact]
        public void WhenCopyingAuthToken_ThenAuthTokenCopied()
        {
            AuthToken authToken = CreateAuthTokenOne();
            Entity copy = new AuthToken(authToken.UserId);

            Assert.NotNull(authToken);

            authToken.CopyTo(ref copy);
            AuthToken copyAuthToken = (AuthToken)copy;

            Assert.Equal(authToken.UserId, copyAuthToken.UserId);
            Assert.Equal(authToken.AccessToken, copyAuthToken.AccessToken);
            Assert.Equal(authToken.AccessIV, copyAuthToken.AccessIV);
            Assert.Equal(authToken.DiscordToken, copyAuthToken.DiscordToken);
            Assert.Equal(authToken.DiscordIV, copyAuthToken.DiscordIV);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static AuthToken CreateAuthTokenOne()
        {
            return new(USER_ID_ONE)
            {
                AccessToken = ACCESS_VALUE,
                AccessIV = ACCESS_IV,
                DiscordToken = DISCORD_VALUE,
                DiscordIV = DISCORD_IV,
            };
        }

        private static AuthToken CreateAuthTokenTwo()
        {
            return new(USER_ID_TWO)
            {
                AccessToken = DISCORD_IV,
                AccessIV = ACCESS_VALUE,
                DiscordToken = ACCESS_IV,
                DiscordIV = DISCORD_VALUE,
            };
        }
    }
}
