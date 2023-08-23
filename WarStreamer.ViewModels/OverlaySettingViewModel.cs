using System.Text.Json.Serialization;

namespace WarStreamer.ViewModels
{
    public class OverlaySettingViewModel
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly decimal _userId;
        private string _textColor;
        private bool _isLogo;
        private int? _logoLocationX;
        private int? _logoLocationY;
        private bool _isClanName;
        private int? _clanNameLocationX;
        private int? _clanNameLocationY;
        private bool _isTotalStars;
        private int? _totalStarsLocationX;
        private int? _totalStarsLocationY;
        private bool _isTotalPercentage;
        private int? _totalPercentageLocationX;
        private int? _totalPercentageLocationY;
        private bool _isAverageDuration;
        private int? _averageDurationLocationX;
        private int? _averageDurationLocationY;
        private bool _isPlayerDetails;
        private int? _playerDetailsLocationX;
        private int? _playerDetailsLocationY;
        private bool _mirrorReflection;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [JsonIgnore]
        public decimal UserId { get => _userId; }

        public string TextColor
        {
            get => _textColor; set
            {
                if (_isLogo)
                    _textColor = value;
            }
        }

        public bool IsLogo
        {
            get => _isLogo;
            set
            {
                _isLogo = value;

                if (!_isLogo)
                {
                    _logoLocationX = null;
                    _logoLocationY = null;
                }
            }
        }

        public int? LogoLocationX
        {
            get => _logoLocationX;
            set
            {
                if (_isLogo) _logoLocationX = value;
            }
        }

        public int? LogoLocationY
        {
            get => _logoLocationY;
            set
            {
                if (_isLogo) _logoLocationY = value;
            }
        }

        public bool IsClanName
        {
            get => _isClanName;
            set
            {
                _isClanName = value;

                if (!_isClanName)
                {
                    _clanNameLocationX = null;
                    _clanNameLocationY = null;
                }
            }
        }

        public int? ClanNameLocationX
        {
            get => _clanNameLocationX; set
            {
                if (_isClanName) _clanNameLocationX = value;
            }
        }

        public int? ClanNameLocationY
        {
            get => _clanNameLocationY; set
            {
                if (_isClanName) _clanNameLocationY = value;
            }
        }

        public bool IsTotalStars
        {
            get => _isTotalStars;
            set
            {
                _isTotalStars = value;

                if (!_isTotalStars)
                {
                    _totalStarsLocationX = null;
                    _totalStarsLocationY = null;
                }
            }
        }

        public int? TotalStarsLocationX
        {
            get => _totalStarsLocationX; set
            {
                if (_isTotalStars) _totalStarsLocationX = value;
            }
        }

        public int? TotalStarsLocationY
        {
            get => _totalStarsLocationY; set
            {
                if (_isTotalStars) _totalStarsLocationY = value;
            }
        }

        public bool IsTotalPercentage
        {
            get => _isTotalPercentage;
            set
            {
                _isTotalPercentage = value;

                if (!_isTotalPercentage)
                {
                    _totalPercentageLocationX = null;
                    _totalPercentageLocationY = null;
                }
            }
        }

        public int? TotalPercentageLocationX
        {
            get => _totalPercentageLocationX; set
            {
                if (_isTotalPercentage) _totalPercentageLocationX = value;
            }
        }

        public int? TotalPercentageLocationY
        {
            get => _totalPercentageLocationY; set
            {
                if (_isTotalPercentage) _totalPercentageLocationY = value;
            }
        }

        public bool IsAverageDuration
        {
            get => _isAverageDuration;
            set
            {
                _isAverageDuration = value;

                if (!_isAverageDuration)
                {
                    _averageDurationLocationX = null;
                    _averageDurationLocationY = null;
                }
            }
        }

        public int? AverageDurationLocationX
        {
            get => _averageDurationLocationX; set
            {
                if (_isAverageDuration) _averageDurationLocationX = value;
            }
        }

        public int? AverageDurationLocationY
        {
            get => _averageDurationLocationY; set
            {
                if (_isAverageDuration) _averageDurationLocationY = value;
            }
        }

        public bool IsPlayerDetails
        {
            get => _isPlayerDetails;
            set
            {
                _isPlayerDetails = value;

                if (!_isPlayerDetails)
                {
                    _playerDetailsLocationX = null;
                    _playerDetailsLocationY = null;
                }
            }
        }

        public int? PlayerDetailsLocationX
        {
            get => _playerDetailsLocationX; set
            {
                if (_isPlayerDetails) _playerDetailsLocationX = value;
            }
        }

        public int? PlayerDetailsLocationY
        {
            get => _playerDetailsLocationY; set
            {
                if (_isPlayerDetails) _playerDetailsLocationY = value;
            }
        }

        public bool MirrorReflection { get => _mirrorReflection; set => _mirrorReflection = value; }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public OverlaySettingViewModel(decimal userId)
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
