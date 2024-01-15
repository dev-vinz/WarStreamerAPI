using Microsoft.Extensions.DependencyInjection;
using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Tests.Tools;

namespace WarStreamer.Tests.Repositories
{
    [TestCaseOrderer("WarStreamer.Tests.Tools.OrderOrderer", "WarStreamer.Tests")]
    public class AccountRepositoryTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const string TAG = "#ABCDEFG";
        private static readonly Guid USER_ID = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560d");
        private static readonly Guid USER_ID_2 = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560e");

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IAccountRepository _repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AccountRepositoryTests()
        {
            _repository = new ServiceTestCollection()
                .BuildServiceProvider()
                .GetRequiredService<IAccountRepository>();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        [TestOrder(1)]
        public void WhenInsertAccount_ThenReturnsAddedAcount()
        {
            Account account = _repository.Save(CreateAccount());

            Assert.NotNull(account);

            Assert.Equal(TAG, account.Tag);
            Assert.Equal(USER_ID, account.UserId);
        }

        [Fact]
        [TestOrder(2)]
        public void WhenGetAllAccounts_ThenReturnsAccounts()
        {
            List<Account> accounts = _repository.GetAll();

            Account account = Assert.Single(accounts);

            Assert.Equal(TAG, account.Tag);
            Assert.Equal(USER_ID, account.UserId);
        }

        [Fact]
        [TestOrder(3)]
        public void WhenGetAllAccountsByUserId_ThenReturnsAccounts()
        {
            List<Account> accounts = _repository.GetByUserId(USER_ID);

            Assert.Single(accounts);

            Account account = accounts.Single();

            Assert.Equal(TAG, account.Tag);
            Assert.Equal(USER_ID, account.UserId);
        }

        [Fact]
        [TestOrder(4)]
        public void WhenGetAllAccountsByUserId_ThenReturnsEmpty()
        {
            Assert.Empty(_repository.GetByUserId(USER_ID_2));
        }

        [Fact]
        [TestOrder(5)]
        public void WhenGetAccountByTag_ThenReturnsAccount()
        {
            Account? account = _repository.GetByTag(TAG);

            Assert.NotNull(account);

            Assert.Equal(TAG, account.Tag);
            Assert.Equal(USER_ID, account.UserId);
        }

        [Fact]
        [TestOrder(6)]
        public void WhenGetAccountByTag_ThenReturnsNull()
        {
            Assert.Null(_repository.GetByTag(string.Empty));
        }

        [Fact]
        [TestOrder(7)]
        public void WhenDeleteAccount_ThenReturnsTrue()
        {
            Account? account = _repository.GetByTag(TAG);

            Assert.NotNull(account);
            Assert.True(_repository.Delete(account));
            Assert.Empty(_repository.GetAll());
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static Account CreateAccount()
        {
            return new(TAG, USER_ID);
        }
    }
}
