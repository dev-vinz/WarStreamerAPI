using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(Account domain);

        public List<Account> GetAll();

        public Account? GetByTag(string tag);

        public List<Account> GetByUserId(string userId);

        public Account Save(Account domain);
    }
}
