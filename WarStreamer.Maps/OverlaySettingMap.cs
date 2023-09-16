using WarStreamer.Commons.Tools;
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

        public OverlaySettingViewModel? GetByUserId(string userId)
        {
            if (!decimal.TryParse(userId, out decimal decimalUserId)) throw new FormatException($"Cannot parse '{userId}' to decimal");

            OverlaySetting? setting = _service.GetByUserId(decimalUserId);

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
            return new(domain.UserId.ToString())
            {
                TextColor = domain.TextColor,
                LogoVisible = domain.IsLogo,
                LogoLocation = new Location2D(domain.LogoLocationX ?? 0, domain.LogoLocationY ?? 0),
                ClanNameVisible = domain.IsClanName,
                ClanNameLocation = new Location2D(domain.ClanNameLocationX ?? 0, domain.ClanNameLocationY ?? 0),
                TotalStarsVisible = domain.IsTotalStars,
                TotalStarsLocation = new Location2D(domain.TotalStarsLocationX ?? 0, domain.TotalStarsLocationY ?? 0),
                TotalPercentageVisible = domain.IsTotalPercentage,
                TotalPercentageLocation = new Location2D(domain.TotalPercentageLocationX ?? 0, domain.TotalPercentageLocationY ?? 0),
                AverageDurationVisible = domain.IsAverageDuration,
                AverageDurationLocation = new Location2D(domain.AverageDurationLocationX ?? 0, domain.AverageDurationLocationY ?? 0),
                PlayerDetailsVisible = domain.IsPlayerDetails,
                PlayerDetailsLocation = new Location2D(domain.PlayerDetailsLocationX ?? 0, domain.PlayerDetailsLocationY ?? 0),
                LastAttackToWinVisible = domain.IsLastAttackToWin,
                LastAttackToWinLocation = new Location2D(domain.LastAttackToWinLocationX ?? 0, domain.LastAttackToWinLocationY ?? 0),
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
            return new(decimal.Parse(viewModel.UserId))
            {
                TextColor = viewModel.TextColor,
                IsLogo = viewModel.LogoVisible,
                LogoLocationX = viewModel.LogoLocation?.X,
                LogoLocationY = viewModel.LogoLocation?.Y,
                IsClanName = viewModel.ClanNameVisible,
                ClanNameLocationX = viewModel.ClanNameLocation?.X,
                ClanNameLocationY = viewModel.ClanNameLocation?.Y,
                IsTotalStars = viewModel.TotalStarsVisible,
                TotalStarsLocationX = viewModel.TotalStarsLocation?.X,
                TotalStarsLocationY = viewModel.TotalStarsLocation?.Y,
                IsTotalPercentage = viewModel.TotalPercentageVisible,
                TotalPercentageLocationX = viewModel.TotalPercentageLocation?.X,
                TotalPercentageLocationY = viewModel.TotalPercentageLocation?.Y,
                IsAverageDuration = viewModel.AverageDurationVisible,
                AverageDurationLocationX = viewModel.AverageDurationLocation?.X,
                AverageDurationLocationY = viewModel.AverageDurationLocation?.Y,
                IsPlayerDetails = viewModel.PlayerDetailsVisible,
                PlayerDetailsLocationX = viewModel.PlayerDetailsLocation?.X,
                PlayerDetailsLocationY = viewModel.PlayerDetailsLocation?.Y,
                IsLastAttackToWin = viewModel.LastAttackToWinVisible,
                LastAttackToWinLocationX = viewModel.LastAttackToWinLocation?.X,
                LastAttackToWinLocationY = viewModel.LastAttackToWinLocation?.Y,
                MirrorReflection = viewModel.MirrorReflection,
            };
        }
    }
}
