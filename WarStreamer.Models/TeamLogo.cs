using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Models
{
    [PrimaryKey(nameof(TeamName), nameof(UserId))]
    public class TeamLogo : Entity
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [MaxLength(50)]
        public string TeamName { get; set; } = null!;

        [Precision(30, 0)]
        public decimal UserId { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        /* * * * * * * * * * * * * * * * * *\
        |*            SHORTCUTS            *|
        \* * * * * * * * * * * * * * * * * */

        public User User { get; set; } = null!;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*             OVERRIDE            *|
        \* * * * * * * * * * * * * * * * * */

        public override void CopyTo(ref Entity entity)
        {
            if (entity is TeamLogo logo)
            {
                logo.Width = Width;
                logo.Height = Height;
                logo.UpdatedAt = UpdatedAt;
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
                TeamLogo? logo = obj as TeamLogo;

                return logo?.TeamName.ToLower() == TeamName.ToLower() && logo.UserId == UserId;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TeamName.ToLower().GetHashCode(), UserId);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                         OPERATORS OVERLOAD                        *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public static bool operator ==(TeamLogo? x, TeamLogo? y)
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

        public static bool operator !=(TeamLogo? x, TeamLogo? y) => !(x == y);
    }
}
