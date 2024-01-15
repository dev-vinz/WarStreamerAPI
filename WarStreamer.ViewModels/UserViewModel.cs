namespace WarStreamer.ViewModels
{
    public class UserViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _id;
        private string _languageId;
        private uint _tierLevel;
        private bool _newsLetter;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string Id
        {
            get => _id;
        }

        public string LanguageId
        {
            get => _languageId;
            set => _languageId = value;
        }

        public uint TierLevel
        {
            get => _tierLevel;
            set => _tierLevel = value;
        }

        public bool NewsLetter
        {
            get => _newsLetter;
            set => _newsLetter = value;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public UserViewModel(string id)
        {
            // Inputs
            {
                _id = id;
                _languageId = string.Empty;
            }
        }
    }
}
