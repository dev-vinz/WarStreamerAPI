using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class OverlaySettingRepository : Repository, IOverlaySettingRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public OverlaySettingRepository(IWarStreamerContext context) : base(context) { }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(OverlaySetting domain)
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

        public List<OverlaySetting> GetAll()
        {
            try
            {
                return Context.Set<OverlaySetting>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public OverlaySetting? GetByUserId(decimal userId)
        {
            try
            {
                return Context.Set<OverlaySetting>().FirstOrDefault(os => os.UserId == userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public OverlaySetting Save(OverlaySetting domain)
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

        public bool Update(OverlaySetting domain)
        {
            try
            {
                domain.UpdatedAt = DateTimeOffset.UtcNow;
                Update<OverlaySetting>(domain);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
