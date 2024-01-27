using WarStreamer.Models;

namespace WarStreamer.Interfaces.Services
{
    public interface IAuthTokenService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
        public AuthToken Create(AuthToken domain);

        public bool Delete(AuthToken domain);

        public AuthToken? GetByUserId(Guid userId);

        public bool Update(AuthToken domain);
    }
}
