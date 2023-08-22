using WarStreamer.Models;

namespace WarStreamer.Interfaces.Services
{
    public interface IAccountService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public Account Create(Account domain);

        public bool Delete(Account domain);

        public List<Account> GetAll();

        public Account? GetByTag(string tag);

        public List<Account> GetByUserId(decimal userId);
    }
}
