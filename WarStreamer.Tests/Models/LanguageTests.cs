using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class LanguageTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private static readonly Guid ID_ONE = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560d");
        private static readonly Guid ID_TWO = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560c");
        private const string CULTURE_INFO = "fr-FR";
        private const string DISPLAY_VALUE = "Français";
        private const string SHORTCUT_VALUE = "fr";
        private const string FLAG_EMOJI = "🇫🇷";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        public void WhenCreatingLanguage_ThenLanguageReturned()
        {
            Language language = CreateLanguageOne();

            Assert.NotNull(language);
            Assert.Equal(CULTURE_INFO, language.CultureInfo);
            Assert.Equal(DISPLAY_VALUE, language.DisplayValue);
            Assert.Equal(SHORTCUT_VALUE, language.ShortcutValue);
            Assert.Equal(FLAG_EMOJI, language.FlagEmoji);
        }

        [Fact]
        public void WhenComparingSameLanguages_ThenLanguagesAreTheSame()
        {
            Language languageOne = CreateLanguageOne();
            Language languageTwo = CreateLanguageOne();

            Assert.NotNull(languageOne);
            Assert.NotNull(languageTwo);

            Assert.True(languageOne == languageTwo);
            Assert.True(languageOne.Equals(languageTwo));
            Assert.True(languageTwo.Equals(languageOne));
            Assert.False(languageOne != languageTwo);
            Assert.Equal(languageOne.GetHashCode(), languageTwo.GetHashCode());
        }

        [Fact]
        public void WhenComparingDifferentLanguages_ThenLanguagesAreDifferent()
        {
            Language languageOne = CreateLanguageOne();
            Language languageTwo = CreateLanguageTwo();

            Assert.NotNull(languageOne);
            Assert.NotNull(languageTwo);

            Assert.False(languageOne == languageTwo);
            Assert.False(languageOne.Equals(languageTwo));
            Assert.False(languageTwo.Equals(languageOne));
            Assert.True(languageOne != languageTwo);
            Assert.NotEqual(languageOne.GetHashCode(), languageTwo.GetHashCode());
        }

        [Fact]
        public void WhenCopyingLanguage_ThenThrowError()
        {
            Language language = CreateLanguageOne();
            Entity copy = CreateLanguageTwo();

            Assert.NotNull(language);

            Assert.Throws<InvalidOperationException>(() => language.CopyTo(ref copy));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static Language CreateLanguageOne()
        {
            return new(ID_ONE, CULTURE_INFO, DISPLAY_VALUE, SHORTCUT_VALUE, FLAG_EMOJI);
        }

        private static Language CreateLanguageTwo()
        {
            return new(ID_TWO, SHORTCUT_VALUE, CULTURE_INFO, FLAG_EMOJI, DISPLAY_VALUE);
        }
    }
}
