using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IImageRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(Image domain);

        public List<Image> GetAll();

        public Image? GetByOverlaySettingIdAndName(string overlaySettingId, string name);

        public List<Image> GetByOverlaySettingId(string overlaySettingId);

        public Image Save(Image domain);

        public bool Update(Image domain);
    }
}
