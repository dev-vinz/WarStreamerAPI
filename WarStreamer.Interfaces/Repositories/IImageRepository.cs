using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IImageRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(Image domain);

        public Image? GetByOverlaySettingIdAndName(Guid overlaySettingId, string name);

        public List<Image> GetByOverlaySettingId(Guid overlaySettingId);

        public Image Save(Image domain);

        public bool Update(Image domain);
    }
}
