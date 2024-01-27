using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IOverlaySettingRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(OverlaySetting domain);

        public OverlaySetting? GetByUserId(Guid userId);

        public OverlaySetting Save(OverlaySetting domain);

        public bool Update(OverlaySetting domain);
    }
}
