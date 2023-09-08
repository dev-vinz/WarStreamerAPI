﻿using WarStreamer.Models;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Tests.Models
{
    public class TeamLogoTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const string TEAM_NAME = "TeamLogoTest";
        private const decimal USER_ID_ONE = 1;
        private const decimal USER_ID_TWO = 2;
        private const int WIDTH = 100;
        private const int HEIGHT = 200;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        public void WhenCreatingTeamLogo_ThenTeamLogoReturned()
        {
            TeamLogo logo = CreateTeamLogoOne();

            Assert.NotNull(logo);
            Assert.Equal(TEAM_NAME.ToUpper(), logo.TeamName);
            Assert.Equal(USER_ID_ONE, logo.UserId);
            Assert.Equal(WIDTH, logo.Width);
            Assert.Equal(HEIGHT, logo.Height);
        }

        [Fact]
        public void WhenComparingSameTeamLogos_ThenTeamLogosAreTheSame()
        {
            TeamLogo logoOne = CreateTeamLogoOne();
            TeamLogo logoTwo = CreateTeamLogoOne();

            Assert.NotNull(logoOne);
            Assert.NotNull(logoTwo);

            Assert.True(logoOne == logoTwo);
            Assert.True(logoOne.Equals(logoTwo));
            Assert.True(logoTwo.Equals(logoOne));
            Assert.False(logoOne != logoTwo);
            Assert.Equal(logoOne.GetHashCode(), logoTwo.GetHashCode());
        }

        [Fact]
        public void WhenComparingDifferentTeamLogos_ThenTeamLogosAreDifferent()
        {
            TeamLogo logoOne = CreateTeamLogoOne();
            TeamLogo logoTwo = CreateTeamLogoTwo();

            Assert.NotNull(logoOne);
            Assert.NotNull(logoTwo);

            Assert.False(logoOne == logoTwo);
            Assert.False(logoOne.Equals(logoTwo));
            Assert.False(logoTwo.Equals(logoOne));
            Assert.True(logoOne != logoTwo);
            Assert.NotEqual(logoOne.GetHashCode(), logoTwo.GetHashCode());
        }

        [Fact]
        public void WhenCopyingTeamLogo_ThenTeamLogoCopied()
        {
            TeamLogo logo = CreateTeamLogoOne();
            Entity copy = new TeamLogo(logo.TeamName, logo.UserId);

            Assert.NotNull(logo);

            logo.CopyTo(ref copy);

            TeamLogo copyLogo = (TeamLogo)copy;

            Assert.Equal(logo.Width, copyLogo.Width);
            Assert.Equal(logo.Height, copyLogo.Height);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static TeamLogo CreateTeamLogoOne()
        {
            return new(TEAM_NAME, USER_ID_ONE)
            {
                Width = WIDTH,
                Height = HEIGHT,
            };
        }

        private static TeamLogo CreateTeamLogoTwo()
        {
            return new(TEAM_NAME, USER_ID_TWO)
            {
                Width = HEIGHT,
                Height = HEIGHT,
            };
        }
    }
}
