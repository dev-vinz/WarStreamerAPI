using WarStreamer.Commons.Extensions;
using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class ImageMap(IImageService service) : IImageMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IImageService _service = service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public ImageViewModel Create(ImageViewModel viewModel)
        {
            Image image = ViewModelToDomain(viewModel);
            return DomainToViewModel(_service.Create(image));
        }

        public bool Delete(ImageViewModel viewModel)
        {
            Image image = ViewModelToDomain(viewModel, Guid.Parse(viewModel.OverlaySettingId));
            return _service.Delete(image);
        }

        public List<ImageViewModel> GetAll()
        {
            return DomainToViewModel(_service.GetAll());
        }

        public ImageViewModel? GetByOverlaySettingIdAndName(string overlaySettingId, string name)
        {
            Guid guid = Guid.Empty.ParseDiscordId(overlaySettingId);
            Image? image = _service.GetByOverlaySettingIdAndName(guid, name);
            return image != null ? DomainToViewModel(image) : null;
        }

        public List<ImageViewModel> GetByOverlaySettingId(string overlaySettingId)
        {
            Guid guid = Guid.Empty.ParseDiscordId(overlaySettingId);
            return DomainToViewModel(_service.GetByOverlaySettingId(guid));
        }

        public bool Update(ImageViewModel viewModel)
        {
            Image image = ViewModelToDomain(viewModel, Guid.Parse(viewModel.OverlaySettingId));
            return _service.Update(image);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static ImageViewModel DomainToViewModel(Image domain)
        {
            return new($"{domain.OverlaySettingId}", domain.Name)
            {
                Location = new(domain.LocationX, domain.LocationY),
                Width = domain.Width,
                Height = domain.Height,
            };
        }

        private static List<ImageViewModel> DomainToViewModel(List<Image> domain)
        {
            return domain.Select(DomainToViewModel).ToList();
        }

        private static Image ViewModelToDomain(ImageViewModel viewModel, Guid overlaySettingId)
        {
            return new(overlaySettingId, viewModel.Name)
            {
                LocationX = viewModel.Location.X,
                LocationY = viewModel.Location.Y,
                Width = viewModel.Width,
                Height = viewModel.Height,
            };
        }

        private static Image ViewModelToDomain(ImageViewModel viewModel)
        {
            return ViewModelToDomain(
                viewModel,
                Guid.Empty.ParseDiscordId(viewModel.OverlaySettingId)
            );
        }
    }
}
