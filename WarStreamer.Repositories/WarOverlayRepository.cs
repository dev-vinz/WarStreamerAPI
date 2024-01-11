﻿using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class WarOverlayRepository(IWarStreamerContext context)
        : Repository(context),
            IWarOverlayRepository
    {
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
                return [.. Context.Set<WarOverlay>()];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<WarOverlay> GetByUserId(string userId)
        {
            try
            {
                return
                [
                    .. Context
                        .Set<WarOverlay>()
                        .Where(o => o.UserId.Equals(userId, StringComparison.Ordinal))
                ];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public WarOverlay? GetByUserIdAndId(string userId, int id)
        {
            try
            {
                return Context
                    .Set<WarOverlay>()
                    .FirstOrDefault(
                        o => o.UserId.Equals(userId, StringComparison.Ordinal) && o.Id == id
                    );
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
                Insert(domain);

                return domain;
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
