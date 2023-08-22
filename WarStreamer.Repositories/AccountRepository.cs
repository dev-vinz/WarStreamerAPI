using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class AccountRepository : Repository, IAccountRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AccountRepository(IWarStreamerContext context) : base(context) { }

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
                return Context.Set<Account>().ToList();
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
                return Context.Set<Account>()
                              .Where(a => a.UserId == userId)
                              .ToList();
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
                domain.CreatedAt = DateTimeOffset.UtcNow;
                domain.UpdatedAt = domain.CreatedAt;

                return Insert(domain);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
