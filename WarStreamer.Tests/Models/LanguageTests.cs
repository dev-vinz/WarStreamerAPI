using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class LanguageTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const int ID_ONE = 1;
        private const int ID_TWO = 2;
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
            Assert.Equal(ID_ONE, language.Id);
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
            Entity copy = new Language();

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
            return new Language
            {
                Id = ID_ONE,
                CultureInfo = CULTURE_INFO,
                DisplayValue = DISPLAY_VALUE,
                ShortcutValue = SHORTCUT_VALUE,
                FlagEmoji = FLAG_EMOJI,
            };
        }

        private static Language CreateLanguageTwo()
        {
            return new Language
            {
                Id = ID_TWO,
                CultureInfo = SHORTCUT_VALUE,
                DisplayValue = CULTURE_INFO,
                ShortcutValue = FLAG_EMOJI,
                FlagEmoji = DISPLAY_VALUE,
            };
        }
    }
}
