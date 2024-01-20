using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;

namespace WarStreamer.Services
{
    public class AuthRefreshTokenService(IAuthRefreshTokenRepository repository)
        : IAuthRefreshTokenService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IAuthRefreshTokenRepository _repository = repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AuthRefreshToken Create(AuthRefreshToken domain)
        {
            return _repository.Save(domain);
        }

        public bool Delete(AuthRefreshToken domain)
        {
            return _repository.Delete(domain);
        }

        public AuthRefreshToken? GetByUserId(Guid userId)
        {
            return _repository.GetByUserId(userId);
        }

        public bool Update(AuthRefreshToken domain)
        {
            return _repository.Update(domain);
        }
    }
}
