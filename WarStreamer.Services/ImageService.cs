using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;

namespace WarStreamer.Services
{
    public class ImageService(IImageRepository repository) : IImageService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IImageRepository _repository = repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public Image Create(Image domain)
        {
            return _repository.Save(domain);
        }

        public bool Delete(Image domain)
        {
            return _repository.Delete(domain);
        }

        public Image? GetByOverlaySettingIdAndName(Guid overlaySettingId, string name)
        {
            return _repository.GetByOverlaySettingIdAndName(overlaySettingId, name);
        }

        public List<Image> GetByOverlaySettingId(Guid overlaySettingId)
        {
            return _repository.GetByOverlaySettingId(overlaySettingId);
        }

        public bool Update(Image domain)
        {
            return _repository.Update(domain);
        }
    }
}
