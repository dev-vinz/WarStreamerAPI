using WarStreamer.ViewModels;

namespace WarStreamer.Interfaces.Maps
{
    public interface IAuthRefreshTokenMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
        public AuthRefreshTokenViewModel Create(AuthRefreshTokenViewModel viewModel);

        public bool Delete(AuthRefreshTokenViewModel viewModel);

        public AuthRefreshTokenViewModel? GetByUserId(string userId);

        public bool Update(AuthRefreshTokenViewModel viewModel);
    }
}
