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
            Image image = ViewModelToDomain(viewModel, Guid.Parse(viewModel.UserId));
            return _service.Delete(image);
        }

        public List<ImageViewModel> GetByUserId(string userId)
        {
            Guid guid = Guid.Empty.ParseDiscordId(userId);
            return DomainToViewModel(_service.GetByUserId(guid));
        }

        public ImageViewModel? GetByUserIdAndName(string userId, string name)
        {
            Guid guid = Guid.Empty.ParseDiscordId(userId);
            Image? image = _service.GetByUserIdAndName(guid, name);
            return image != null ? DomainToViewModel(image) : null;
        }

        public List<ImageViewModel> GetUsedByUserId(string userId)
        {
            Guid guid = Guid.Empty.ParseDiscordId(userId);
            return DomainToViewModel(_service.GetUsedByUserId(guid));
        }

        public bool Update(ImageViewModel viewModel)
        {
            Image image = ViewModelToDomain(viewModel, Guid.Parse(viewModel.UserId));
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
            return new($"{domain.UserId}", domain.Name)
            {
                Location = new(domain.LocationX, domain.LocationY),
                Width = domain.Width,
                Height = domain.Height,
                IsUsed = domain.IsUsed,
            };
        }

        private static List<ImageViewModel> DomainToViewModel(List<Image> domain)
        {
            return domain.Select(DomainToViewModel).ToList();
        }

        private static Image ViewModelToDomain(ImageViewModel viewModel, Guid userId)
        {
            return new(userId, viewModel.Name)
            {
                LocationX = viewModel.Location.X,
                LocationY = viewModel.Location.Y,
                Width = viewModel.Width,
                Height = viewModel.Height,
                IsUsed = viewModel.IsUsed,
            };
        }

        private static Image ViewModelToDomain(ImageViewModel viewModel)
        {
            return ViewModelToDomain(viewModel, Guid.Empty.ParseDiscordId(viewModel.UserId));
        }
    }
}
