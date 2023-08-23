using System.Text.Json.Serialization;

namespace WarStreamer.ViewModels
{
    public class WarOverlayViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly decimal _userId;
        private readonly int _id;
        private readonly string _clanTag;
        private DateTimeOffset _lastCheckout;
        private bool _isEnded;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [JsonIgnore]
        public decimal UserId { get => _userId; }

        [JsonIgnore]
        public int Id { get => _id; }

        public string ClanTag { get => _clanTag; }

        public DateTimeOffset LastCheckout { get => _lastCheckout; set => _lastCheckout = value; }

        public bool IsEnded { get => _isEnded; set => _isEnded = value; }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public WarOverlayViewModel(decimal userId, int id, string clanTag)
        {
            // Inputs
            {
                _userId = userId;
                _id = id;
                _clanTag = clanTag;
            }
        }
    }
}
