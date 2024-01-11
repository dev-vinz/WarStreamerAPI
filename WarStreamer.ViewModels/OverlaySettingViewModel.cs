using WarStreamer.Commons.Tools;

namespace WarStreamer.ViewModels
{
    public class OverlaySettingViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _userId;
        private int? _fontId;
        private string _textColor;
        private bool _logoVisible;
        private int? _logoSize;
        private Location2D? _logoLocation;
        private bool _clanNameVisible;
        private int? _clanNameSize;
        private Location2D? _clanNameLocation;
        private bool _totalStarsVisible;
        private int? _totalStarsSize;
        private Location2D? _totalStarsLocation;
        private bool _totalPercentageVisible;
        private int? _totalPercentageSize;
        private Location2D? _totalPercentageLocation;
        private bool _averageDurationVisible;
        private int? _averageDurationSize;
        private Location2D? _averageDurationLocation;
        private bool _playerDetailsVisible;
        private int? _playerDetailsSize;
        private Location2D? _playerDetailsLocation;
        private bool _lastAttackToWinVisible;
        private int? _lastAttackToWinSize;
        private Location2D? _lastAttackToWinLocation;
        private bool _mirrorReflection;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string UserId
        {
            get => _userId;
        }

        public int? FontId
        {
            get => _fontId;
            set => _fontId = value;
        }

        public string TextColor
        {
            get => _textColor;
            set => _textColor = value;
        }

        public bool LogoVisible
        {
            get => _logoVisible;
            set
            {
                _logoVisible = value;

                if (!_logoVisible)
                {
                    _logoSize = null;
                    _logoLocation = null;
                }
            }
        }

        public int? LogoSize
        {
            get => _logoSize;
            set
            {
                if (_logoVisible)
                {
                    _logoSize = value;
                }
            }
        }

        public Location2D? LogoLocation
        {
            get => _logoLocation;
            set
            {
                if (_logoVisible)
                {
                    _logoLocation = value;
                }
            }
        }

        public bool ClanNameVisible
        {
            get => _clanNameVisible;
            set
            {
                _clanNameVisible = value;

                if (!_clanNameVisible)
                {
                    _clanNameSize = null;
                    _clanNameLocation = null;
                }
            }
        }

        public int? ClanNameSize
        {
            get => _clanNameSize;
            set
            {
                if (_clanNameVisible)
                {
                    _clanNameSize = value;
                }
            }
        }

        public Location2D? ClanNameLocation
        {
            get => _clanNameLocation;
            set
            {
                if (_clanNameVisible)
                {
                    _clanNameLocation = value;
                }
            }
        }

        public bool TotalStarsVisible
        {
            get => _totalStarsVisible;
            set
            {
                _totalStarsVisible = value;

                if (!_totalStarsVisible)
                {
                    _totalStarsSize = null;
                    _totalStarsLocation = null;
                }
            }
        }

        public int? TotalStarsSize
        {
            get => _totalStarsSize;
            set
            {
                if (_totalStarsVisible)
                {
                    _totalStarsSize = value;
                }
            }
        }

        public Location2D? TotalStarsLocation
        {
            get => _totalStarsLocation;
            set
            {
                if (_totalStarsVisible)
                {
                    _totalStarsLocation = value;
                }
            }
        }

        public bool TotalPercentageVisible
        {
            get => _totalPercentageVisible;
            set
            {
                _totalPercentageVisible = value;

                if (!_totalPercentageVisible)
                {
                    _totalPercentageSize = null;
                    _totalPercentageLocation = null;
                }
            }
        }

        public int? TotalPercentageSize
        {
            get => _totalPercentageSize;
            set
            {
                if (_totalPercentageVisible)
                {
                    _totalPercentageSize = value;
                }
            }
        }

        public Location2D? TotalPercentageLocation
        {
            get => _totalPercentageLocation;
            set
            {
                if (_totalPercentageVisible)
                {
                    _totalPercentageLocation = value;
                }
            }
        }

        public bool AverageDurationVisible
        {
            get => _averageDurationVisible;
            set
            {
                _averageDurationVisible = value;

                if (!_averageDurationVisible)
                {
                    _averageDurationSize = null;
                    _averageDurationLocation = null;
                }
            }
        }

        public int? AverageDurationSize
        {
            get => _averageDurationSize;
            set
            {
                if (_averageDurationVisible)
                {
                    _averageDurationSize = value;
                }
            }
        }

        public Location2D? AverageDurationLocation
        {
            get => _averageDurationLocation;
            set
            {
                if (_averageDurationVisible)
                    _averageDurationLocation = value;
            }
        }

        public bool PlayerDetailsVisible
        {
            get => _playerDetailsVisible;
            set
            {
                _playerDetailsVisible = value;

                if (!_playerDetailsVisible)
                {
                    _playerDetailsSize = null;
                    _playerDetailsLocation = null;
                }
            }
        }

        public int? PlayerDetailsSize
        {
            get => _playerDetailsSize;
            set
            {
                if (_playerDetailsVisible)
                {
                    _playerDetailsSize = value;
                }
            }
        }

        public Location2D? PlayerDetailsLocation
        {
            get => _playerDetailsLocation;
            set
            {
                if (_playerDetailsVisible)
                {
                    _playerDetailsLocation = value;
                }
            }
        }

        public bool LastAttackToWinVisible
        {
            get => _lastAttackToWinVisible;
            set
            {
                _lastAttackToWinVisible = value;

                if (!_lastAttackToWinVisible)
                {
                    _lastAttackToWinSize = null;
                    _lastAttackToWinLocation = null;
                }
            }
        }

        public int? LastAttackToWinSize
        {
            get => _lastAttackToWinSize;
            set
            {
                if (_lastAttackToWinVisible)
                {
                    _lastAttackToWinSize = value;
                }
            }
        }

        public Location2D? LastAttackToWinLocation
        {
            get => _lastAttackToWinLocation;
            set
            {
                if (_lastAttackToWinVisible)
                {
                    _lastAttackToWinLocation = value;
                }
            }
        }

        public bool MirrorReflection
        {
            get => _mirrorReflection;
            set => _mirrorReflection = value;
        }

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
