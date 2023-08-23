using System.Text.Json.Serialization;

namespace WarStreamer.ViewModels
{
    public class TeamLogoViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _teamName;
        private readonly decimal _userId;
        private int _width;
        private int _height;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string TeamName { get => _teamName; }

        [JsonIgnore]
        public decimal UserId { get => _userId; }

        public int Width { get => _width; set => _width = value; }

        public int Height { get => _height; set => _height = value; }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public TeamLogoViewModel(string teamName, decimal userId)
        {
            // Inputs
            {
                _teamName = teamName;
                _userId = userId;
            }
        }
    }
}
