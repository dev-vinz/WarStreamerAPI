using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;

namespace WarStreamer.Services
{
    public class AuthTokenService(IAuthTokenRepository repository) : IAuthTokenService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IAuthTokenRepository _repository = repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AuthToken Create(AuthToken domain)
        {
            return _repository.Save(domain);
        }

        public bool Delete(AuthToken domain)
        {
            return _repository.Delete(domain);
        }

        public AuthToken? GetByUserId(Guid userId)
        {
            return _repository.GetByUserId(userId);
        }

        public bool Update(AuthToken domain)
        {
            return _repository.Update(domain);
        }
    }
}
