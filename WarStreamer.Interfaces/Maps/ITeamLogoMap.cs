using WarStreamer.ViewModels;

namespace WarStreamer.Interfaces.Maps
{
    public interface ITeamLogoMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public TeamLogoViewModel Create(TeamLogoViewModel viewModel);

        public bool Delete(TeamLogoViewModel viewModel);

        public List<TeamLogoViewModel> GetAll();

        public List<TeamLogoViewModel> GetByUserId(decimal userId);

        public TeamLogoViewModel? GetByUserIdAndName(decimal userId, string name);

        public bool Update(TeamLogoViewModel viewModel);
    }
}
