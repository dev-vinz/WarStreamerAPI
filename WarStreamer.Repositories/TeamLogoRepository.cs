using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class TeamLogoRepository : Repository, ITeamLogoRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public TeamLogoRepository(IWarStreamerContext context) : base(context) { }

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
                return Context.Set<TeamLogo>().ToList();
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
                return Context.Set<TeamLogo>()
                              .Where(l => l.UserId == userId)
                              .ToList();
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
                return Context.Set<TeamLogo>().FirstOrDefault(l => l.UserId == userId && l.TeamName == name.ToUpper());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TeamLogo Save(TeamLogo domain)
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

        public bool Update(TeamLogo domain)
        {
            try
            {
                domain.UpdatedAt = DateTimeOffset.UtcNow;
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
