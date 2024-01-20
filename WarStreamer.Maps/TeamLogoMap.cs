using WarStreamer.Commons.Extensions;
using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class TeamLogoMap(ITeamLogoService service) : ITeamLogoMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly ITeamLogoService _service = service;

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
            TeamLogo logo = ViewModelToDomain(viewModel, Guid.Parse(viewModel.UserId));
            return _service.Delete(logo);
        }

        public List<TeamLogoViewModel> GetAll()
        {
            return DomainToViewModel(_service.GetAll());
        }

        public List<TeamLogoViewModel> GetByUserId(string userId)
        {
            Guid guid = Guid.Empty.ParseDiscordId(userId);
            return DomainToViewModel(_service.GetByUserId(guid));
        }

        public TeamLogoViewModel? GetByUserIdAndName(string userId, string name)
        {
            Guid guid = Guid.Empty.ParseDiscordId(userId);
            TeamLogo? logo = _service.GetByUserIdAndName(guid, name);
            return logo != null ? DomainToViewModel(logo) : null;
        }

        public bool Update(TeamLogoViewModel viewModel)
        {
            TeamLogo logo = ViewModelToDomain(viewModel, Guid.Parse(viewModel.UserId));
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
            return new TeamLogoViewModel(domain.TeamName, $"{domain.UserId}")
            {
                ClanTags = [.. domain.ClanTags]
            };
        }

        private static List<TeamLogoViewModel> DomainToViewModel(List<TeamLogo> domain)
        {
            return domain.Select(DomainToViewModel).ToList();
        }

        private static TeamLogo ViewModelToDomain(TeamLogoViewModel viewModel, Guid userId)
        {
            return new(viewModel.TeamName, userId) { ClanTags = [.. viewModel.ClanTags] };
        }

        private static TeamLogo ViewModelToDomain(TeamLogoViewModel viewModel)
        {
            return ViewModelToDomain(viewModel, Guid.Empty.ParseDiscordId(viewModel.UserId));
        }
    }
}
