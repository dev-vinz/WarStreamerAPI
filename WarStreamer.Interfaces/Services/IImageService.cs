using WarStreamer.Models;

namespace WarStreamer.Interfaces.Services
{
    public interface IImageService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public Image Create(Image domain);

        public bool Delete(Image domain);

        public List<Image> GetByUserId(Guid userId);

        public Image? GetByUserIdAndName(Guid userId, string name);

        public List<Image> GetUsedByUserId(Guid userId);

        public bool Update(Image domain);
    }
}
