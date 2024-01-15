using WarStreamer.Commons.Extensions;
using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class UserTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const string USER_ID_ONE = "1";
        private const string USER_ID_TWO = "2";
        private const string LANGUAGE_ID = "4";
        private const uint TIER_LEVEL = 3;
        private const bool NEWSLETTER = true;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        public void WhenCreatingUser_ThenUserReturned()
        {
            User user = CreateUserOne();

            Assert.NotNull(user);
            Assert.Equal(Guid.Empty.ParseDiscordId(USER_ID_ONE), user.Id);
            Assert.Equal(Guid.Empty.ParseDiscordId(LANGUAGE_ID), user.LanguageId);
            Assert.Equal(TIER_LEVEL, user.TierLevel);
            Assert.Equal(NEWSLETTER, user.NewsLetter);
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
            Entity copy = new User(user.Id);

            Assert.NotNull(user);

            user.CopyTo(ref copy);

            User copyUser = (User)copy;

            Assert.Equal(user.LanguageId, copyUser.LanguageId);
            Assert.Equal(user.TierLevel, copyUser.TierLevel);
            Assert.Equal(user.NewsLetter, copyUser.NewsLetter);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static User CreateUserOne()
        {
            return new(Guid.Empty.ParseDiscordId(USER_ID_ONE))
            {
                LanguageId = Guid.Empty.ParseDiscordId(LANGUAGE_ID),
                TierLevel = TIER_LEVEL,
                NewsLetter = NEWSLETTER,
            };
        }

        private static User CreateUserTwo()
        {
            return new(Guid.Empty.ParseDiscordId(USER_ID_TWO))
            {
                LanguageId = Guid.Empty.ParseDiscordId(TIER_LEVEL.ToString()),
                TierLevel = uint.Parse(LANGUAGE_ID),
                NewsLetter = !NEWSLETTER,
            };
        }
    }
}
