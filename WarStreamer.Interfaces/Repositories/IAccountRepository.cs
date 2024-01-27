using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(Account domain);

        public Account? GetByTag(string tag);

        public List<Account> GetByUserId(Guid userId);

        public Account Save(Account domain);
    }
}
