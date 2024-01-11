using Microsoft.Extensions.DependencyInjection;
using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Tests.Tools;

namespace WarStreamer.Tests.Repositories
{
    [TestCaseOrderer("WarStreamer.Tests.Tools.OrderOrderer", "WarStreamer.Tests")]
    public class ImageRepositoryTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const string NAME = "Image";
        private const string OVERLAY_SETTING_ID = "0";
        private const int LOCATION_X = 100;
        private const int LOCATION_X_UPDATED = 150;
        private const int LOCATION_Y = 150;
        private const int LOCATION_Y_UPDATED = 100;
        private const int WIDTH = 600;
        private const int WIDTH_UPDATED = 900;
        private const int HEIGHT = 900;
        private const int HEIGHT_UPDATED = 600;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IImageRepository _repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public ImageRepositoryTests()
        {
            _repository = new ServiceTestCollection()
                .BuildServiceProvider()
                .GetRequiredService<IImageRepository>();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        [TestOrder(1)]
        public void WhenInsertImage_ThenReturnsAddedImage()
        {
            Image image = _repository.Save(CreateImage());

            Assert.NotNull(image);

            Assert.Equal(NAME.ToUpper(), image.Name);
            Assert.Equal(OVERLAY_SETTING_ID, image.OverlaySettingId);
            Assert.Equal(LOCATION_X, image.LocationX);
            Assert.Equal(LOCATION_Y, image.LocationY);
            Assert.Equal(WIDTH, image.Width);
            Assert.Equal(HEIGHT, image.Height);
        }

        [Fact]
        [TestOrder(2)]
        public void WhenGetAllImages_ThenReturnsImages()
        {
            List<Image> images = _repository.GetAll();

            Assert.Single(images);

            Image image = images.Single();

            Assert.Equal(NAME.ToUpper(), image.Name);
            Assert.Equal(OVERLAY_SETTING_ID, image.OverlaySettingId);
            Assert.Equal(LOCATION_X, image.LocationX);
            Assert.Equal(LOCATION_Y, image.LocationY);
            Assert.Equal(WIDTH, image.Width);
            Assert.Equal(HEIGHT, image.Height);
        }

        [Fact]
        [TestOrder(3)]
        public void WhenGetAllImagesByOverlaySettingId_ThenReturnsImages()
        {
            List<Image> images = _repository.GetByOverlaySettingId(OVERLAY_SETTING_ID);

            Image image = Assert.Single(images);

            Assert.Equal(NAME.ToUpper(), image.Name);
            Assert.Equal(OVERLAY_SETTING_ID, image.OverlaySettingId);
            Assert.Equal(LOCATION_X, image.LocationX);
            Assert.Equal(LOCATION_Y, image.LocationY);
            Assert.Equal(WIDTH, image.Width);
            Assert.Equal(HEIGHT, image.Height);
        }

        [Fact]
        [TestOrder(4)]
        public void WhenGetAllImagesByOverlaySettingId_ThenReturnsEmpty()
        {
            Assert.Empty(_repository.GetByOverlaySettingId(OVERLAY_SETTING_ID + 1));
        }

        [Fact]
        [TestOrder(5)]
        public void WhenUpdateImage_ThenReturnsTrue()
        {
            Image? image = _repository.GetByOverlaySettingIdAndName(OVERLAY_SETTING_ID, NAME);

            Assert.NotNull(image);

            image.LocationX = LOCATION_X_UPDATED;
            image.LocationY = LOCATION_Y_UPDATED;
            image.Width = WIDTH_UPDATED;
            image.Height = HEIGHT_UPDATED;

            Assert.True(_repository.Update(image));
        }

        [Fact]
        [TestOrder(6)]
        public void WhenGetImageById_ThenReturnsImage()
        {
            Image? image = _repository.GetByOverlaySettingIdAndName(OVERLAY_SETTING_ID, NAME);

            Assert.NotNull(image);

            Assert.Equal(NAME.ToUpper(), image.Name);
            Assert.Equal(OVERLAY_SETTING_ID, image.OverlaySettingId);
            Assert.Equal(LOCATION_X_UPDATED, image.LocationX);
            Assert.Equal(LOCATION_Y_UPDATED, image.LocationY);
            Assert.Equal(WIDTH_UPDATED, image.Width);
            Assert.Equal(HEIGHT_UPDATED, image.Height);
        }

        [Fact]
        [TestOrder(7)]
        public void WhenGetImageById_ThenReturnsNull()
        {
            Assert.Null(_repository.GetByOverlaySettingIdAndName(OVERLAY_SETTING_ID + 1, NAME));
        }

        [Fact]
        [TestOrder(8)]
        public void WhenDeleteImage_ThenReturnsTrue()
        {
            Image? image = _repository.GetByOverlaySettingIdAndName(OVERLAY_SETTING_ID, NAME);

            Assert.NotNull(image);
            Assert.True(_repository.Delete(image));
            Assert.Empty(_repository.GetAll());
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static Image CreateImage()
        {
            return new(OVERLAY_SETTING_ID, NAME)
            {
                LocationX = LOCATION_X,
                LocationY = LOCATION_Y,
                Width = WIDTH,
                Height = HEIGHT,
            };
        }
    }
}
