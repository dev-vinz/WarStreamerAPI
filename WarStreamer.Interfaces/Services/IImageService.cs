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

        public Image? GetByOverlaySettingIdAndName(string overlaySettingId, string name);

        public List<Image> GetByOverlaySettingId(string overlaySettingId);

        public bool Update(Image domain);
    }
}
