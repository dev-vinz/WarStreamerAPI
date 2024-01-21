using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class AuthRefreshTokenTests
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
        private const string TOKEN_VALUE = "lkajevflu_wjrefhiuzg345-fwiurhv/=";
        private const string INITIALIZATION_VECTOR = "ahjber*çRFCER_EçFJNW=)%/(*";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        public void WhenCreatingAuthRefreshToken_ThenAuthRefreshTokenReturned()
        {
            AuthRefreshToken authToken = CreateAuthRefreshTokenOne();

            Assert.NotNull(authToken);
            Assert.Equal(USER_ID_ONE, authToken.UserId);
            Assert.Equal(TOKEN_VALUE, authToken.TokenValue);
            Assert.Equal(INITIALIZATION_VECTOR, authToken.AesInitializationVector);
            Assert.Equal(authToken.ExpiresAt, authToken.IssuedAt.AddMonths(1));
        }

        [Fact]
        public void WhenComparingSameAuthRefreshTokens_ThenAuthRefreshTokensAreTheSame()
        {
            AuthRefreshToken authTokenOne = CreateAuthRefreshTokenOne();
            AuthRefreshToken authTokenTwo = CreateAuthRefreshTokenOne();

            Assert.NotNull(authTokenOne);
            Assert.NotNull(authTokenTwo);

            Assert.True(authTokenOne == authTokenTwo);
            Assert.True(authTokenOne.Equals(authTokenTwo));
            Assert.True(authTokenTwo.Equals(authTokenOne));
            Assert.False(authTokenOne != authTokenTwo);
            Assert.Equal(authTokenOne.GetHashCode(), authTokenTwo.GetHashCode());
        }

        [Fact]
        public void WhenComparingDifferentAuthRefreshTokens_ThenAuthRefreshTokensAreDifferent()
        {
            AuthRefreshToken authTokenOne = CreateAuthRefreshTokenOne();
            AuthRefreshToken authTokenTwo = CreateAuthRefreshTokenTwo();

            Assert.NotNull(authTokenOne);
            Assert.NotNull(authTokenTwo);

            Assert.False(authTokenOne == authTokenTwo);
            Assert.False(authTokenOne.Equals(authTokenTwo));
            Assert.False(authTokenTwo.Equals(authTokenOne));
            Assert.True(authTokenOne != authTokenTwo);
            Assert.NotEqual(authTokenOne.GetHashCode(), authTokenTwo.GetHashCode());
        }

        [Fact]
        public void WhenCopyingAuthRefreshToken_ThenAuthRefreshTokenCopied()
        {
            AuthRefreshToken authToken = CreateAuthRefreshTokenOne();
            Entity copy = new AuthRefreshToken(authToken.UserId);

            Assert.NotNull(authToken);

            authToken.CopyTo(ref copy);
            AuthRefreshToken copyAuthToken = (AuthRefreshToken)copy;

            Assert.Equal(authToken.UserId, copyAuthToken.UserId);
            Assert.Equal(authToken.TokenValue, copyAuthToken.TokenValue);
            Assert.Equal(authToken.AesInitializationVector, copyAuthToken.AesInitializationVector);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static AuthRefreshToken CreateAuthRefreshTokenOne()
        {
            return new(USER_ID_ONE)
            {
                TokenValue = TOKEN_VALUE,
                AesInitializationVector = INITIALIZATION_VECTOR,
            };
        }

        private static AuthRefreshToken CreateAuthRefreshTokenTwo()
        {
            return new(USER_ID_TWO)
            {
                TokenValue = INITIALIZATION_VECTOR,
                AesInitializationVector = TOKEN_VALUE
            };
        }
    }
}
