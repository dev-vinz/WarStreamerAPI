using WarStreamer.Commons.Extensions;
using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class OverlaySettingTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const string USER_ID_ONE = "1";
        private const string USER_ID_TWO = "2";
        private static readonly Guid FONT_ID = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560d");
        private static readonly Guid FONT_ID_2 = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560c");
        private const string TEXT_COLOR = "#000000";
        private const bool IS_LOGO = true;
        private const int LOGO_SIZE = 8;
        private const int LOGO_LOCATION_X = 100;
        private const int LOGO_LOCATION_Y = 100;
        private const bool IS_CLAN_NAME = true;
        private const int CLAN_NAME_SIZE = 10;
        private const int CLAN_NAME_LOCATION_X = 400;
        private const int CLAN_NAME_LOCATION_Y = 400;
        private const bool IS_TOTAL_STARS = true;
        private const int TOTAL_STARS_SIZE = 12;
        private const int TOTAL_STARS_LOCATION_X = 200;
        private const int TOTAL_STARS_LOCATION_Y = 200;
        private const bool IS_TOTAL_PERCENTAGE = true;
        private const int TOTAL_PERCENTAGE_SIZE = 14;
        private const int TOTAL_PERCENTAGE_LOCATION_X = 300;
        private const int TOTAL_PERCENTAGE_LOCATION_Y = 300;
        private const bool IS_AVERAGE_DURATION = true;
        private const int AVERAGE_DURATION_SIZE = 16;
        private const int AVERAGE_DURATION_LOCATION_X = 500;
        private const int AVERAGE_DURATION_LOCATION_Y = 500;
        private const bool IS_PLAYER_DETAILS = true;
        private const int PLAYER_DETAILS_SIZE = 18;
        private const int PLAYER_DETAILS_LOCATION_X = 600;
        private const int PLAYER_DETAILS_LOCATION_Y = 600;
        private const bool IS_LAST_ATTACK_TO_WIN = true;
        private const int LAST_ATTACK_TO_WIN_SIZE = 20;
        private const int LAST_ATTACK_TO_WIN_LOCATION_X = 700;
        private const int LAST_ATTACK_TO_WIN_LOCATION_Y = 700;
        private const bool IS_HEROES_EQUIPMENTS = true;
        private const int HEROES_EQUIPMENTS_SIZE = 40;
        private const int HEROES_EQUIPMENTS_LOCATION_X = 800;
        private const int HEROES_EQUIPMENTS_LOCATION_Y = 800;
        private const bool MIRROR_REFLECTION = false;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        public void WhenCreatingOverlaySetting_ThenOverlaySettingReturned()
        {
            OverlaySetting setting = CreateOverlaySettingOne();

            Assert.NotNull(setting);

            Assert.Equal(Guid.Empty.ParseDiscordId(USER_ID_ONE), setting.UserId);
            Assert.Equal(FONT_ID, setting.FontId);
            Assert.Equal(TEXT_COLOR, setting.TextColor);

            Assert.Equal(IS_LOGO, setting.IsLogo);
            Assert.Equal(LOGO_SIZE, setting.LogoSize);
            Assert.Equal(LOGO_LOCATION_X, setting.LogoLocationX);
            Assert.Equal(LOGO_LOCATION_Y, setting.LogoLocationY);

            Assert.Equal(IS_CLAN_NAME, setting.IsClanName);
            Assert.Equal(CLAN_NAME_SIZE, setting.ClanNameSize);
            Assert.Equal(CLAN_NAME_LOCATION_X, setting.ClanNameLocationX);
            Assert.Equal(CLAN_NAME_LOCATION_Y, setting.ClanNameLocationY);

            Assert.Equal(IS_TOTAL_STARS, setting.IsTotalStars);
            Assert.Equal(TOTAL_STARS_SIZE, setting.TotalStarsSize);
            Assert.Equal(TOTAL_STARS_LOCATION_X, setting.TotalStarsLocationX);
            Assert.Equal(TOTAL_STARS_LOCATION_Y, setting.TotalStarsLocationY);

            Assert.Equal(IS_TOTAL_PERCENTAGE, setting.IsTotalPercentage);
            Assert.Equal(TOTAL_PERCENTAGE_SIZE, setting.TotalPercentageSize);
            Assert.Equal(TOTAL_PERCENTAGE_LOCATION_X, setting.TotalPercentageLocationX);
            Assert.Equal(TOTAL_PERCENTAGE_LOCATION_Y, setting.TotalPercentageLocationY);

            Assert.Equal(IS_AVERAGE_DURATION, setting.IsAverageDuration);
            Assert.Equal(AVERAGE_DURATION_SIZE, setting.AverageDurationSize);
            Assert.Equal(AVERAGE_DURATION_LOCATION_X, setting.AverageDurationLocationX);
            Assert.Equal(AVERAGE_DURATION_LOCATION_Y, setting.AverageDurationLocationY);

            Assert.Equal(IS_PLAYER_DETAILS, setting.IsPlayerDetails);
            Assert.Equal(PLAYER_DETAILS_SIZE, setting.PlayerDetailsSize);
            Assert.Equal(PLAYER_DETAILS_LOCATION_X, setting.PlayerDetailsLocationX);
            Assert.Equal(PLAYER_DETAILS_LOCATION_Y, setting.PlayerDetailsLocationY);

            Assert.Equal(IS_LAST_ATTACK_TO_WIN, setting.IsLastAttackToWin);
            Assert.Equal(LAST_ATTACK_TO_WIN_SIZE, setting.LastAttackToWinSize);
            Assert.Equal(LAST_ATTACK_TO_WIN_LOCATION_X, setting.LastAttackToWinLocationX);
            Assert.Equal(LAST_ATTACK_TO_WIN_LOCATION_Y, setting.LastAttackToWinLocationY);

            Assert.Equal(IS_HEROES_EQUIPMENTS, setting.IsHeroesEquipments);
            Assert.Equal(HEROES_EQUIPMENTS_SIZE, setting.HeroesEquipmentsSize);
            Assert.Equal(HEROES_EQUIPMENTS_LOCATION_X, setting.HeroesEquipmentLocationX);
            Assert.Equal(HEROES_EQUIPMENTS_LOCATION_Y, setting.HeroesEquipmentLocationY);

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

            Assert.Equal(setting.FontId, copySetting.FontId);
            Assert.Equal(setting.TextColor, copySetting.TextColor);

            Assert.Equal(setting.IsLogo, copySetting.IsLogo);
            Assert.Equal(setting.LogoSize, copySetting.LogoSize);
            Assert.Equal(setting.LogoLocationX, copySetting.LogoLocationX);
            Assert.Equal(setting.LogoLocationY, copySetting.LogoLocationY);

            Assert.Equal(setting.IsClanName, copySetting.IsClanName);
            Assert.Equal(setting.ClanNameSize, copySetting.ClanNameSize);
            Assert.Equal(setting.ClanNameLocationX, copySetting.ClanNameLocationX);
            Assert.Equal(setting.ClanNameLocationY, copySetting.ClanNameLocationY);

            Assert.Equal(setting.IsTotalStars, copySetting.IsTotalStars);
            Assert.Equal(setting.TotalStarsSize, copySetting.TotalStarsSize);
            Assert.Equal(setting.TotalStarsLocationX, copySetting.TotalStarsLocationX);
            Assert.Equal(setting.TotalStarsLocationY, copySetting.TotalStarsLocationY);

            Assert.Equal(setting.IsTotalPercentage, copySetting.IsTotalPercentage);
            Assert.Equal(setting.TotalPercentageSize, copySetting.TotalPercentageSize);
            Assert.Equal(setting.TotalPercentageLocationX, copySetting.TotalPercentageLocationX);
            Assert.Equal(setting.TotalPercentageLocationY, copySetting.TotalPercentageLocationY);

            Assert.Equal(setting.IsAverageDuration, copySetting.IsAverageDuration);
            Assert.Equal(setting.AverageDurationSize, copySetting.AverageDurationSize);
            Assert.Equal(setting.AverageDurationLocationX, copySetting.AverageDurationLocationX);
            Assert.Equal(setting.AverageDurationLocationY, copySetting.AverageDurationLocationY);

            Assert.Equal(setting.IsPlayerDetails, copySetting.IsPlayerDetails);
            Assert.Equal(setting.PlayerDetailsSize, copySetting.PlayerDetailsSize);
            Assert.Equal(setting.PlayerDetailsLocationX, copySetting.PlayerDetailsLocationX);
            Assert.Equal(setting.PlayerDetailsLocationY, copySetting.PlayerDetailsLocationY);

            Assert.Equal(setting.IsLastAttackToWin, copySetting.IsLastAttackToWin);
            Assert.Equal(setting.LastAttackToWinSize, copySetting.LastAttackToWinSize);
            Assert.Equal(setting.LastAttackToWinLocationX, copySetting.LastAttackToWinLocationX);
            Assert.Equal(setting.LastAttackToWinLocationY, copySetting.LastAttackToWinLocationY);

            Assert.Equal(setting.IsHeroesEquipments, copySetting.IsHeroesEquipments);
            Assert.Equal(setting.HeroesEquipmentsSize, copySetting.HeroesEquipmentsSize);
            Assert.Equal(setting.HeroesEquipmentLocationX, copySetting.HeroesEquipmentLocationX);
            Assert.Equal(setting.HeroesEquipmentLocationY, copySetting.HeroesEquipmentLocationY);

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
            return new(Guid.Empty.ParseDiscordId(USER_ID_ONE))
            {
                FontId = FONT_ID,
                TextColor = TEXT_COLOR,
                IsLogo = IS_LOGO,
                LogoSize = LOGO_SIZE,
                LogoLocationX = LOGO_LOCATION_X,
                LogoLocationY = LOGO_LOCATION_Y,
                IsClanName = IS_CLAN_NAME,
                ClanNameSize = CLAN_NAME_SIZE,
                ClanNameLocationX = CLAN_NAME_LOCATION_X,
                ClanNameLocationY = CLAN_NAME_LOCATION_Y,
                IsTotalStars = IS_TOTAL_STARS,
                TotalStarsSize = TOTAL_STARS_SIZE,
                TotalStarsLocationX = TOTAL_STARS_LOCATION_X,
                TotalStarsLocationY = TOTAL_STARS_LOCATION_Y,
                IsTotalPercentage = IS_TOTAL_PERCENTAGE,
                TotalPercentageSize = TOTAL_PERCENTAGE_SIZE,
                TotalPercentageLocationX = TOTAL_PERCENTAGE_LOCATION_X,
                TotalPercentageLocationY = TOTAL_PERCENTAGE_LOCATION_Y,
                IsAverageDuration = IS_AVERAGE_DURATION,
                AverageDurationSize = AVERAGE_DURATION_SIZE,
                AverageDurationLocationX = AVERAGE_DURATION_LOCATION_X,
                AverageDurationLocationY = AVERAGE_DURATION_LOCATION_Y,
                IsPlayerDetails = IS_PLAYER_DETAILS,
                PlayerDetailsSize = PLAYER_DETAILS_SIZE,
                PlayerDetailsLocationX = PLAYER_DETAILS_LOCATION_X,
                PlayerDetailsLocationY = PLAYER_DETAILS_LOCATION_Y,
                IsLastAttackToWin = IS_LAST_ATTACK_TO_WIN,
                LastAttackToWinSize = LAST_ATTACK_TO_WIN_SIZE,
                LastAttackToWinLocationX = LAST_ATTACK_TO_WIN_LOCATION_X,
                LastAttackToWinLocationY = LAST_ATTACK_TO_WIN_LOCATION_Y,
                IsHeroesEquipments = IS_HEROES_EQUIPMENTS,
                HeroesEquipmentsSize = HEROES_EQUIPMENTS_SIZE,
                HeroesEquipmentLocationX = HEROES_EQUIPMENTS_LOCATION_X,
                HeroesEquipmentLocationY = HEROES_EQUIPMENTS_LOCATION_Y,
                MirrorReflection = MIRROR_REFLECTION,
            };
        }

        private static OverlaySetting CreateOverlaySettingTwo()
        {
            return new(Guid.Empty.ParseDiscordId(USER_ID_TWO))
            {
                FontId = FONT_ID_2,
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
                IsLastAttackToWin = !IS_LAST_ATTACK_TO_WIN,
                IsHeroesEquipments = !IS_HEROES_EQUIPMENTS,
                MirrorReflection = !MIRROR_REFLECTION,
            };
        }
    }
}
