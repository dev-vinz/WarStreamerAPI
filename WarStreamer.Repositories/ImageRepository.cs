using Microsoft.EntityFrameworkCore;
using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class ImageRepository(IWarStreamerContext context)
        : Repository(context),
            IImageRepository
    {
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

        public List<Image> GetByUserId(Guid userId)
        {
            try
            {
                return
                [
                    .. Context
                        .Set<Image>()
                        .Where(i => i.UserId == userId),
                ];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Image? GetByUserIdAndName(Guid userId, string name)
        {
            try
            {
                return Context
                    .Set<Image>()
                    .FirstOrDefault(i => i.UserId == userId && EF.Functions.Like(i.Name, name));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Image> GetUsedByUserId(Guid userId)
        {
            try
            {
                return
                [
                    .. Context
                        .Set<Image>()
                        .Where(i => i.UserId == userId && i.IsUsed)
                ];
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
                Insert(domain);

                return domain;
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
