﻿namespace WarStreamer.ViewModels
{
    public class WarOverlayViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _userId;
        private readonly Guid _id;
        private readonly string _clanTag;
        private DateTimeOffset _lastCheckout;
        private bool _isEnded;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string UserId
        {
            get => _userId;
        }

        public Guid Id
        {
            get => _id;
        }

        public string ClanTag
        {
            get => _clanTag;
        }

        public DateTimeOffset LastCheckout
        {
            get => _lastCheckout;
            set => _lastCheckout = value;
        }

        public bool IsEnded
        {
            get => _isEnded;
            set => _isEnded = value;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public WarOverlayViewModel(string userId, Guid id, string clanTag)
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
