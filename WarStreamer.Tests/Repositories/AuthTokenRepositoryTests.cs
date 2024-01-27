using Microsoft.Extensions.DependencyInjection;
using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Tests.Tools;

namespace WarStreamer.Tests.Repositories
{
    [TestCaseOrderer("WarStreamer.Tests.Tools.OrderOrderer", "WarStreamer.Tests")]
    public class AuthTokenRepositoryTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private static readonly Guid USER_ID = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560d");
        private static readonly Guid USER_ID_2 = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560e");
        private const string ACCESS_VALUE = "lkajevflu_wjrefhiuzg345-fwiurhv/=";
        private const string ACCESS_IV = "ahjber*çRFCER_EçFJNW=)%/(*";
        private const string DISCORD_VALUE = "lkajevflu_wjrefhiuzg345-faecferfw rtgvwiurhv/=";
        private const string DISCORD_IV = "ahjber*çRFCER_EçFJNç&%/*WHTBSFVW=)%/(*";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IAuthTokenRepository _repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AuthTokenRepositoryTests()
        {
            _repository = new ServiceTestCollection()
                .BuildServiceProvider()
                .GetRequiredService<IAuthTokenRepository>();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        [TestOrder(1)]
        public void WhenInsertAuthToken_ThenReturnsAddedAuthToken()
        {
            AuthToken authToken = _repository.Save(CreateAuthToken());

            Assert.NotNull(authToken);

            Assert.Equal(USER_ID, authToken.UserId);
            Assert.Equal(ACCESS_VALUE, authToken.AccessToken);
            Assert.Equal(ACCESS_IV, authToken.AccessIV);
            Assert.Equal(DISCORD_VALUE, authToken.DiscordToken);
            Assert.Equal(DISCORD_IV, authToken.DiscordIV);
            Assert.Equal(authToken.ExpiresAt, authToken.IssuedAt.AddMonths(4));
        }

        [Fact]
        [TestOrder(2)]
        public void WhenUpdateAuthToken_ThenReturnsTrue()
        {
            AuthToken? authToken = _repository.GetByUserId(USER_ID);

            Assert.NotNull(authToken);

            authToken.AccessToken = DISCORD_VALUE;
            authToken.DiscordIV = ACCESS_IV;

            Assert.True(_repository.Update(authToken));
        }

        [Fact]
        [TestOrder(3)]
        public void WhenGetAuthTokenByUserId_ThenReturnsAuthToken()
        {
            AuthToken? authToken = _repository.GetByUserId(USER_ID);

            Assert.NotNull(authToken);

            Assert.Equal(USER_ID, authToken.UserId);
            Assert.Equal(DISCORD_VALUE, authToken.AccessToken);
            Assert.Equal(ACCESS_IV, authToken.AccessIV);
            Assert.Equal(DISCORD_VALUE, authToken.DiscordToken);
            Assert.Equal(ACCESS_IV, authToken.DiscordIV);
            Assert.Equal(authToken.ExpiresAt, authToken.IssuedAt.AddMonths(4));
        }

        [Fact]
        [TestOrder(4)]
        public void WhenGetAuthTokenByUserId_ThenReturnsNull()
        {
            Assert.Null(_repository.GetByUserId(USER_ID_2));
        }

        [Fact]
        [TestOrder(5)]
        public void WhenDeleteAuthToken_ThenReturnsTrue()
        {
            AuthToken? authToken = _repository.GetByUserId(USER_ID);

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

        private static AuthToken CreateAuthToken()
        {
            return new(USER_ID)
            {
                AccessToken = ACCESS_VALUE,
                AccessIV = ACCESS_IV,
                DiscordToken = DISCORD_VALUE,
                DiscordIV = DISCORD_IV,
            };
        }
    }
}
