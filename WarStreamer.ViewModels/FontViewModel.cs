namespace WarStreamer.ViewModels
{
    public class FontViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _id;
        private readonly string _displayName;
        private readonly string _familyName;
        private readonly string _fileName;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string Id
        {
            get => _id;
        }

        public string DisplayName
        {
            get => _displayName;
        }

        public string FamilyName
        {
            get => _familyName;
        }

        public string FileName
        {
            get => _fileName;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public FontViewModel(string id, string displayName, string familyName, string fileName)
        {
            // Inputs
            {
                _id = id;
                _displayName = displayName;
                _familyName = familyName;
                _fileName = fileName;
            }
        }
    }
}
