using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class AccountRepository(IWarStreamerContext context)
        : Repository(context),
            IAccountRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(Account domain)
        {
            try
            {
                Remove(domain);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Account> GetAll()
        {
            try
            {
                return [.. Context.Set<Account>()];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Account? GetByTag(string tag)
        {
            try
            {
                return Context.Set<Account>().FirstOrDefault(a => a.Tag == tag);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Account> GetByUserId(decimal userId)
        {
            try
            {
                return
                [
                    .. Context.Set<Account>()
                              .Where(a => a.UserId == userId),
                ];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Account Save(Account domain)
        {
            try
            {
                Insert(domain);

                return domain;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
