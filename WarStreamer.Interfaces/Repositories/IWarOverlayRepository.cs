using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IWarOverlayRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(WarOverlay domain);

        public List<WarOverlay> GetAll();

        public List<WarOverlay> GetByUserId(decimal userId);

        public WarOverlay? GetByUserIdAndId(decimal userId, int id);

        public WarOverlay? Save(WarOverlay domain);

        public bool Update(WarOverlay domain);
    }
}
