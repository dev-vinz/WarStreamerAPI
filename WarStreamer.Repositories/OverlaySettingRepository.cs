using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class OverlaySettingRepository(IWarStreamerContext context)
        : Repository(context),
            IOverlaySettingRepository
    {
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
                return [.. Context.Set<OverlaySetting>()];
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
                Insert(domain);

                return domain;
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
