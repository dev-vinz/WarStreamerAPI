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

        public List<TeamLogo> GetAll();

        public List<TeamLogo> GetByUserId(decimal userId);

        public bool Update(TeamLogo domain);
    }
}
