using WarStreamer.ViewModels;

namespace WarStreamer.Interfaces.Maps
{
    public interface IAuthTokenMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
        public AuthTokenViewModel Create(AuthTokenViewModel viewModel);

        public bool Delete(AuthTokenViewModel viewModel);

        public AuthTokenViewModel? GetByUserId(string userId);

        public bool Update(AuthTokenViewModel viewModel);
    }
}
