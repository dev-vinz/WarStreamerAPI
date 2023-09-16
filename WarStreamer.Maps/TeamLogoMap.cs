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

        public List<TeamLogoViewModel> GetByUserId(string userId)
        {
            if (!decimal.TryParse(userId, out decimal decimalUserId)) throw new FormatException($"Cannot parse '{userId}' to decimal");

            return DomainToViewModel(_service.GetByUserId(decimalUserId));
        }

        public TeamLogoViewModel? GetByUserIdAndName(string userId, string name)
        {
            if (!decimal.TryParse(userId, out decimal decimalUserId)) throw new FormatException($"Cannot parse '{userId}' to decimal");

            TeamLogo? logo = _service.GetByUserIdAndName(decimalUserId, name);

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
            return new TeamLogoViewModel(domain.TeamName, domain.UserId.ToString())
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
            return new(viewModel.TeamName, decimal.Parse(viewModel.UserId))
            {
                Width = viewModel.Width,
                Height = viewModel.Height,
            };
        }
    }
}
