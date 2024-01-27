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

        public List<WarOverlay> GetByUserId(Guid userId);

        public WarOverlay? GetByUserIdAndId(Guid userId, int id);

        public bool Update(WarOverlay domain);
    }
}
