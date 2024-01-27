using WarStreamer.Commons.Tools;

namespace WarStreamer.ViewModels
{
    public class ImageViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _userId;
        private readonly string _name;
        private Location2D _location;
        private int _width;
        private int _height;
        private bool _isUsed;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string UserId
        {
            get => _userId;
        }

        public string Name
        {
            get => _name;
        }

        public Location2D Location
        {
            get => _location;
            set => _location = value;
        }

        public int Width
        {
            get => _width;
            set => _width = value;
        }

        public int Height
        {
            get => _height;
            set => _height = value;
        }

        public bool IsUsed
        {
            get => _isUsed;
            set => _isUsed = value;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public ImageViewModel(string userId, string name)
        {
            // Inputs
            {
                _userId = userId;
                _name = name;
                _location = new(0, 0);
            }
        }
    }
}
