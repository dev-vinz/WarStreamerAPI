using Microsoft.Extensions.DependencyInjection;
using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Tests.Tools;

namespace WarStreamer.Tests.Repositories
{
    [TestCaseOrderer("WarStreamer.Tests.Tools.OrderOrderer", "WarStreamer.Tests")]
    public class WarOverlayRepositoryTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const decimal USER_ID = 1;
        private const int ID = 0;
        private const string CLAN_TAG = "#ABCDEFG";
        private const bool IS_ENDED = false;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWarOverlayRepository _repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public WarOverlayRepositoryTests()
        {
            _repository = new ServiceTestCollection()
                .BuildServiceProvider()
                .GetRequiredService<IWarOverlayRepository>();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        [TestOrder(1)]
        public void WhenInsertWarOverlay_ThenReturnsAddedWarOverlay()
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;

            WarOverlay overlay = _repository.Save(CreateWarOverlay(now));

            Assert.NotNull(overlay);

            Assert.Equal(USER_ID, overlay.UserId);
            Assert.Equal(ID, overlay.Id);
            Assert.Equal(CLAN_TAG, overlay.ClanTag);
            Assert.Equal(now, overlay.LastCheckout);
            Assert.Equal(IS_ENDED, overlay.IsEnded);
            Assert.NotEqual(DateTimeOffset.MinValue, overlay.CreatedAt);
            Assert.Equal(overlay.CreatedAt, overlay.UpdatedAt);
        }

        [Fact]
        [TestOrder(2)]
        public void WhenGetAllWarOverlays_ThenReturnsWarOverlays()
        {
            List<WarOverlay> overlays = _repository.GetAll();

            WarOverlay overlay = Assert.Single(overlays);

            Assert.Equal(USER_ID, overlay.UserId);
            Assert.Equal(ID, overlay.Id);
            Assert.Equal(CLAN_TAG, overlay.ClanTag);
            Assert.Equal(IS_ENDED, overlay.IsEnded);
            Assert.NotEqual(DateTimeOffset.MinValue, overlay.CreatedAt);
            Assert.Equal(overlay.CreatedAt, overlay.UpdatedAt);
        }

        [Fact]
        [TestOrder(3)]
        public void WhenGetWarOverlaysByUserId_ThenReturnsWarOverlays()
        {
            List<WarOverlay> overlays = _repository.GetByUserId(USER_ID);

            WarOverlay overlay = Assert.Single(overlays);

            Assert.Equal(USER_ID, overlay.UserId);
            Assert.Equal(ID, overlay.Id);
            Assert.Equal(CLAN_TAG, overlay.ClanTag);
            Assert.Equal(IS_ENDED, overlay.IsEnded);
            Assert.NotEqual(DateTimeOffset.MinValue, overlay.CreatedAt);
            Assert.Equal(overlay.CreatedAt, overlay.UpdatedAt);
        }

        [Fact]
        [TestOrder(4)]
        public void WhenGetWarOverlaysByUserId_ThenReturnsEmpty()
        {
            Assert.Empty(_repository.GetByUserId(USER_ID + 1));
        }

        [Fact]
        [TestOrder(5)]
        public void WhenUpdateWarOverlay_ThenReturnsTrue()
        {
            WarOverlay overlay = Assert.Single(_repository.GetByUserId(USER_ID));
            DateTimeOffset now = DateTimeOffset.UtcNow;

            overlay.LastCheckout = now;
            overlay.IsEnded = !IS_ENDED;

            Assert.True(_repository.Update(overlay));

            WarOverlay updatedOverlay = Assert.Single(_repository.GetAll());

            Assert.Equal(overlay.LastCheckout, updatedOverlay.LastCheckout);
            Assert.Equal(overlay.IsEnded, updatedOverlay.IsEnded);
            Assert.NotEqual(updatedOverlay.CreatedAt, updatedOverlay.UpdatedAt);
        }

        [Fact]
        [TestOrder(6)]
        public void WhenDeleteWarOverlay_ThenReturnsTrue()
        {
            WarOverlay overlay = Assert.Single(_repository.GetByUserId(USER_ID));

            Assert.True(_repository.Delete(overlay));
            Assert.Empty(_repository.GetAll());
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static WarOverlay CreateWarOverlay(DateTimeOffset dateTime)
        {
            return new WarOverlay
            {
                UserId = USER_ID,
                Id = ID,
                ClanTag = CLAN_TAG,
                LastCheckout = dateTime,
                IsEnded = IS_ENDED,
            };
        }
    }
}
