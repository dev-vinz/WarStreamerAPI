﻿namespace WarStreamer.ViewModels
{
    public class LanguageViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _id;
        private readonly string _cultureInfo;
        private readonly string _displayValue;
        private readonly string _shortcutValue;
        private readonly string _flagEmoji;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string Id
        {
            get => _id;
        }

        public string CultureInfo
        {
            get => _cultureInfo;
        }

        public string DisplayValue
        {
            get => _displayValue;
        }

        public string ShortcutValue
        {
            get => _shortcutValue;
        }

        public string FlagEmoji
        {
            get => _flagEmoji;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public LanguageViewModel(
            string id,
            string cultureInfo,
            string displayValue,
            string shortcutValue,
            string flagEmoji
        )
        {
            // Inputs
            {
                _id = id;
                _cultureInfo = cultureInfo;
                _displayValue = displayValue;
                _shortcutValue = shortcutValue;
                _flagEmoji = flagEmoji;
            }
        }
    }
}
