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
        private static readonly Guid USER_ID = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560d");
        private static readonly Guid USER_ID_2 = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560e");
        private const int LOCATION_X = 100;
        private const int LOCATION_X_UPDATED = 150;
        private const int LOCATION_Y = 150;
        private const int LOCATION_Y_UPDATED = 100;
        private const int WIDTH = 600;
        private const int WIDTH_UPDATED = 900;
        private const int HEIGHT = 900;
        private const int HEIGHT_UPDATED = 600;
        private const bool IS_USED = true;

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
            Assert.Equal(USER_ID, image.UserId);
            Assert.Equal(LOCATION_X, image.LocationX);
            Assert.Equal(LOCATION_Y, image.LocationY);
            Assert.Equal(WIDTH, image.Width);
            Assert.Equal(HEIGHT, image.Height);
            Assert.Equal(IS_USED, image.IsUsed);
        }

        [Fact]
        [TestOrder(2)]
        public void WhenGetAllImagesUsedByUserId_ThenReturnsImages()
        {
            List<Image> images = _repository.GetUsedByUserId(USER_ID);

            Image image = Assert.Single(images);

            Assert.Equal(NAME.ToUpper(), image.Name);
            Assert.Equal(USER_ID, image.UserId);
            Assert.Equal(LOCATION_X, image.LocationX);
            Assert.Equal(LOCATION_Y, image.LocationY);
            Assert.Equal(WIDTH, image.Width);
            Assert.Equal(HEIGHT, image.Height);
            Assert.Equal(IS_USED, image.IsUsed);
        }

        [Fact]
        [TestOrder(3)]
        public void WhenGetAllImagesByUserId_ThenReturnsEmpty()
        {
            Assert.Empty(_repository.GetByUserId(USER_ID_2));
        }

        [Fact]
        [TestOrder(4)]
        public void WhenUpdateImage_ThenReturnsTrue()
        {
            Image? image = _repository.GetByUserIdAndName(USER_ID, NAME);

            Assert.NotNull(image);

            image.LocationX = LOCATION_X_UPDATED;
            image.LocationY = LOCATION_Y_UPDATED;
            image.Width = WIDTH_UPDATED;
            image.Height = HEIGHT_UPDATED;
            image.IsUsed = !IS_USED;

            Assert.True(_repository.Update(image));
            Assert.Empty(_repository.GetUsedByUserId(USER_ID));
        }

        [Fact]
        [TestOrder(5)]
        public void WhenGetImageById_ThenReturnsImage()
        {
            Image? image = _repository.GetByUserIdAndName(USER_ID, NAME);

            Assert.NotNull(image);

            Assert.Equal(NAME.ToUpper(), image.Name);
            Assert.Equal(USER_ID, image.UserId);
            Assert.Equal(LOCATION_X_UPDATED, image.LocationX);
            Assert.Equal(LOCATION_Y_UPDATED, image.LocationY);
            Assert.Equal(WIDTH_UPDATED, image.Width);
            Assert.Equal(HEIGHT_UPDATED, image.Height);
            Assert.Equal(!IS_USED, image.IsUsed);
        }

        [Fact]
        [TestOrder(6)]
        public void WhenGetImageById_ThenReturnsNull()
        {
            Assert.Null(_repository.GetByUserIdAndName(USER_ID_2, NAME));
        }

        [Fact]
        [TestOrder(7)]
        public void WhenDeleteImage_ThenReturnsTrue()
        {
            Image? image = _repository.GetByUserIdAndName(USER_ID, NAME);

            Assert.NotNull(image);
            Assert.True(_repository.Delete(image));
            Assert.Empty(_repository.GetByUserId(USER_ID));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static Image CreateImage()
        {
            return new(USER_ID, NAME)
            {
                LocationX = LOCATION_X,
                LocationY = LOCATION_Y,
                Width = WIDTH,
                Height = HEIGHT,
                IsUsed = IS_USED,
            };
        }
    }
}
