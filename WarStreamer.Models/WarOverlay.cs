using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Models
{
    [PrimaryKey(nameof(UserId), nameof(Id))]
    public class WarOverlay : Entity
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Precision(30, 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal UserId { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; private set; }

        public string ClanTag { get; private set; }

        public DateTimeOffset LastCheckout { get; set; }

        public bool IsEnded { get; set; }

        /* * * * * * * * * * * * * * * * * *\
        |*            SHORTCUTS            *|
        \* * * * * * * * * * * * * * * * * */

        public User User { get; set; } = null!;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public WarOverlay(decimal userId, int id, string clanTag)
        {
            // Inputs
            {
                UserId = userId;
                Id = id;
                ClanTag = clanTag;
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
            if (entity is WarOverlay overlay)
            {
                overlay.LastCheckout = LastCheckout;
                overlay.IsEnded = IsEnded;
                overlay.UpdatedAt = UpdatedAt;
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
                WarOverlay? overlay = obj as WarOverlay;

                return overlay?.UserId == UserId && overlay.Id == Id;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, Id);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                         OPERATORS OVERLOAD                        *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public static bool operator ==(WarOverlay? x, WarOverlay? y)
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

        public static bool operator !=(WarOverlay? x, WarOverlay? y) => !(x == y);
    }
}
