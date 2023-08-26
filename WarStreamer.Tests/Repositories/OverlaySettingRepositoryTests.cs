using Microsoft.Extensions.DependencyInjection;
using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Tests.Tools;

namespace WarStreamer.Tests.Repositories
{
    [TestCaseOrderer("WarStreamer.Tests.Tools.OrderOrderer", "WarStreamer.Tests")]
    public class OverlaySettingRepositoryTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const decimal USER_ID = 1;
        private const string TEXT_COLOR = "#000000";
        private const string TEXT_COLOR_UPDATED = "#00FF00";
        private const bool IS_LOGO = true;
        private const int LOGO_LOCATION_X = 100;
        private const int LOGO_LOCATION_Y = 100;
        private const bool IS_CLAN_NAME = true;
        private const int CLAN_NAME_LOCATION_X = 400;
        private const int CLAN_NAME_LOCATION_Y = 400;
        private const bool IS_TOTAL_STARS = true;
        private const int TOTAL_STARS_LOCATION_X = 200;
        private const int TOTAL_STARS_LOCATION_Y = 200;
        private const bool IS_TOTAL_PERCENTAGE = true;
        private const int TOTAL_PERCENTAGE_LOCATION_X = 300;
        private const int TOTAL_PERCENTAGE_LOCATION_Y = 300;
        private const bool IS_AVERAGE_DURATION = true;
        private const int AVERAGE_DURATION_LOCATION_X = 500;
        private const int AVERAGE_DURATION_LOCATION_Y = 500;
        private const bool IS_PLAYER_DETAILS = true;
        private const int PLAYER_DETAILS_LOCATION_X = 600;
        private const int PLAYER_DETAILS_LOCATION_Y = 600;
        private const bool MIRROR_REFLECTION = false;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IOverlaySettingRepository _repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public OverlaySettingRepositoryTests()
        {
            _repository = new ServiceTestCollection()
                .BuildServiceProvider()
                .GetRequiredService<IOverlaySettingRepository>();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        [TestOrder(1)]
        public void WhenInsertOverlaySetting_ThenReturnsAddedWarOverlay()
        {
            OverlaySetting setting = _repository.Save(CreateOverlaySetting());

            Assert.NotNull(setting);

            Assert.Equal(USER_ID, setting.UserId);
            Assert.Equal(TEXT_COLOR, setting.TextColor);

            Assert.Equal(IS_LOGO, setting.IsLogo);
            Assert.Equal(LOGO_LOCATION_X, setting.LogoLocationX);
            Assert.Equal(LOGO_LOCATION_Y, setting.LogoLocationY);

            Assert.Equal(IS_CLAN_NAME, setting.IsClanName);
            Assert.Equal(CLAN_NAME_LOCATION_X, setting.ClanNameLocationX);
            Assert.Equal(CLAN_NAME_LOCATION_Y, setting.ClanNameLocationY);

            Assert.Equal(IS_TOTAL_STARS, setting.IsTotalStars);
            Assert.Equal(TOTAL_STARS_LOCATION_X, setting.TotalStarsLocationX);
            Assert.Equal(TOTAL_STARS_LOCATION_Y, setting.TotalStarsLocationY);

            Assert.Equal(IS_TOTAL_PERCENTAGE, setting.IsTotalPercentage);
            Assert.Equal(TOTAL_PERCENTAGE_LOCATION_X, setting.TotalPercentageLocationX);
            Assert.Equal(TOTAL_PERCENTAGE_LOCATION_Y, setting.TotalPercentageLocationY);

            Assert.Equal(IS_AVERAGE_DURATION, setting.IsAverageDuration);
            Assert.Equal(AVERAGE_DURATION_LOCATION_X, setting.AverageDurationLocationX);
            Assert.Equal(AVERAGE_DURATION_LOCATION_Y, setting.AverageDurationLocationY);

            Assert.Equal(IS_PLAYER_DETAILS, setting.IsPlayerDetails);
            Assert.Equal(PLAYER_DETAILS_LOCATION_X, setting.PlayerDetailsLocationX);
            Assert.Equal(PLAYER_DETAILS_LOCATION_Y, setting.PlayerDetailsLocationY);

            Assert.Equal(MIRROR_REFLECTION, setting.MirrorReflection);
            Assert.NotEqual(DateTimeOffset.MinValue, setting.CreatedAt);
            Assert.Equal(setting.CreatedAt, setting.UpdatedAt);
        }

        [Fact]
        [TestOrder(2)]
        public void WhenGetAllOverlaySettings_ThenReturnsOverlaySettings()
        {
            List<OverlaySetting> settings = _repository.GetAll();

            OverlaySetting setting = Assert.Single(settings);

            Assert.Equal(USER_ID, setting.UserId);
            Assert.Equal(TEXT_COLOR, setting.TextColor);

            Assert.Equal(IS_LOGO, setting.IsLogo);
            Assert.Equal(LOGO_LOCATION_X, setting.LogoLocationX);
            Assert.Equal(LOGO_LOCATION_Y, setting.LogoLocationY);

            Assert.Equal(IS_CLAN_NAME, setting.IsClanName);
            Assert.Equal(CLAN_NAME_LOCATION_X, setting.ClanNameLocationX);
            Assert.Equal(CLAN_NAME_LOCATION_Y, setting.ClanNameLocationY);

            Assert.Equal(IS_TOTAL_STARS, setting.IsTotalStars);
            Assert.Equal(TOTAL_STARS_LOCATION_X, setting.TotalStarsLocationX);
            Assert.Equal(TOTAL_STARS_LOCATION_Y, setting.TotalStarsLocationY);

            Assert.Equal(IS_TOTAL_PERCENTAGE, setting.IsTotalPercentage);
            Assert.Equal(TOTAL_PERCENTAGE_LOCATION_X, setting.TotalPercentageLocationX);
            Assert.Equal(TOTAL_PERCENTAGE_LOCATION_Y, setting.TotalPercentageLocationY);

            Assert.Equal(IS_AVERAGE_DURATION, setting.IsAverageDuration);
            Assert.Equal(AVERAGE_DURATION_LOCATION_X, setting.AverageDurationLocationX);
            Assert.Equal(AVERAGE_DURATION_LOCATION_Y, setting.AverageDurationLocationY);

            Assert.Equal(IS_PLAYER_DETAILS, setting.IsPlayerDetails);
            Assert.Equal(PLAYER_DETAILS_LOCATION_X, setting.PlayerDetailsLocationX);
            Assert.Equal(PLAYER_DETAILS_LOCATION_Y, setting.PlayerDetailsLocationY);

            Assert.Equal(MIRROR_REFLECTION, setting.MirrorReflection);
            Assert.NotEqual(DateTimeOffset.MinValue, setting.CreatedAt);
            Assert.Equal(setting.CreatedAt, setting.UpdatedAt);
        }

        [Fact]
        [TestOrder(3)]
        public void WhenGetOverlaySettingByUserId_ThenReturnsOverlaySetting()
        {
            OverlaySetting? setting = _repository.GetByUserId(USER_ID);

            Assert.NotNull(setting);

            Assert.Equal(USER_ID, setting.UserId);
            Assert.Equal(TEXT_COLOR, setting.TextColor);

            Assert.Equal(IS_LOGO, setting.IsLogo);
            Assert.Equal(LOGO_LOCATION_X, setting.LogoLocationX);
            Assert.Equal(LOGO_LOCATION_Y, setting.LogoLocationY);

            Assert.Equal(IS_CLAN_NAME, setting.IsClanName);
            Assert.Equal(CLAN_NAME_LOCATION_X, setting.ClanNameLocationX);
            Assert.Equal(CLAN_NAME_LOCATION_Y, setting.ClanNameLocationY);

            Assert.Equal(IS_TOTAL_STARS, setting.IsTotalStars);
            Assert.Equal(TOTAL_STARS_LOCATION_X, setting.TotalStarsLocationX);
            Assert.Equal(TOTAL_STARS_LOCATION_Y, setting.TotalStarsLocationY);

            Assert.Equal(IS_TOTAL_PERCENTAGE, setting.IsTotalPercentage);
            Assert.Equal(TOTAL_PERCENTAGE_LOCATION_X, setting.TotalPercentageLocationX);
            Assert.Equal(TOTAL_PERCENTAGE_LOCATION_Y, setting.TotalPercentageLocationY);

            Assert.Equal(IS_AVERAGE_DURATION, setting.IsAverageDuration);
            Assert.Equal(AVERAGE_DURATION_LOCATION_X, setting.AverageDurationLocationX);
            Assert.Equal(AVERAGE_DURATION_LOCATION_Y, setting.AverageDurationLocationY);

            Assert.Equal(IS_PLAYER_DETAILS, setting.IsPlayerDetails);
            Assert.Equal(PLAYER_DETAILS_LOCATION_X, setting.PlayerDetailsLocationX);
            Assert.Equal(PLAYER_DETAILS_LOCATION_Y, setting.PlayerDetailsLocationY);

            Assert.Equal(MIRROR_REFLECTION, setting.MirrorReflection);
            Assert.NotEqual(DateTimeOffset.MinValue, setting.CreatedAt);
            Assert.Equal(setting.CreatedAt, setting.UpdatedAt);
        }

        [Fact]
        [TestOrder(4)]
        public void WhenGetOverlaySettingByUserId_ThenReturnsNull()
        {
            Assert.Null(_repository.GetByUserId(USER_ID + 1));
        }

        [Fact]
        [TestOrder(5)]
        public void WhenUpdateOverlaySetting_ThenReturnsTrue()
        {
            OverlaySetting? setting = _repository.GetByUserId(USER_ID);

            Assert.NotNull(setting);

            setting.TextColor = TEXT_COLOR_UPDATED;

            Assert.True(_repository.Update(setting));
        }

        [Fact]
        [TestOrder(6)]
        public void WhenDeleteOverlaySetting_ThenReturnsTrue()
        {
            OverlaySetting? setting = _repository.GetByUserId(USER_ID);

            Assert.NotNull(setting);
            Assert.True(_repository.Delete(setting));
            Assert.Empty(_repository.GetAll());
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static OverlaySetting CreateOverlaySetting()
        {
            return new(USER_ID)
            {
                TextColor = TEXT_COLOR,
                IsLogo = IS_LOGO,
                LogoLocationX = LOGO_LOCATION_X,
                LogoLocationY = LOGO_LOCATION_Y,
                IsClanName = IS_CLAN_NAME,
                ClanNameLocationX = CLAN_NAME_LOCATION_X,
                ClanNameLocationY = CLAN_NAME_LOCATION_Y,
                IsTotalStars = IS_TOTAL_STARS,
                TotalStarsLocationX = TOTAL_STARS_LOCATION_X,
                TotalStarsLocationY = TOTAL_STARS_LOCATION_Y,
                IsTotalPercentage = IS_TOTAL_PERCENTAGE,
                TotalPercentageLocationX = TOTAL_PERCENTAGE_LOCATION_X,
                TotalPercentageLocationY = TOTAL_PERCENTAGE_LOCATION_Y,
                IsAverageDuration = IS_AVERAGE_DURATION,
                AverageDurationLocationX = AVERAGE_DURATION_LOCATION_X,
                AverageDurationLocationY = AVERAGE_DURATION_LOCATION_Y,
                IsPlayerDetails = IS_PLAYER_DETAILS,
                PlayerDetailsLocationX = PLAYER_DETAILS_LOCATION_X,
                PlayerDetailsLocationY = PLAYER_DETAILS_LOCATION_Y,
                MirrorReflection = MIRROR_REFLECTION,
            };
        }
    }
}
