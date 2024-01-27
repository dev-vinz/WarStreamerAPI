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

        public List<TeamLogoViewModel> GetByUserId(string userId);

        public TeamLogoViewModel? GetByUserIdAndName(string userId, string name);

        public bool Update(TeamLogoViewModel viewModel);
    }
}
