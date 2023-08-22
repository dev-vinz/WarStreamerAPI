using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;

namespace WarStreamer.Services
{
    public class ImageService : IImageService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IImageRepository _repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public ImageService(IImageRepository repository)
        {
            _repository = repository;
        }

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

        public List<Image> GetAll()
        {
            return _repository.GetAll();
        }

        public Image? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<Image> GetByOverlaySettingId(decimal overlaySettingId)
        {
            return _repository.GetByOverlaySettingId(overlaySettingId);
        }

        public bool Update(Image domain)
        {
            return _repository.Update(domain);
        }
    }
}
