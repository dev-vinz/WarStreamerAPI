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

        public List<Image> GetAll();

        public Image? GetById(int id);

        public List<Image> GetByOverlaySettingId(decimal overlaySettingId);

        public bool Update(Image domain);
    }
}
