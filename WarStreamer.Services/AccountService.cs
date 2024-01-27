using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;

namespace WarStreamer.Services
{
    public class AccountService(IAccountRepository repository) : IAccountService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IAccountRepository _repository = repository;

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

        public Account? GetByTag(string tag)
        {
            return _repository.GetByTag(tag);
        }

        public List<Account> GetByUserId(Guid userId)
        {
            return _repository.GetByUserId(userId);
        }
    }
}
