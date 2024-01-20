using WarStreamer.Commons.Extensions;
using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class AuthRefreshTokenMap(IAuthRefreshTokenService service) : IAuthRefreshTokenMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IAuthRefreshTokenService _service = service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AuthRefreshTokenViewModel Create(AuthRefreshTokenViewModel viewModel)
        {
            AuthRefreshToken authToken = ViewModelToDomain(viewModel);
            return DomainToViewModel(_service.Create(authToken));
        }

        public bool Delete(AuthRefreshTokenViewModel viewModel)
        {
            AuthRefreshToken authToken = ViewModelToDomain(viewModel, Guid.Parse(viewModel.UserId));
            return _service.Delete(authToken);
        }

        public AuthRefreshTokenViewModel? GetByUserId(string userId)
        {
            Guid guid = Guid.Empty.ParseDiscordId(userId);
            AuthRefreshToken? authToken = _service.GetByUserId(guid);

            return authToken != null ? DomainToViewModel(authToken) : null;
        }

        public bool Update(AuthRefreshTokenViewModel viewModel)
        {
            AuthRefreshToken authToken = ViewModelToDomain(viewModel, Guid.Parse(viewModel.UserId));
            return _service.Update(authToken);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static AuthRefreshTokenViewModel DomainToViewModel(AuthRefreshToken domain)
        {
            return new(domain.UserId.ToString(), domain.IssuedAt, domain.ExpiresAt)
            {
                TokenValue = domain.TokenValue,
                AesInitializationVector = domain.AesInitializationVector
            };
        }

        private static AuthRefreshToken ViewModelToDomain(
            AuthRefreshTokenViewModel viewModel,
            Guid userId
        )
        {
            return new(userId)
            {
                TokenValue = viewModel.TokenValue,
                AesInitializationVector = viewModel.AesInitializationVector
            };
        }

        private static AuthRefreshToken ViewModelToDomain(AuthRefreshTokenViewModel viewModel)
        {
            return ViewModelToDomain(viewModel, Guid.Empty.ParseDiscordId(viewModel.UserId));
        }
    }
}
