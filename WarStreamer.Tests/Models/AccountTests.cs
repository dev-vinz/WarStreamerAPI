using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class AccountTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const decimal USER_ID = 0;
        private const string TAG_ONE = "#ABCDEFG";
        private const string TAG_TWO = "#ABCDEF";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        public void WhenCreatingAccount_ThenAccountReturned()
        {
            Account account = CreateAccountOne();

            Assert.NotNull(account);
            Assert.Equal(USER_ID, account.UserId);
            Assert.Equal(TAG_ONE, account.Tag);
        }

        [Fact]
        public void WhenComparingSameAccounts_ThenAccountsAreTheSame()
        {
            Account accountOne = CreateAccountOne();
            Account accountTwo = CreateAccountOne();

            Assert.NotNull(accountOne);
            Assert.NotNull(accountTwo);

            Assert.True(accountOne == accountTwo);
            Assert.True(accountOne.Equals(accountTwo));
            Assert.True(accountTwo.Equals(accountOne));
            Assert.False(accountOne != accountTwo);
            Assert.Equal(accountOne.GetHashCode(), accountTwo.GetHashCode());
        }

        [Fact]
        public void WhenComparingDifferentAccounts_ThenAccountsAreDifferent()
        {
            Account accountOne = CreateAccountOne();
            Account accountTwo = CreateAccountTwo();

            Assert.NotNull(accountOne);
            Assert.NotNull(accountTwo);

            Assert.False(accountOne == accountTwo);
            Assert.False(accountOne.Equals(accountTwo));
            Assert.False(accountTwo.Equals(accountOne));
            Assert.True(accountOne != accountTwo);
            Assert.NotEqual(accountOne.GetHashCode(), accountTwo.GetHashCode());
        }

        [Fact]
        public void WhenCopyingAccount_ThenThrowError()
        {
            Account account = CreateAccountOne();
            Entity copy = new Account(account.Tag, account.UserId);

            Assert.NotNull(account);

            Assert.Throws<InvalidOperationException>(() => account.CopyTo(ref copy));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static Account CreateAccountOne()
        {
            return new(TAG_ONE, USER_ID);
        }

        private static Account CreateAccountTwo()
        {
            return new(TAG_TWO, USER_ID);
        }
    }
}
