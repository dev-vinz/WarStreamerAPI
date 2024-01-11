using WarStreamer.Commons.Tools;
using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class OverlaySettingMap(IOverlaySettingService service) : IOverlaySettingMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IOverlaySettingService _service = service;

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

        public OverlaySettingViewModel? GetByUserId(string userId)
        {
            OverlaySetting? setting = _service.GetByUserId(userId);
            return setting != null ? DomainToViewModel(setting) : null;
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
            return new(domain.UserId)
            {
                FontId = domain.FontId,
                TextColor = domain.TextColor,
                LogoVisible = domain.IsLogo,
                LogoSize = domain.LogoSize,
                LogoLocation = new Location2D(domain.LogoLocationX ?? 0, domain.LogoLocationY ?? 0),
                ClanNameVisible = domain.IsClanName,
                ClanNameSize = domain.ClanNameSize,
                ClanNameLocation = new Location2D(
                    domain.ClanNameLocationX ?? 0,
                    domain.ClanNameLocationY ?? 0
                ),
                TotalStarsVisible = domain.IsTotalStars,
                TotalStarsSize = domain.TotalStarsSize,
                TotalStarsLocation = new Location2D(
                    domain.TotalStarsLocationX ?? 0,
                    domain.TotalStarsLocationY ?? 0
                ),
                TotalPercentageVisible = domain.IsTotalPercentage,
                TotalPercentageSize = domain.TotalPercentageSize,
                TotalPercentageLocation = new Location2D(
                    domain.TotalPercentageLocationX ?? 0,
                    domain.TotalPercentageLocationY ?? 0
                ),
                AverageDurationVisible = domain.IsAverageDuration,
                AverageDurationSize = domain.AverageDurationSize,
                AverageDurationLocation = new Location2D(
                    domain.AverageDurationLocationX ?? 0,
                    domain.AverageDurationLocationY ?? 0
                ),
                PlayerDetailsVisible = domain.IsPlayerDetails,
                PlayerDetailsSize = domain.PlayerDetailsSize,
                PlayerDetailsLocation = new Location2D(
                    domain.PlayerDetailsLocationX ?? 0,
                    domain.PlayerDetailsLocationY ?? 0
                ),
                LastAttackToWinVisible = domain.IsLastAttackToWin,
                LastAttackToWinSize = domain.LastAttackToWinSize,
                LastAttackToWinLocation = new Location2D(
                    domain.LastAttackToWinLocationX ?? 0,
                    domain.LastAttackToWinLocationY ?? 0
                ),
                MirrorReflection = domain.MirrorReflection,
            };
        }

        private static List<OverlaySettingViewModel> DomainToViewModel(List<OverlaySetting> domain)
        {
            return domain.Select(DomainToViewModel).ToList();
        }

        private static OverlaySetting ViewModelToDomain(OverlaySettingViewModel viewModel)
        {
            return new(viewModel.UserId)
            {
                FontId = viewModel.FontId,
                TextColor = viewModel.TextColor,
                IsLogo = viewModel.LogoVisible,
                LogoSize = viewModel.LogoSize,
                LogoLocationX = viewModel.LogoLocation?.X,
                LogoLocationY = viewModel.LogoLocation?.Y,
                IsClanName = viewModel.ClanNameVisible,
                ClanNameSize = viewModel.ClanNameSize,
                ClanNameLocationX = viewModel.ClanNameLocation?.X,
                ClanNameLocationY = viewModel.ClanNameLocation?.Y,
                IsTotalStars = viewModel.TotalStarsVisible,
                TotalStarsSize = viewModel.TotalStarsSize,
                TotalStarsLocationX = viewModel.TotalStarsLocation?.X,
                TotalStarsLocationY = viewModel.TotalStarsLocation?.Y,
                IsTotalPercentage = viewModel.TotalPercentageVisible,
                TotalPercentageSize = viewModel.TotalPercentageSize,
                TotalPercentageLocationX = viewModel.TotalPercentageLocation?.X,
                TotalPercentageLocationY = viewModel.TotalPercentageLocation?.Y,
                IsAverageDuration = viewModel.AverageDurationVisible,
                AverageDurationSize = viewModel.AverageDurationSize,
                AverageDurationLocationX = viewModel.AverageDurationLocation?.X,
                AverageDurationLocationY = viewModel.AverageDurationLocation?.Y,
                IsPlayerDetails = viewModel.PlayerDetailsVisible,
                PlayerDetailsSize = viewModel.PlayerDetailsSize,
                PlayerDetailsLocationX = viewModel.PlayerDetailsLocation?.X,
                PlayerDetailsLocationY = viewModel.PlayerDetailsLocation?.Y,
                IsLastAttackToWin = viewModel.LastAttackToWinVisible,
                LastAttackToWinSize = viewModel.LastAttackToWinSize,
                LastAttackToWinLocationX = viewModel.LastAttackToWinLocation?.X,
                LastAttackToWinLocationY = viewModel.LastAttackToWinLocation?.Y,
                MirrorReflection = viewModel.MirrorReflection,
            };
        }
    }
}
