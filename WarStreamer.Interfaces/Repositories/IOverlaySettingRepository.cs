using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IOverlaySettingRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(OverlaySetting domain);

        public List<OverlaySetting> GetAll();

        public OverlaySetting? GetByUserId(Guid userId);

        public OverlaySetting Save(OverlaySetting domain);

        public bool Update(OverlaySetting domain);
    }
}
