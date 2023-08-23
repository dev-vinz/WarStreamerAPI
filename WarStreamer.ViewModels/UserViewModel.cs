using System.Text.Json.Serialization;

namespace WarStreamer.ViewModels
{
    public class UserViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly decimal _id;
        private int _languageId;
        private uint _tierLevel;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [JsonIgnore]
        public decimal Id { get => _id; }

        public int LanguageId { get => _languageId; set => _languageId = value; }

        public uint TierLevel { get => _tierLevel; set => _tierLevel = value; }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public UserViewModel(decimal id)
        {
            // Inputs
            {
                _id = id;
            }
        }
    }
}
