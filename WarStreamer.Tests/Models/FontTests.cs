using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class FontTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private static readonly Guid ID_ONE = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560d");
        private static readonly Guid ID_TWO = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560c");
        private const string DISPLAY_NAME = "Quicksand";
        private const string FAMILY_NAME = "Supercell-Magic";
        private const string FILE_NAME = "Quicksand Light.ttf";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        public void WhenCreatingLanguage_ThenFontReturned()
        {
            Font font = CreateFontOne();

            Assert.NotNull(font);
            Assert.Equal(DISPLAY_NAME, font.DisplayName);
            Assert.Equal(FAMILY_NAME, font.FamilyName);
            Assert.Equal(FILE_NAME, font.FileName);
        }

        [Fact]
        public void WhenComparingSameFonts_ThenFontsAreTheSame()
        {
            Font fontOne = CreateFontOne();
            Font fontTwo = CreateFontOne();

            Assert.NotNull(fontOne);
            Assert.NotNull(fontTwo);

            Assert.True(fontOne == fontTwo);
            Assert.True(fontOne.Equals(fontTwo));
            Assert.True(fontTwo.Equals(fontOne));
            Assert.False(fontOne != fontTwo);
            Assert.Equal(fontOne.GetHashCode(), fontTwo.GetHashCode());
        }

        [Fact]
        public void WhenComparingDifferentFonts_ThenFontsAreDifferent()
        {
            Font fontOne = CreateFontOne();
            Font fontTwo = CreateFontTwo();

            Assert.NotNull(fontOne);
            Assert.NotNull(fontTwo);

            Assert.False(fontOne == fontTwo);
            Assert.False(fontOne.Equals(fontTwo));
            Assert.False(fontTwo.Equals(fontOne));
            Assert.True(fontOne != fontTwo);
            Assert.NotEqual(fontOne.GetHashCode(), fontTwo.GetHashCode());
        }

        [Fact]
        public void WhenCopyingFont_ThenThrowError()
        {
            Font font = CreateFontOne();
            Entity copy = CreateFontTwo();

            Assert.NotNull(font);

            Assert.Throws<InvalidOperationException>(() => font.CopyTo(ref copy));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static Font CreateFontOne()
        {
            return new(ID_ONE, DISPLAY_NAME, FAMILY_NAME, FILE_NAME);
        }

        private static Font CreateFontTwo()
        {
            return new(ID_TWO, FILE_NAME, DISPLAY_NAME, FAMILY_NAME);
        }
    }
}
