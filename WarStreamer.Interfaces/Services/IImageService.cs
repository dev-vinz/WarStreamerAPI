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

        public Image? GetByOverlaySettingIdAndName(Guid overlaySettingId, string name);

        public List<Image> GetByOverlaySettingId(Guid overlaySettingId);

        public bool Update(Image domain);
    }
}
