using WarStreamer.Commons.Extensions;
using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class ImageTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const string NAME_ONE = "Image 1";
        private const string NAME_TWO = "Image 2";
        private const string OVERLAY_SETTING_ID = "0";
        private const int LOCATION_X = 100;
        private const int LOCATION_Y = 150;
        private const int WIDTH = 600;
        private const int HEIGHT = 900;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        public void WhenCreatingImage_ThenImageReturned()
        {
            Image image = CreateImageOne();

            Assert.NotNull(image);
            Assert.Equal(NAME_ONE.ToUpper(), image.Name);
            Assert.Equal(Guid.Empty.ParseDiscordId(OVERLAY_SETTING_ID), image.OverlaySettingId);
            Assert.Equal(LOCATION_X, image.LocationX);
            Assert.Equal(LOCATION_Y, image.LocationY);
            Assert.Equal(WIDTH, image.Width);
            Assert.Equal(HEIGHT, image.Height);
        }

        [Fact]
        public void WhenComparingSameImages_ThenImagesAreTheSame()
        {
            Image imageOne = CreateImageOne();
            Image imageTwo = CreateImageOne();

            Assert.NotNull(imageOne);
            Assert.NotNull(imageTwo);

            Assert.True(imageOne == imageTwo);
            Assert.True(imageOne.Equals(imageTwo));
            Assert.True(imageTwo.Equals(imageOne));
            Assert.False(imageOne != imageTwo);
            Assert.Equal(imageOne.GetHashCode(), imageTwo.GetHashCode());
        }

        [Fact]
        public void WhenComparingDifferentImages_ThenImagesAreDifferent()
        {
            Image imageOne = CreateImageOne();
            Image imageTwo = CreateImageTwo();

            Assert.NotNull(imageOne);
            Assert.NotNull(imageTwo);

            Assert.False(imageOne == imageTwo);
            Assert.False(imageOne.Equals(imageTwo));
            Assert.False(imageTwo.Equals(imageOne));
            Assert.True(imageOne != imageTwo);
            Assert.NotEqual(imageOne.GetHashCode(), imageTwo.GetHashCode());
        }

        [Fact]
        public void WhenCopyingImage_ThenImageCopied()
        {
            Image image = CreateImageOne();
            Entity copy = new Image(image.OverlaySettingId, image.Name);

            Assert.NotNull(image);

            image.CopyTo(ref copy);

            Image copyImage = (Image)copy;

            Assert.Equal(image.LocationX, copyImage.LocationX);
            Assert.Equal(image.LocationY, copyImage.LocationY);
            Assert.Equal(image.Width, copyImage.Width);
            Assert.Equal(image.Height, copyImage.Height);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static Image CreateImageOne()
        {
            return new(Guid.Empty.ParseDiscordId(OVERLAY_SETTING_ID), NAME_ONE)
            {
                LocationX = LOCATION_X,
                LocationY = LOCATION_Y,
                Width = WIDTH,
                Height = HEIGHT,
            };
        }

        private static Image CreateImageTwo()
        {
            return new(Guid.Empty.ParseDiscordId(OVERLAY_SETTING_ID), NAME_TWO)
            {
                LocationX = LOCATION_Y,
                LocationY = LOCATION_X,
                Width = HEIGHT,
                Height = WIDTH,
            };
        }
    }
}
