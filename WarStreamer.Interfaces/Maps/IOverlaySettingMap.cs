using WarStreamer.ViewModels;

namespace WarStreamer.Interfaces.Maps
{
    public interface IOverlaySettingMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public OverlaySettingViewModel Create(OverlaySettingViewModel viewModel);

        public bool Delete(OverlaySettingViewModel viewModel);

        public OverlaySettingViewModel? GetByUserId(string userId);

        public bool Update(OverlaySettingViewModel viewModel);
    }
}
