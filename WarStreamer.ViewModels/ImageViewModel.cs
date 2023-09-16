using WarStreamer.Commons.Tools;

namespace WarStreamer.ViewModels
{
    public class ImageViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _overlaySettingId;
        private readonly string _name;
        private Location2D _location;
        private int _width;
        private int _height;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string OverlaySettingId { get => _overlaySettingId; }

        public string Name { get => _name; }

        public Location2D Location { get => _location; set => _location = value; }

        public int Width { get => _width; set => _width = value; }

        public int Height { get => _height; set => _height = value; }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public ImageViewModel(string overlaySettingId, string name)
        {
            // Inputs
            {
                _overlaySettingId = overlaySettingId;
                _name = name;
                _location = new(0, 0);
            }
        }
    }
}
