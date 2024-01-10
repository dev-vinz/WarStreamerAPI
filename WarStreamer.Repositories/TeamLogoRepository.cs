﻿using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class TeamLogoRepository(IWarStreamerContext context)
        : Repository(context),
            ITeamLogoRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(TeamLogo domain)
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

        public List<TeamLogo> GetAll()
        {
            try
            {
                return [.. Context.Set<TeamLogo>()];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TeamLogo> GetByUserId(decimal userId)
        {
            try
            {
                return
                [
                    .. Context
                        .Set<TeamLogo>()
                        .Where(l => l.UserId == userId)
                ];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TeamLogo? GetByUserIdAndName(decimal userId, string name)
        {
            try
            {
                return Context
                    .Set<TeamLogo>()
                    .FirstOrDefault(
                        l =>
                            l.UserId == userId
                            && l.TeamName.Equals(name, StringComparison.CurrentCultureIgnoreCase)
                    );
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TeamLogo? Save(TeamLogo domain)
        {
            try
            {
                return Insert(domain);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(TeamLogo domain)
        {
            try
            {
                Update<TeamLogo>(domain);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
