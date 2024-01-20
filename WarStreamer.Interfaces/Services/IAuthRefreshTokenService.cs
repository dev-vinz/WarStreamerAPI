using WarStreamer.Models;

namespace WarStreamer.Interfaces.Services
{
    public interface IAuthRefreshTokenService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
        public AuthRefreshToken Create(AuthRefreshToken domain);

        public bool Delete(AuthRefreshToken domain);

        public AuthRefreshToken? GetByUserId(Guid userId);

        public bool Update(AuthRefreshToken domain);
    }
}
