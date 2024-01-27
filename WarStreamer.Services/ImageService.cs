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

        public List<Image> GetByUserId(Guid userId)
        {
            return _repository.GetByUserId(userId);
        }

        public Image? GetByUserIdAndName(Guid userId, string name)
        {
            return _repository.GetByUserIdAndName(userId, name);
        }

        public List<Image> GetUsedByUserId(Guid userId)
        {
            return _repository.GetUsedByUserId(userId);
        }

        public bool Update(Image domain)
        {
            return _repository.Update(domain);
        }
    }
}
