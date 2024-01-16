namespace WarStreamer.ViewModels
{
    public class TeamLogoViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _teamName;
        private readonly string _userId;
        private HashSet<string> _clanTags;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string TeamName
        {
            get => _teamName;
        }

        public string UserId
        {
            get => _userId;
        }

        public HashSet<string> ClanTags
        {
            get => _clanTags;
            set => _clanTags = value;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public TeamLogoViewModel(string teamName, string userId)
        {
            // Inputs
            {
                _teamName = teamName;
                _userId = userId;
            }

            // Outputs
            {
                _clanTags = [];
            }
        }
    }
}
