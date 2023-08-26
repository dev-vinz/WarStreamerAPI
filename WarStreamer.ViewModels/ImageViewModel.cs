namespace WarStreamer.ViewModels
{
    public class ImageViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly int _id;
        private readonly decimal _overlaySettingId;
        private int _locationX;
        private int _locationY;
        private int _width;
        private int _height;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public int Id { get => _id; }

        public decimal OverlaySettingId { get => _overlaySettingId; }

        public int LocationX { get => _locationX; set => _locationX = value; }

        public int LocationY { get => _locationY; set => _locationY = value; }

        public int Width { get => _width; set => _width = value; }

        public int Height { get => _height; set => _height = value; }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public ImageViewModel(int id, decimal overlaySettingId)
        {
            // Inputs
            {
                _id = id;
                _overlaySettingId = overlaySettingId;
            }
        }
    }
}
