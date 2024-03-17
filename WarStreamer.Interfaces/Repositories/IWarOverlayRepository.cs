using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IWarOverlayRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(WarOverlay domain);

        public List<WarOverlay> GetByUserId(Guid userId);

        public WarOverlay? GetByUserIdAndId(Guid userId, Guid id);

        public WarOverlay Save(WarOverlay domain);

        public bool Update(WarOverlay domain);
    }
}
