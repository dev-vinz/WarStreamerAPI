using WarStreamer.Models;

namespace WarStreamer.Interfaces.Services
{
    public interface ITeamLogoService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public TeamLogo Create(TeamLogo domain);

        public bool Delete(TeamLogo domain);

        public List<TeamLogo> GetByUserId(Guid userId);

        public TeamLogo? GetByUserIdAndName(Guid userId, string name);

        public bool Update(TeamLogo domain);
    }
}
