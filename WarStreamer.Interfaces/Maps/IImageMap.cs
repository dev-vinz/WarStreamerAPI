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

        public List<ImageViewModel> GetByUserId(string userId);

        public ImageViewModel? GetByUserIdAndName(string userId, string name);

        public List<ImageViewModel> GetUsedByUserId(string userId);

        public bool Update(ImageViewModel viewModel);
    }
}
