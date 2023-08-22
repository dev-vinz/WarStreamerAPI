using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class UserTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const decimal USER_ID_ONE = 1;
        private const decimal USER_ID_TWO = 2;
        private const int LANGUAGE_ID = 1;
        private const uint TIER_LEVEL = 3;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        public void WhenCreatingUser_ThenUserReturned()
        {
            User user = CreateUserOne();

            Assert.NotNull(user);
            Assert.Equal(USER_ID_ONE, user.Id);
            Assert.Equal(LANGUAGE_ID, user.LanguageId);
            Assert.Equal(TIER_LEVEL, user.TierLevel);
        }

        [Fact]
        public void WhenComparingSameUsers_ThenUsersAreTheSame()
        {
            User userOne = CreateUserOne();
            User userTwo = CreateUserOne();

            Assert.NotNull(userOne);
            Assert.NotNull(userTwo);

            Assert.True(userOne == userTwo);
            Assert.True(userOne.Equals(userTwo));
            Assert.True(userTwo.Equals(userOne));
            Assert.False(userOne != userTwo);
            Assert.Equal(userOne.GetHashCode(), userTwo.GetHashCode());
        }

        [Fact]
        public void WhenComparingDifferentUsers_ThenUsersAreDifferent()
        {
            User userOne = CreateUserOne();
            User userTwo = CreateUserTwo();

            Assert.NotNull(userOne);
            Assert.NotNull(userTwo);

            Assert.False(userOne == userTwo);
            Assert.False(userOne.Equals(userTwo));
            Assert.False(userTwo.Equals(userOne));
            Assert.True(userOne != userTwo);
            Assert.NotEqual(userOne.GetHashCode(), userTwo.GetHashCode());
        }

        [Fact]
        public void WhenCopyingUser_ThenUserCopied()
        {
            User user = CreateUserOne();
            Entity copy = new User();

            Assert.NotNull(user);

            user.CopyTo(ref copy);

            User copyUser = (User)copy;

            Assert.Equal(user.LanguageId, copyUser.LanguageId);
            Assert.Equal(user.TierLevel, copyUser.TierLevel);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static User CreateUserOne()
        {
            return new User
            {
                Id = USER_ID_ONE,
                LanguageId = LANGUAGE_ID,
                TierLevel = TIER_LEVEL,
            };
        }

        private static User CreateUserTwo()
        {
            return new User
            {
                Id = USER_ID_TWO,
                LanguageId = (int)TIER_LEVEL,
                TierLevel = LANGUAGE_ID
            };
        }
    }
}
