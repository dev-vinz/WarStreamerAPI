using WarStreamer.Commons.Tools;

namespace WarStreamer.ViewModels
{
    public class OverlaySettingViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _userId;
        private string _textColor;
        private bool _logoVisible;
        private Location2D? _logoLocation;
        private bool _clanNameVisible;
        private Location2D? _clanNameLocation;
        private bool _totalStarsVisible;
        private Location2D? _totalStarsLocation;
        private bool _totalPercentageVisible;
        private Location2D? _totalPercentageLocation;
        private bool _averageDurationVisible;
        private Location2D? _averageDurationLocation;
        private bool _playerDetailsVisible;
        private Location2D? _playerDetailsLocation;
        private bool _lastAttackToWinVisible;
        private Location2D? _lastAttackToWinLocation;
        private bool _mirrorReflection;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string UserId { get => _userId; }

        public string TextColor { get => _textColor; set => _textColor = value; }

        public bool LogoVisible
        {
            get => _logoVisible;
            set
            {
                _logoVisible = value;

                if (!_logoVisible) _logoLocation = null;
            }
        }

        public Location2D? LogoLocation
        {
            get => _logoLocation;
            set
            {
                if (_logoVisible) _logoLocation = value;
            }
        }

        public bool ClanNameVisible
        {
            get => _clanNameVisible;
            set
            {
                _clanNameVisible = value;

                if (!_clanNameVisible) _clanNameLocation = null;
            }
        }

        public Location2D? ClanNameLocation
        {
            get => _clanNameLocation;
            set
            {
                if (_clanNameVisible) _clanNameLocation = value;
            }
        }

        public bool TotalStarsVisible
        {
            get => _totalStarsVisible;
            set
            {
                _totalStarsVisible = value;

                if (!_totalStarsVisible) _totalStarsLocation = null;
            }
        }

        public Location2D? TotalStarsLocation
        {
            get => _totalStarsLocation;
            set
            {
                if (_totalStarsVisible) _totalStarsLocation = value;
            }
        }

        public bool TotalPercentageVisible
        {
            get => _totalPercentageVisible;
            set
            {
                _totalPercentageVisible = value;

                if (!_totalPercentageVisible) _totalPercentageLocation = null;
            }
        }

        public Location2D? TotalPercentageLocation
        {
            get => _totalPercentageLocation;
            set
            {
                if (_totalPercentageVisible) _totalPercentageLocation = value;
            }
        }

        public bool AverageDurationVisible
        {
            get => _averageDurationVisible;
            set
            {
                _averageDurationVisible = value;

                if (!_averageDurationVisible) _averageDurationLocation = null;
            }
        }

        public Location2D? AverageDurationLocation
        {
            get => _averageDurationLocation;
            set
            {
                if (_averageDurationVisible) _averageDurationLocation = value;
            }
        }

        public bool PlayerDetailsVisible
        {
            get => _playerDetailsVisible;
            set
            {
                _playerDetailsVisible = value;

                if (!_playerDetailsVisible) _playerDetailsLocation = null;
            }
        }

        public Location2D? PlayerDetailsLocation
        {
            get => _playerDetailsLocation;
            set
            {
                if (_playerDetailsVisible) _playerDetailsLocation = value;
            }
        }

        public bool LastAttackToWinVisible
        {
            get => _lastAttackToWinVisible;
            set
            {
                _lastAttackToWinVisible = value;

                if (!_lastAttackToWinVisible) _lastAttackToWinLocation = null;
            }
        }

        public Location2D? LastAttackToWinLocation
        {
            get => _lastAttackToWinLocation;
            set
            {
                if (_lastAttackToWinVisible) _lastAttackToWinLocation = value;
            }
        }

        public bool MirrorReflection { get => _mirrorReflection; set => _mirrorReflection = value; }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public OverlaySettingViewModel(string userId)
        {
            // Inputs
            {
                _userId = userId;
            }

            // Tools
            {
                _textColor = string.Empty;
            }
        }
    }
}
