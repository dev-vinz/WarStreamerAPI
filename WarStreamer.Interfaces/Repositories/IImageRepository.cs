using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IImageRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(Image domain);

        public List<Image> GetByUserId(Guid userId);

        public Image? GetByUserIdAndName(Guid userId, string name);

        public List<Image> GetUsedByUserId(Guid userId);

        public Image Save(Image domain);

        public bool Update(Image domain);
    }
}
