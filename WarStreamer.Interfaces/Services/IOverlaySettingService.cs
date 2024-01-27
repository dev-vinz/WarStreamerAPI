using WarStreamer.Models;

namespace WarStreamer.Interfaces.Services
{
    public interface IOverlaySettingService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public OverlaySetting Create(OverlaySetting domain);

        public bool Delete(OverlaySetting domain);

        public OverlaySetting? GetByUserId(Guid userId);

        public bool Update(OverlaySetting domain);
    }
}
