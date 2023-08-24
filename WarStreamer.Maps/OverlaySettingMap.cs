using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class OverlaySettingMap : IOverlaySettingMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IOverlaySettingService _service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public OverlaySettingMap(IOverlaySettingService service)
        {
            _service = service;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public OverlaySettingViewModel Create(OverlaySettingViewModel viewModel)
        {
            OverlaySetting setting = ViewModelToDomain(viewModel);
            return DomainToViewModel(_service.Create(setting));
        }

        public bool Delete(OverlaySettingViewModel viewModel)
        {
            OverlaySetting setting = ViewModelToDomain(viewModel);
            return _service.Delete(setting);
        }

        public List<OverlaySettingViewModel> GetAll()
        {
            return DomainToViewModel(_service.GetAll());
        }

        public OverlaySettingViewModel? GetByUserId(decimal userId)
        {
            OverlaySetting? setting = _service.GetByUserId(userId);

            if (setting == null) return null;

            return DomainToViewModel(setting);
        }

        public bool Update(OverlaySettingViewModel viewModel)
        {
            OverlaySetting setting = ViewModelToDomain(viewModel);
            return _service.Update(setting);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static OverlaySettingViewModel DomainToViewModel(OverlaySetting domain)
        {
            return new OverlaySettingViewModel(domain.UserId)
            {
                TextColor = domain.TextColor,
                IsLogo = domain.IsLogo,
                LogoLocationX = domain.LogoLocationX,
                LogoLocationY = domain.LogoLocationY,
                IsClanName = domain.IsClanName,
                ClanNameLocationX = domain.ClanNameLocationX,
                ClanNameLocationY = domain.ClanNameLocationY,
                IsTotalStars = domain.IsTotalStars,
                TotalStarsLocationX = domain.TotalStarsLocationX,
                TotalStarsLocationY = domain.TotalStarsLocationY,
                IsTotalPercentage = domain.IsTotalPercentage,
                TotalPercentageLocationX = domain.TotalPercentageLocationX,
                TotalPercentageLocationY = domain.TotalPercentageLocationY,
                IsAverageDuration = domain.IsAverageDuration,
                AverageDurationLocationX = domain.AverageDurationLocationX,
                AverageDurationLocationY = domain.AverageDurationLocationY,
                IsPlayerDetails = domain.IsPlayerDetails,
                PlayerDetailsLocationX = domain.PlayerDetailsLocationX,
                PlayerDetailsLocationY = domain.PlayerDetailsLocationY,
                MirrorReflection = domain.MirrorReflection,
            };
        }

        private static List<OverlaySettingViewModel> DomainToViewModel(List<OverlaySetting> domain)
        {
            return domain
                .Select(DomainToViewModel)
                .ToList();
        }

        private static OverlaySetting ViewModelToDomain(OverlaySettingViewModel viewModel)
        {
            return new OverlaySetting
            {
                UserId = viewModel.UserId,
                TextColor = viewModel.TextColor,
                IsLogo = viewModel.IsLogo,
                LogoLocationX = viewModel.LogoLocationX,
                LogoLocationY = viewModel.LogoLocationY,
                IsClanName = viewModel.IsClanName,
                ClanNameLocationX = viewModel.ClanNameLocationX,
                ClanNameLocationY = viewModel.ClanNameLocationY,
                IsTotalStars = viewModel.IsTotalStars,
                TotalStarsLocationX = viewModel.TotalStarsLocationX,
                TotalStarsLocationY = viewModel.TotalStarsLocationY,
                IsTotalPercentage = viewModel.IsTotalPercentage,
                TotalPercentageLocationX = viewModel.TotalPercentageLocationX,
                TotalPercentageLocationY = viewModel.TotalPercentageLocationY,
                IsAverageDuration = viewModel.IsAverageDuration,
                AverageDurationLocationX = viewModel.AverageDurationLocationX,
                AverageDurationLocationY = viewModel.AverageDurationLocationY,
                IsPlayerDetails = viewModel.IsPlayerDetails,
                PlayerDetailsLocationX = viewModel.PlayerDetailsLocationX,
                PlayerDetailsLocationY = viewModel.PlayerDetailsLocationY,
                MirrorReflection = viewModel.MirrorReflection,
            };
        }
    }
}
