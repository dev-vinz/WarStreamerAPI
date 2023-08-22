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

        private const decimal USER_ID = 0;
        private const int LANGUAGE_ID = 1;
        private const int LANGUAGE_ID_UPDATED = 0;
        private const uint TIER_LEVEL = 3;
        private const uint TIER_LEVEL_UPDATED = 1;

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
            Assert.NotEqual(DateTimeOffset.MinValue, user.CreatedAt);
            Assert.Equal(user.CreatedAt, user.UpdatedAt);
        }

        [Fact]
        [TestOrder(2)]
        public void WhenGetAllUsers_ThenReturnsUsers()
        {
            List<User> users = _repository.GetAll();

            Assert.Single(users);

            User user = users.Single();

            Assert.Equal(USER_ID, user.Id);
            Assert.Equal(LANGUAGE_ID, user.LanguageId);
            Assert.Equal(TIER_LEVEL, user.TierLevel);
            Assert.NotEqual(DateTimeOffset.MinValue, user.CreatedAt);
            Assert.Equal(user.CreatedAt, user.UpdatedAt);
        }

        [Fact]
        [TestOrder(3)]
        public void WhenGetUserById_ThenReturnsNull()
        {
            User? user = _repository.GetById(USER_ID + 1);

            Assert.Null(user);
        }

        [Fact]
        [TestOrder(4)]
        public void WhenUpdateUser_ThenReturnsTrue()
        {
            User? user = _repository.GetById(USER_ID);

            Assert.NotNull(user);

            user.LanguageId = LANGUAGE_ID_UPDATED;
            user.TierLevel = TIER_LEVEL_UPDATED;

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
            Assert.NotEqual(DateTimeOffset.MinValue, user.CreatedAt);
            Assert.NotEqual(DateTimeOffset.MinValue, user.UpdatedAt);
            Assert.NotEqual(user.CreatedAt, user.UpdatedAt);
        }

        [Fact]
        [TestOrder(6)]
        public void WhenDeleteUser_ThenReturnsTrue()
        {
            User? user = _repository.GetById(USER_ID);

            Assert.NotNull(user);
            Assert.True(_repository.Delete(user));
            Assert.Empty(_repository.GetAll());
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static User CreateUser()
        {
            return new User
            {
                Id = USER_ID,
                LanguageId = LANGUAGE_ID,
                TierLevel = TIER_LEVEL,
            };
        }
    }
}
