using WarStreamer.ViewModels;

namespace WarStreamer.Interfaces.Maps
{
    public interface IWarOverlayMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public WarOverlayViewModel Create(WarOverlayViewModel viewModel);

        public bool Delete(WarOverlayViewModel viewModel);

        public List<WarOverlayViewModel> GetByUserId(string userId);

        public WarOverlayViewModel? GetByUserIdAndId(string userId, Guid id);

        public bool Update(WarOverlayViewModel viewModel);
    }
}
