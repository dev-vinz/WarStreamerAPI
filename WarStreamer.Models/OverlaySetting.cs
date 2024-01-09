using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Models
{
    public class OverlaySetting : Entity
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Key]
        [Column("Id")]
        [Precision(30, 0)]
        public decimal UserId { get; private set; }

        public int? FontId { get; set; }

        public string TextColor { get; set; } = null!;

        public bool IsLogo { get; set; }

        public int? LogoSize { get; set; }

        public int? LogoLocationX { get; set; }

        public int? LogoLocationY { get; set; }

        public bool IsClanName { get; set; }

        public int? ClanNameSize { get; set; }

        public int? ClanNameLocationX { get; set; }

        public int? ClanNameLocationY { get; set; }

        public bool IsTotalStars { get; set; }

        public int? TotalStarsSize { get; set; }

        public int? TotalStarsLocationX { get; set; }

        public int? TotalStarsLocationY { get; set; }

        public bool IsTotalPercentage { get; set; }

        public int? TotalPercentageSize { get; set; }

        public int? TotalPercentageLocationX { get; set; }

        public int? TotalPercentageLocationY { get; set; }

        public bool IsAverageDuration { get; set; }

        public int? AverageDurationSize { get; set; }

        public int? AverageDurationLocationX { get; set; }

        public int? AverageDurationLocationY { get; set; }

        public bool IsPlayerDetails { get; set; }

        public int? PlayerDetailsSize { get; set; }

        public int? PlayerDetailsLocationX { get; set; }

        public int? PlayerDetailsLocationY { get; set; }

        public bool IsLastAttackToWin { get; set; }

        public int? LastAttackToWinSize { get; set; }

        public int? LastAttackToWinLocationX { get; set; }

        public int? LastAttackToWinLocationY { get; set; }

        public bool MirrorReflection { get; set; }

        /* * * * * * * * * * * * * * * * * *\
        |*            SHORTCUTS            *|
        \* * * * * * * * * * * * * * * * * */

        public User User { get; set; } = null!;

        public Font Font { get; set; } = null!;

        public ICollection<Image> Images { get; set; } = new List<Image>();

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public OverlaySetting(decimal userId)
        {
            // Inputs
            {
                UserId = userId;
            }
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*             OVERRIDE            *|
        \* * * * * * * * * * * * * * * * * */

        public override void CopyTo(ref Entity entity)
        {
            if (entity is OverlaySetting setting)
            {
                setting.FontId = FontId;
                setting.TextColor = TextColor;

                setting.IsLogo = IsLogo;
                setting.LogoSize = LogoSize;
                setting.LogoLocationX = LogoLocationX;
                setting.LogoLocationY = LogoLocationY;

                setting.IsClanName = IsClanName;
                setting.ClanNameSize = ClanNameSize;
                setting.ClanNameLocationX = ClanNameLocationX;
                setting.ClanNameLocationY = ClanNameLocationY;

                setting.IsTotalStars = IsTotalStars;
                setting.TotalStarsSize = TotalStarsSize;
                setting.TotalStarsLocationX = TotalStarsLocationX;
                setting.TotalStarsLocationY = TotalStarsLocationY;

                setting.IsTotalPercentage = IsTotalPercentage;
                setting.TotalPercentageSize = TotalPercentageSize;
                setting.TotalPercentageLocationX = TotalPercentageLocationX;
                setting.TotalPercentageLocationY = TotalPercentageLocationY;

                setting.IsAverageDuration = IsAverageDuration;
                setting.AverageDurationSize = AverageDurationSize;
                setting.AverageDurationLocationX = AverageDurationLocationX;
                setting.AverageDurationLocationY = AverageDurationLocationY;

                setting.IsPlayerDetails = IsPlayerDetails;
                setting.PlayerDetailsSize = PlayerDetailsSize;
                setting.PlayerDetailsLocationX = PlayerDetailsLocationX;
                setting.PlayerDetailsLocationY = PlayerDetailsLocationY;

                setting.IsLastAttackToWin = IsLastAttackToWin;
                setting.LastAttackToWinSize = LastAttackToWinSize;
                setting.LastAttackToWinLocationX = LastAttackToWinLocationX;
                setting.LastAttackToWinLocationY = LastAttackToWinLocationY;

                setting.MirrorReflection = MirrorReflection;
            }
            else
            {
                throw new ArgumentException("Cannot copy to a different type.");
            }
        }

        public override bool Equals(object? obj)
        {
            // Check for null and compare run-time types.
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                OverlaySetting? setting = obj as OverlaySetting;

                return setting?.UserId == UserId;
            }
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                         OPERATORS OVERLOAD                        *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public static bool operator ==(OverlaySetting? x, OverlaySetting? y)
        {
            if (x is null && y is null)
            {
                return true;
            }
            else
            {
                return x?.Equals(y) ?? false;
            }
        }

        public static bool operator !=(OverlaySetting? x, OverlaySetting? y) => !(x == y);
    }
}
