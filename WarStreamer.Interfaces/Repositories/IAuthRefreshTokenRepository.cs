using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IAuthRefreshTokenRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(AuthRefreshToken domain);

        public AuthRefreshToken? GetByUserId(Guid userId);

        public AuthRefreshToken Save(AuthRefreshToken domain);

        public bool Update(AuthRefreshToken domain);
    }
}
