using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class AuthRefreshTokenRepository(IWarStreamerContext context)
        : Repository(context),
            IAuthRefreshTokenRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(AuthRefreshToken domain)
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

        public AuthRefreshToken? GetByUserId(Guid userId)
        {
            try
            {
                return Context.Set<AuthRefreshToken>().FirstOrDefault(t => t.UserId == userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AuthRefreshToken Save(AuthRefreshToken domain)
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

        public bool Update(AuthRefreshToken domain)
        {
            try
            {
                Update<AuthRefreshToken>(domain);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
