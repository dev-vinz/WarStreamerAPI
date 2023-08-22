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

        public List<OverlaySetting> GetAll();

        public OverlaySetting? GetByUserId(decimal userId);

        public bool Update(OverlaySetting domain);
    }
}
