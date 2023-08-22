using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class ImageRepository : Repository, IImageRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public ImageRepository(IWarStreamerContext context) : base(context) { }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(Image domain)
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

        public List<Image> GetAll()
        {
            try
            {
                return Context.Set<Image>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Image? GetById(int id)
        {
            try
            {
                return Context.Set<Image>().FirstOrDefault(i => i.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Image> GetByOverlaySettingId(decimal overlaySettingId)
        {
            try
            {
                return Context.Set<Image>()
                              .Where(i => i.OverlaySettingId == overlaySettingId)
                              .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Image Save(Image domain)
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

        public bool Update(Image domain)
        {
            try
            {
                domain.UpdatedAt = DateTimeOffset.UtcNow;
                Update<Image>(domain);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
