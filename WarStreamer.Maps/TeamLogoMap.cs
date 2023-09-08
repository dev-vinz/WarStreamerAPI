using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class TeamLogoMap : ITeamLogoMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly ITeamLogoService _service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public TeamLogoMap(ITeamLogoService service)
        {
            _service = service;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public TeamLogoViewModel Create(TeamLogoViewModel viewModel)
        {
            TeamLogo logo = ViewModelToDomain(viewModel);
            return DomainToViewModel(_service.Create(logo));
        }

        public bool Delete(TeamLogoViewModel viewModel)
        {
            TeamLogo logo = ViewModelToDomain(viewModel);
            return _service.Delete(logo);
        }

        public List<TeamLogoViewModel> GetAll()
        {
            return DomainToViewModel(_service.GetAll());
        }

        public List<TeamLogoViewModel> GetByUserId(decimal userId)
        {
            return DomainToViewModel(_service.GetByUserId(userId));
        }

        public TeamLogoViewModel? GetByUserIdAndName(decimal userId, string name)
        {
            TeamLogo? logo = _service.GetByUserIdAndName(userId, name);

            if (logo == null) return null;

            return DomainToViewModel(logo);
        }

        public bool Update(TeamLogoViewModel viewModel)
        {
            TeamLogo logo = ViewModelToDomain(viewModel);
            return _service.Update(logo);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static TeamLogoViewModel DomainToViewModel(TeamLogo domain)
        {
            return new TeamLogoViewModel(domain.TeamName, domain.UserId)
            {
                Width = domain.Width,
                Height = domain.Height,
            };
        }

        private static List<TeamLogoViewModel> DomainToViewModel(List<TeamLogo> domain)
        {
            return domain
                .Select(DomainToViewModel)
                .ToList();
        }

        private static TeamLogo ViewModelToDomain(TeamLogoViewModel viewModel)
        {
            return new(viewModel.TeamName, viewModel.UserId)
            {
                Width = viewModel.Width,
                Height = viewModel.Height,
            };
        }
    }
}
