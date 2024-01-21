using Microsoft.Extensions.DependencyInjection;
using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Tests.Tools;

namespace WarStreamer.Tests.Repositories
{
    [TestCaseOrderer("WarStreamer.Tests.Tools.OrderOrderer", "WarStreamer.Tests")]
    public class AuthRefreshTokenRepositoryTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private static readonly Guid USER_ID = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560d");
        private static readonly Guid USER_ID_2 = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560e");
        private const string TOKEN_VALUE = "lkajevflu_wjrefhiuzg345-fwiurhv/=";
        private const string TOKEN_VALUE_UPDATED = "lkajevflu_wjrefhiuzgaref345-fwiurhv/=";
        private const string INITIALIZATION_VECTOR = "ahjber*çRFCER_EçFJNW=)%/(*";
        private const string INITIALIZATION_VECTOR_UPDATED = "afrewhjber*çRFCER_EçFJNW=)%/(*";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IAuthRefreshTokenRepository _repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AuthRefreshTokenRepositoryTests()
        {
            _repository = new ServiceTestCollection()
                .BuildServiceProvider()
                .GetRequiredService<IAuthRefreshTokenRepository>();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        [TestOrder(1)]
        public void WhenInsertAuthRefreshToken_ThenReturnsAddedAuthRefreshToken()
        {
            AuthRefreshToken authToken = _repository.Save(CreateAuthRefreshToken());

            Assert.NotNull(authToken);

            Assert.Equal(USER_ID, authToken.UserId);
            Assert.Equal(TOKEN_VALUE, authToken.TokenValue);
            Assert.Equal(INITIALIZATION_VECTOR, authToken.AesInitializationVector);
            Assert.Equal(authToken.ExpiresAt, authToken.IssuedAt.AddMonths(1));
        }

        [Fact]
        [TestOrder(2)]
        public void WhenUpdateAuthRefreshToken_ThenReturnsTrue()
        {
            AuthRefreshToken? authToken = _repository.GetByUserId(USER_ID);

            Assert.NotNull(authToken);

            authToken.TokenValue = TOKEN_VALUE_UPDATED;
            authToken.AesInitializationVector = INITIALIZATION_VECTOR_UPDATED;

            Assert.True(_repository.Update(authToken));
        }

        [Fact]
        [TestOrder(3)]
        public void WhenGetAuthRefreshTokenByUserId_ThenReturnsAuthRefreshToken()
        {
            AuthRefreshToken? authToken = _repository.GetByUserId(USER_ID);

            Assert.NotNull(authToken);

            Assert.Equal(USER_ID, authToken.UserId);
            Assert.Equal(TOKEN_VALUE_UPDATED, authToken.TokenValue);
            Assert.Equal(INITIALIZATION_VECTOR_UPDATED, authToken.AesInitializationVector);
            Assert.Equal(authToken.ExpiresAt, authToken.IssuedAt.AddMonths(1));
        }

        [Fact]
        [TestOrder(4)]
        public void WhenGetAuthRefreshTokenByUserId_ThenReturnsNull()
        {
            Assert.Null(_repository.GetByUserId(USER_ID_2));
        }

        [Fact]
        [TestOrder(5)]
        public void WhenDeleteAuthRefreshToken_ThenReturnsTrue()
        {
            AuthRefreshToken? authToken = _repository.GetByUserId(USER_ID);

            Assert.NotNull(authToken);
            Assert.True(_repository.Delete(authToken));
            Assert.Null(_repository.GetByUserId(USER_ID));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static AuthRefreshToken CreateAuthRefreshToken()
        {
            return new(USER_ID)
            {
                TokenValue = TOKEN_VALUE,
                AesInitializationVector = INITIALIZATION_VECTOR,
            };
        }
    }
}
