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

        public List<ImageViewModel> GetAll();

        public ImageViewModel? GetById(int id);

        public List<ImageViewModel> GetByOverlaySettingId(decimal overlaySettingId);

        public bool Update(ImageViewModel viewModel);
    }
}
