using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IAuthTokenRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(AuthToken domain);

        public AuthToken? GetByUserId(Guid userId);

        public AuthToken Save(AuthToken domain);

        public bool Update(AuthToken domain);
    }
}
