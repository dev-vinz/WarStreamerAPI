using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface ITeamLogoRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(TeamLogo domain);

        public List<TeamLogo> GetAll();

        public List<TeamLogo> GetByUserId(decimal userId);

        public TeamLogo? GetByUserIdAndName(decimal userId, string name);

        public TeamLogo Save(TeamLogo domain);

        public bool Update(TeamLogo domain);
    }
}
