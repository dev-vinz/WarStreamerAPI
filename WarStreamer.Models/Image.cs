using Microsoft.EntityFrameworkCore;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Models
{
    [PrimaryKey(nameof(OverlaySettingId), nameof(Name))]
    public class Image : Entity
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Precision(30, 0)]
        public decimal OverlaySettingId { get; private set; }

        public string Name { get; private set; }

        public int LocationX { get; set; }

        public int LocationY { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        /* * * * * * * * * * * * * * * * * *\
        |*            SHORTCUTS            *|
        \* * * * * * * * * * * * * * * * * */

        public OverlaySetting OverlaySetting { get; set; } = null!;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public Image(decimal overlaySettingId, string name)
        {
            // Inputs
            {
                OverlaySettingId = overlaySettingId;
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
                image.UpdatedAt = UpdatedAt;
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

                return image?.OverlaySettingId == OverlaySettingId && image.Name.ToUpper() == Name;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OverlaySettingId, Name);
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
