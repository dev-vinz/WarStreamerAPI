using WarStreamer.ViewModels;

namespace WarStreamer.Interfaces.Maps
{
    public interface IImageMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public ImageViewModel Create(ImageViewModel viewModel);

        public bool Delete(ImageViewModel viewModel);

        public ImageViewModel? GetByOverlaySettingIdAndName(string overlaySettingId, string name);

        public List<ImageViewModel> GetByOverlaySettingId(string overlaySettingId);

        public bool Update(ImageViewModel viewModel);
    }
}
