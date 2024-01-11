using WarStreamer.Models;

namespace WarStreamer.Interfaces.Services
{
    public interface IWarOverlayService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public WarOverlay Create(WarOverlay domain);

        public bool Delete(WarOverlay domain);

        public List<WarOverlay> GetAll();

        public List<WarOverlay> GetByUserId(string userId);

        public WarOverlay? GetByUserIdAndId(string userId, int id);

        public bool Update(WarOverlay domain);
    }
}
