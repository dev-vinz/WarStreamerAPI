using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class AuthTokenRepository(IWarStreamerContext context)
        : Repository(context),
            IAuthTokenRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(AuthToken domain)
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

        public AuthToken? GetByUserId(Guid userId)
        {
            try
            {
                return Context.Set<AuthToken>().FirstOrDefault(t => t.UserId == userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AuthToken Save(AuthToken domain)
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

        public bool Update(AuthToken domain)
        {
            try
            {
                Update<AuthToken>(domain);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
