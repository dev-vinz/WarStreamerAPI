using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class ImageMap : IImageMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IImageService _service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public ImageMap(IImageService service)
        {
            _service = service;
        }

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
            Image image = ViewModelToDomain(viewModel);
            return _service.Delete(image);
        }

        public List<ImageViewModel> GetAll()
        {
            return DomainToViewModel(_service.GetAll());
        }

        public ImageViewModel? GetByOverlaySettingIdAndName(string overlaySettingId, string name)
        {
            if (!decimal.TryParse(overlaySettingId, out decimal decimalOverlaySettingId)) throw new FormatException($"Cannot parse '{overlaySettingId}' to decimal");

            Image? image = _service.GetByOverlaySettingIdAndName(decimalOverlaySettingId, name);

            if (image == null) return null;

            return DomainToViewModel(image);
        }

        public List<ImageViewModel> GetByOverlaySettingId(string overlaySettingId)
        {
            if (!decimal.TryParse(overlaySettingId, out decimal decimalOverlaySettingId)) throw new FormatException($"Cannot parse '{overlaySettingId}' to decimal");

            return DomainToViewModel(_service.GetByOverlaySettingId(decimalOverlaySettingId));
        }

        public bool Update(ImageViewModel viewModel)
        {
            Image image = ViewModelToDomain(viewModel);
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
            return new(domain.OverlaySettingId.ToString(), domain.Name)
            {
                Location = new(domain.LocationX, domain.LocationY),
                Width = domain.Width,
                Height = domain.Height,
            };
        }

        private static List<ImageViewModel> DomainToViewModel(List<Image> domain)
        {
            return domain
                .Select(DomainToViewModel)
                .ToList();
        }

        private static Image ViewModelToDomain(ImageViewModel viewModel)
        {
            return new(decimal.Parse(viewModel.OverlaySettingId), viewModel.Name)
            {
                LocationX = viewModel.Location.X,
                LocationY = viewModel.Location.Y,
                Width = viewModel.Width,
                Height = viewModel.Height,
            };
        }
    }
}
