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

        public ImageViewModel? GetById(int id)
        {
            Image? image = _service.GetById(id);

            if (image == null) return null;

            return DomainToViewModel(image);
        }

        public List<ImageViewModel> GetByOverlaySettingId(decimal overlaySettingId)
        {
            return DomainToViewModel(_service.GetByOverlaySettingId(overlaySettingId));
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
            return new(domain.Id, domain.OverlaySettingId)
            {
                LocationX = domain.LocationX,
                LocationY = domain.LocationY,
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
            return new(viewModel.Id, viewModel.OverlaySettingId)
            {
                LocationX = viewModel.LocationX,
                LocationY = viewModel.LocationY,
                Width = viewModel.Width,
                Height = viewModel.Height,
            };
        }
    }
}
