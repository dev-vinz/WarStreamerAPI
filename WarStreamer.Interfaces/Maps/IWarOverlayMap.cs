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

        public List<WarOverlayViewModel> GetAll();

        public List<WarOverlayViewModel> GetByUserId(decimal userId);

        public bool Update(WarOverlayViewModel viewModel);
    }
}
