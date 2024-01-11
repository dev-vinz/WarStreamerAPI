using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class WarOverlayMap(IWarOverlayService service) : IWarOverlayMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWarOverlayService _service = service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public WarOverlayViewModel Create(WarOverlayViewModel viewModel)
        {
            WarOverlay overlay = ViewModelToDomain(viewModel);
            return DomainToViewModel(_service.Create(overlay));
        }

        public bool Delete(WarOverlayViewModel viewModel)
        {
            WarOverlay overlay = ViewModelToDomain(viewModel);
            return _service.Delete(overlay);
        }

        public List<WarOverlayViewModel> GetAll()
        {
            return DomainToViewModel(_service.GetAll());
        }

        public List<WarOverlayViewModel> GetByUserId(string userId)
        {
            return DomainToViewModel(_service.GetByUserId(userId));
        }

        public WarOverlayViewModel? GetByUserIdAndId(string userId, int id)
        {
            WarOverlay? overlay = _service.GetByUserIdAndId(userId, id);
            return overlay != null ? DomainToViewModel(overlay) : null;
        }

        public bool Update(WarOverlayViewModel viewModel)
        {
            WarOverlay overlay = ViewModelToDomain(viewModel);
            return _service.Update(overlay);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static WarOverlayViewModel DomainToViewModel(WarOverlay domain)
        {
            return new(domain.UserId, domain.Id, domain.ClanTag)
            {
                LastCheckout = domain.LastCheckout,
                IsEnded = domain.IsEnded,
            };
        }

        private static List<WarOverlayViewModel> DomainToViewModel(List<WarOverlay> domain)
        {
            return domain.Select(DomainToViewModel).ToList();
        }

        private static WarOverlay ViewModelToDomain(WarOverlayViewModel viewModel)
        {
            return new(viewModel.UserId, viewModel.Id, viewModel.ClanTag)
            {
                LastCheckout = viewModel.LastCheckout,
                IsEnded = viewModel.IsEnded,
            };
        }
    }
}
