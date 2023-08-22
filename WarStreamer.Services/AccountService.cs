using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;

namespace WarStreamer.Services
{
    public class AccountService : IAccountService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IAccountRepository _repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public Account Create(Account domain)
        {
            return _repository.Save(domain);
        }

        public bool Delete(Account domain)
        {
            return _repository.Delete(domain);
        }

        public List<Account> GetAll()
        {
            return _repository.GetAll();
        }

        public Account? GetByTag(string tag)
        {
            return _repository.GetByTag(tag);
        }

        public List<Account> GetByUserId(decimal userId)
        {
            return _repository.GetByUserId(userId);
        }
    }
}
