using Microsoft.Extensions.DependencyInjection;
using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Tests.Tools;

namespace WarStreamer.Tests.Repositories
{
    [TestCaseOrderer("WarStreamer.Tests.Tools.OrderOrderer", "WarStreamer.Tests")]
    public class UserRepositoryTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private static readonly Guid USER_ID = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560c");
        private static readonly Guid USER_ID_2 = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560d");
        private static readonly Guid LANGUAGE_ID = Guid.Parse(
            "01e75c83-c6f5-4192-b57e-7427cec5560e"
        );
        private static readonly Guid LANGUAGE_ID_UPDATED = Guid.Parse(
            "01e75c83-c6f5-4192-b57e-7427cec5560f"
        );
        private const uint TIER_LEVEL = 3;
        private const uint TIER_LEVEL_UPDATED = 1;
        private const bool NEWSLETTER = true;
        private const bool NEWSLETTER_UPDATED = false;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IUserRepository _repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public UserRepositoryTests()
        {
            _repository = new ServiceTestCollection()
                .BuildServiceProvider()
                .GetRequiredService<IUserRepository>();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        [TestOrder(1)]
        public void WhenInsertNewUser_ThenReturnsAddedUser()
        {
            User user = _repository.Save(CreateUser());

            Assert.NotNull(user);

            Assert.Equal(USER_ID, user.Id);
            Assert.Equal(LANGUAGE_ID, user.LanguageId);
            Assert.Equal(TIER_LEVEL, user.TierLevel);
            Assert.Equal(NEWSLETTER, user.NewsLetter);
        }

        [Fact]
        [TestOrder(2)]
        public void WhenGetUser_ThenReturnsUser()
        {
            User? user = _repository.GetById(USER_ID);

            Assert.NotNull(user);

            Assert.Equal(USER_ID, user.Id);
            Assert.Equal(LANGUAGE_ID, user.LanguageId);
            Assert.Equal(TIER_LEVEL, user.TierLevel);
            Assert.Equal(NEWSLETTER, user.NewsLetter);
        }

        [Fact]
        [TestOrder(3)]
        public void WhenGetUserById_ThenReturnsNull()
        {
            Assert.Null(_repository.GetById(USER_ID_2));
        }

        [Fact]
        [TestOrder(4)]
        public void WhenUpdateUser_ThenReturnsTrue()
        {
            User? user = _repository.GetById(USER_ID);

            Assert.NotNull(user);

            user.LanguageId = LANGUAGE_ID_UPDATED;
            user.TierLevel = TIER_LEVEL_UPDATED;
            user.NewsLetter = NEWSLETTER_UPDATED;

            Assert.True(_repository.Update(user));
        }

        [Fact]
        [TestOrder(5)]
        public void WhenGetUserById_ThenReturnsUser()
        {
            User? user = _repository.GetById(USER_ID);

            Assert.NotNull(user);
            Assert.Equal(USER_ID, user.Id);
            Assert.Equal(LANGUAGE_ID_UPDATED, user.LanguageId);
            Assert.Equal(TIER_LEVEL_UPDATED, user.TierLevel);
            Assert.Equal(NEWSLETTER_UPDATED, user.NewsLetter);
        }

        [Fact]
        [TestOrder(6)]
        public void WhenDeleteUser_ThenReturnsTrue()
        {
            User? user = _repository.GetById(USER_ID);

            Assert.NotNull(user);
            Assert.True(_repository.Delete(user));
            Assert.Null(_repository.GetById(USER_ID));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static User CreateUser()
        {
            return new(USER_ID)
            {
                LanguageId = LANGUAGE_ID,
                TierLevel = TIER_LEVEL,
                NewsLetter = NEWSLETTER,
            };
        }
    }
}
