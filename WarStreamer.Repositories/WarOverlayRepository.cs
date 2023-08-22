using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class WarOverlayRepository : Repository, IWarOverlayRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public WarOverlayRepository(IWarStreamerContext context) : base(context) { }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(WarOverlay domain)
        {
            try
            {
                Remove(domain);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<WarOverlay> GetAll()
        {
            try
            {
                return Context.Set<WarOverlay>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<WarOverlay> GetByUserId(decimal userId)
        {
            try
            {
                return Context.Set<WarOverlay>()
                              .Where(o => o.UserId == userId)
                              .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public WarOverlay Save(WarOverlay domain)
        {
            try
            {
                domain.CreatedAt = DateTimeOffset.UtcNow;
                domain.UpdatedAt = domain.CreatedAt;

                return Insert(domain);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(WarOverlay domain)
        {
            try
            {
                domain.UpdatedAt = DateTimeOffset.UtcNow;
                Update<WarOverlay>(domain);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
