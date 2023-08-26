using System.Drawing;
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
            return new(domain.UserId)
            {
                TextColor = domain.TextColor,
                LogoVisible = domain.IsLogo,
                LogoLocation = new Point(domain.LogoLocationX ?? 0, domain.LogoLocationY ?? 0),
                ClanNameVisible = domain.IsClanName,
                ClanNameLocation = new Point(domain.ClanNameLocationX ?? 0, domain.ClanNameLocationY ?? 0),
                TotalStarsVisible = domain.IsTotalStars,
                TotalStarsLocation = new Point(domain.TotalStarsLocationX ?? 0, domain.TotalStarsLocationY ?? 0),
                TotalPercentageVisible = domain.IsTotalPercentage,
                TotalPercentageLocation = new Point(domain.TotalPercentageLocationX ?? 0, domain.TotalPercentageLocationY ?? 0),
                AverageDurationVisible = domain.IsAverageDuration,
                AverageDurationLocation = new Point(domain.AverageDurationLocationX ?? 0, domain.AverageDurationLocationY ?? 0),
                PlayerDetailsVisible = domain.IsPlayerDetails,
                PlayerDetailsLocation = new Point(domain.PlayerDetailsLocationX ?? 0, domain.PlayerDetailsLocationY ?? 0),
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
            return new(viewModel.UserId)
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
                MirrorReflection = viewModel.MirrorReflection,
            };
        }
    }
}
