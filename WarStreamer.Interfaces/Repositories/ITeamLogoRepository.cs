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

        public List<TeamLogo> GetByUserId(Guid userId);

        public TeamLogo? GetByUserIdAndName(Guid userId, string name);

        public TeamLogo Save(TeamLogo domain);

        public bool Update(TeamLogo domain);
    }
}
