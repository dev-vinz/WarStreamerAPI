using WarStreamer.Commons.Extensions;
using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class AuthTokenMap(IAuthTokenService service) : IAuthTokenMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IAuthTokenService _service = service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AuthTokenViewModel Create(AuthTokenViewModel viewModel)
        {
            AuthToken authToken = ViewModelToDomain(viewModel);
            return DomainToViewModel(_service.Create(authToken));
        }

        public bool Delete(AuthTokenViewModel viewModel)
        {
            AuthToken authToken = ViewModelToDomain(viewModel, Guid.Parse(viewModel.UserId));
            return _service.Delete(authToken);
        }

        public AuthTokenViewModel? GetByUserId(string userId)
        {
            Guid guid = Guid.Empty.ParseDiscordId(userId);
            AuthToken? authToken = _service.GetByUserId(guid);

            return authToken != null ? DomainToViewModel(authToken) : null;
        }

        public bool Update(AuthTokenViewModel viewModel)
        {
            AuthToken authToken = ViewModelToDomain(viewModel, Guid.Parse(viewModel.UserId));
            return _service.Update(authToken);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static AuthTokenViewModel DomainToViewModel(AuthToken domain)
        {
            return new(domain.UserId.ToString(), domain.IssuedAt, domain.ExpiresAt)
            {
                AccessToken = domain.AccessToken,
                AccessIV = domain.AccessIV,
                DiscordToken = domain.DiscordToken,
                DiscordIV = domain.DiscordIV,
            };
        }

        private static AuthToken ViewModelToDomain(AuthTokenViewModel viewModel, Guid userId)
        {
            return new(userId)
            {
                AccessToken = viewModel.AccessToken,
                AccessIV = viewModel.AccessIV,
                DiscordToken = viewModel.DiscordToken,
                DiscordIV = viewModel.DiscordIV,
            };
        }

        private static AuthToken ViewModelToDomain(AuthTokenViewModel viewModel)
        {
            return ViewModelToDomain(viewModel, Guid.Empty.ParseDiscordId(viewModel.UserId));
        }
    }
}
