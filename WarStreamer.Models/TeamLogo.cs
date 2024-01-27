using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Models
{
    [PrimaryKey(nameof(UserId), nameof(TeamName))]
    public class TeamLogo : Entity
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserId { get; private set; }

        [MaxLength(50)]
        public string TeamName { get; private set; } = null!;

        public string[] ClanTags { get; set; }

        /* * * * * * * * * * * * * * * * * *\
        |*            SHORTCUTS            *|
        \* * * * * * * * * * * * * * * * * */

        public User User { get; set; } = null!;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public TeamLogo(Guid userId, string teamName)
        {
            // Inputs
            {
                UserId = userId;
                TeamName = teamName.ToUpper();
                ClanTags = [];
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
            if (entity is TeamLogo logo)
            {
                // Erase array
                ClanTags = new string[logo.ClanTags.Length];

                // And copy
                Array.Copy(logo.ClanTags, ClanTags, ClanTags.Length);
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

                return logo?.UserId.Equals(UserId)
                    ?? false
                        && logo.TeamName.Equals(
                            TeamName,
                            StringComparison.CurrentCultureIgnoreCase
                        );
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TeamName, UserId);
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
