using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class WarOverlayTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const decimal USER_ID = 1;
        private const int ID_ONE = 0;
        private const int ID_TWO = 1;
        private const string CLAN_TAG = "#ABCDEFG";
        private const bool IS_ENDED = false;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        public void WhenCreatingWarOverlay_ThenWarOverlayReturned()
        {
            DateTimeOffset UTC_NOW = DateTimeOffset.UtcNow;

            WarOverlay overlay = CreateWarOverlayOne();
            overlay.LastCheckout = UTC_NOW;

            Assert.NotNull(overlay);
            Assert.Equal(USER_ID, overlay.UserId);
            Assert.Equal(ID_ONE, overlay.Id);
            Assert.Equal(CLAN_TAG, overlay.ClanTag);
            Assert.Equal(UTC_NOW, overlay.LastCheckout);
            Assert.Equal(IS_ENDED, overlay.IsEnded);
        }

        [Fact]
        public void WhenComparingSameWarOverlays_ThenWarOverlaysAreTheSame()
        {
            WarOverlay overlayOne = CreateWarOverlayOne();
            WarOverlay overlayTwo = CreateWarOverlayOne();

            Assert.NotNull(overlayOne);
            Assert.NotNull(overlayTwo);

            Assert.True(overlayOne == overlayTwo);
            Assert.True(overlayOne.Equals(overlayTwo));
            Assert.True(overlayTwo.Equals(overlayOne));
            Assert.False(overlayOne != overlayTwo);
            Assert.Equal(overlayOne.GetHashCode(), overlayTwo.GetHashCode());
        }

        [Fact]
        public void WhenComparingDifferentWarOverlays_ThenWarOverlaysAreDifferent()
        {
            WarOverlay overlayOne = CreateWarOverlayOne();
            WarOverlay overlayTwo = CreateWarOverlayTwo();

            Assert.NotNull(overlayOne);
            Assert.NotNull(overlayTwo);

            Assert.False(overlayOne == overlayTwo);
            Assert.False(overlayOne.Equals(overlayTwo));
            Assert.False(overlayTwo.Equals(overlayOne));
            Assert.True(overlayOne != overlayTwo);
            Assert.NotEqual(overlayOne.GetHashCode(), overlayTwo.GetHashCode());
        }

        [Fact]
        public void WhenCopyingWarOverlay_ThenWarOverlayCopied()
        {
            DateTimeOffset UTC_NOW = DateTimeOffset.UtcNow;

            WarOverlay overlay = CreateWarOverlayOne();
            overlay.LastCheckout = UTC_NOW;

            Entity copy = new WarOverlay();

            Assert.NotNull(overlay);

            overlay.CopyTo(ref copy);

            WarOverlay copyOverlay = (WarOverlay)copy;

            Assert.Equal(overlay.LastCheckout, copyOverlay.LastCheckout);
            Assert.Equal(overlay.IsEnded, copyOverlay.IsEnded);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static WarOverlay CreateWarOverlayOne()
        {
            return new WarOverlay
            {
                UserId = USER_ID,
                Id = ID_ONE,
                ClanTag = CLAN_TAG,
                IsEnded = IS_ENDED,
            };
        }

        private static WarOverlay CreateWarOverlayTwo()
        {
            return new WarOverlay
            {
                UserId = USER_ID,
                Id = ID_TWO,
                ClanTag = CLAN_TAG,
                IsEnded = !IS_ENDED,
            };
        }
    }
}
