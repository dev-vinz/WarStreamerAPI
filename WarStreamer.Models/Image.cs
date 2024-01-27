using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Models
{
    [PrimaryKey(nameof(UserId), nameof(Name))]
    public class Image : Entity
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserId { get; private set; }

        public string Name { get; private set; }

        public int LocationX { get; set; }

        public int LocationY { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool IsUsed { get; set; }

        /* * * * * * * * * * * * * * * * * *\
        |*            SHORTCUTS            *|
        \* * * * * * * * * * * * * * * * * */

        public User User { get; set; } = null!;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public Image(Guid userId, string name)
        {
            // Inputs
            {
                UserId = userId;
                Name = name.ToUpper();
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
            if (entity is Image image)
            {
                image.LocationX = LocationX;
                image.LocationY = LocationY;
                image.Width = Width;
                image.Height = Height;
                image.IsUsed = IsUsed;
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
                Image? image = obj as Image;

                return (image?.UserId.Equals(UserId) ?? false)
                    && image.Name.Equals(Name, StringComparison.CurrentCultureIgnoreCase);
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, Name);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                         OPERATORS OVERLOAD                        *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public static bool operator ==(Image? x, Image? y)
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

        public static bool operator !=(Image? x, Image? y) => !(x == y);
    }
}
