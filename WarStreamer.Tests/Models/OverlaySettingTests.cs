using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class OverlaySettingTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const decimal USER_ID_ONE = 1;
        private const decimal USER_ID_TWO = 2;
        private const string TEXT_COLOR = "#000000";
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
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        public void WhenCreatingOverlaySetting_ThenOverlaySettingReturned()
        {
            OverlaySetting setting = CreateOverlaySettingOne();

            Assert.NotNull(setting);

            Assert.Equal(USER_ID_ONE, setting.UserId);
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
        }

        [Fact]
        public void WhenComparingSameOverlaySettings_ThenOverlaySettingsAreTheSame()
        {
            OverlaySetting settingOne = CreateOverlaySettingOne();
            OverlaySetting settingTwo = CreateOverlaySettingOne();

            Assert.NotNull(settingOne);
            Assert.NotNull(settingTwo);

            Assert.True(settingOne == settingTwo);
            Assert.True(settingOne.Equals(settingTwo));
            Assert.True(settingTwo.Equals(settingOne));
            Assert.False(settingOne != settingTwo);
            Assert.Equal(settingOne.GetHashCode(), settingTwo.GetHashCode());
        }

        [Fact]
        public void WhenComparingDifferentOverlaySettings_ThenOverlaySettingsAreDifferent()
        {
            OverlaySetting settingOne = CreateOverlaySettingOne();
            OverlaySetting settingTwo = CreateOverlaySettingTwo();

            Assert.NotNull(settingOne);
            Assert.NotNull(settingTwo);

            Assert.False(settingOne == settingTwo);
            Assert.False(settingOne.Equals(settingTwo));
            Assert.False(settingTwo.Equals(settingOne));
            Assert.True(settingOne != settingTwo);
            Assert.NotEqual(settingOne.GetHashCode(), settingTwo.GetHashCode());
        }

        [Fact]
        public void WhenCopyingOverlaySetting_ThenOverlaySettingCopied()
        {
            OverlaySetting setting = CreateOverlaySettingOne();
            Entity copy = new OverlaySetting(setting.UserId);

            Assert.NotNull(setting);

            setting.CopyTo(ref copy);

            OverlaySetting copySetting = (OverlaySetting)copy;

            Assert.Equal(setting.TextColor, copySetting.TextColor);

            Assert.Equal(setting.IsLogo, copySetting.IsLogo);
            Assert.Equal(setting.LogoLocationX, copySetting.LogoLocationX);
            Assert.Equal(setting.LogoLocationY, copySetting.LogoLocationY);

            Assert.Equal(setting.IsClanName, copySetting.IsClanName);
            Assert.Equal(setting.ClanNameLocationX, copySetting.ClanNameLocationX);
            Assert.Equal(setting.ClanNameLocationY, copySetting.ClanNameLocationY);

            Assert.Equal(setting.IsTotalStars, copySetting.IsTotalStars);
            Assert.Equal(setting.TotalStarsLocationX, copySetting.TotalStarsLocationX);
            Assert.Equal(setting.TotalStarsLocationY, copySetting.TotalStarsLocationY);

            Assert.Equal(setting.IsTotalPercentage, copySetting.IsTotalPercentage);
            Assert.Equal(setting.TotalPercentageLocationX, copySetting.TotalPercentageLocationX);
            Assert.Equal(setting.TotalPercentageLocationY, copySetting.TotalPercentageLocationY);

            Assert.Equal(setting.IsAverageDuration, copySetting.IsAverageDuration);
            Assert.Equal(setting.AverageDurationLocationX, copySetting.AverageDurationLocationX);
            Assert.Equal(setting.AverageDurationLocationY, copySetting.AverageDurationLocationY);

            Assert.Equal(setting.IsPlayerDetails, copySetting.IsPlayerDetails);
            Assert.Equal(setting.PlayerDetailsLocationX, copySetting.PlayerDetailsLocationX);
            Assert.Equal(setting.PlayerDetailsLocationY, copySetting.PlayerDetailsLocationY);

            Assert.Equal(setting.MirrorReflection, copySetting.MirrorReflection);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static OverlaySetting CreateOverlaySettingOne()
        {
            return new(USER_ID_ONE)
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

        private static OverlaySetting CreateOverlaySettingTwo()
        {
            return new(USER_ID_TWO)
            {
                TextColor = TEXT_COLOR,
                IsLogo = !IS_LOGO,
                IsClanName = IS_CLAN_NAME,
                ClanNameLocationX = LOGO_LOCATION_X,
                ClanNameLocationY = LOGO_LOCATION_Y,
                IsTotalStars = IS_TOTAL_STARS,
                TotalStarsLocationX = TOTAL_STARS_LOCATION_X,
                TotalStarsLocationY = TOTAL_STARS_LOCATION_Y,
                IsTotalPercentage = IS_TOTAL_PERCENTAGE,
                TotalPercentageLocationX = TOTAL_PERCENTAGE_LOCATION_X,
                TotalPercentageLocationY = TOTAL_PERCENTAGE_LOCATION_Y,
                IsAverageDuration = !IS_AVERAGE_DURATION,
                IsPlayerDetails = !IS_PLAYER_DETAILS,
                MirrorReflection = !MIRROR_REFLECTION,
            };
        }
    }
}
