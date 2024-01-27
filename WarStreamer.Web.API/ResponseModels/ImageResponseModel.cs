namespace WarStreamer.Web.API.ResponseModels
{
    public class ImageResponseModel
    {
        public string UserId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public byte[] Image { get; set; } = null!;

        public int LocationX { get; set; }

        public int LocationY { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool IsUsed { get; set; }
    }
}
