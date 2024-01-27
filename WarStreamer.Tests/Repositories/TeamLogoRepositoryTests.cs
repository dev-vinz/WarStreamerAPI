using Microsoft.Extensions.DependencyInjection;
using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Tests.Tools;

namespace WarStreamer.Tests.Repositories
{
    [TestCaseOrderer("WarStreamer.Tests.Tools.OrderOrderer", "WarStreamer.Tests")]
    public class TeamLogoRepositoryTests
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const string TEAM_NAME = "TeamLogoTest";
        private static readonly Guid USER_ID = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560d");
        private static readonly Guid USER_ID_2 = Guid.Parse("01e75c83-c6f5-4192-b57e-7427cec5560e");
        private static readonly string[] CLAN_TAGS = ["#TAG_1", "#TAG_2"];

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly ITeamLogoRepository _repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public TeamLogoRepositoryTests()
        {
            _repository = new ServiceTestCollection()
                .BuildServiceProvider()
                .GetRequiredService<ITeamLogoRepository>();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Fact]
        [TestOrder(1)]
        public void WhenInsertTeamLogo_ThenReturnsAddedTeamLogo()
        {
            TeamLogo logo = _repository.Save(CreateTeamLogo());

            Assert.NotNull(logo);

            Assert.Equal(TEAM_NAME.ToUpper(), logo.TeamName);
            Assert.Equal(USER_ID, logo.UserId);
            Assert.Equal(CLAN_TAGS, logo.ClanTags);
        }

        [Fact]
        [TestOrder(2)]
        public void WhenGetTeamLogosByUserId_ThenReturnsTeamLogos()
        {
            List<TeamLogo> logos = _repository.GetByUserId(USER_ID);

            TeamLogo logo = Assert.Single(logos);

            Assert.Equal(TEAM_NAME.ToUpper(), logo.TeamName);
            Assert.Equal(USER_ID, logo.UserId);
            Assert.Equal(CLAN_TAGS, logo.ClanTags);
        }

        [Fact]
        [TestOrder(3)]
        public void WhenGetTeamLogosByUserId_ThenReturnsEmpty()
        {
            Assert.Empty(_repository.GetByUserId(USER_ID_2));
        }

        [Fact]
        [TestOrder(4)]
        public void WhenGetTeamLogoByUserIdAndName_ThenReturnsTeamLogo()
        {
            TeamLogo? logo = _repository.GetByUserIdAndName(USER_ID, TEAM_NAME);

            Assert.NotNull(logo);

            Assert.Equal(TEAM_NAME.ToUpper(), logo.TeamName);
            Assert.Equal(USER_ID, logo.UserId);
            Assert.Equal(CLAN_TAGS, logo.ClanTags);
        }

        [Fact]
        [TestOrder(5)]
        public void WhenGetTeamLogoByUserIdAndName_ThenReturnsNull()
        {
            TeamLogo? logo = _repository.GetByUserIdAndName(USER_ID_2, TEAM_NAME);

            Assert.Null(logo);
        }

        [Fact]
        [TestOrder(6)]
        public void WhenDeleteTeamLogo_ThenReturnsTrue()
        {
            TeamLogo logo = Assert.Single(_repository.GetByUserId(USER_ID));

            Assert.True(_repository.Delete(logo));
            Assert.Empty(_repository.GetByUserId(USER_ID));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static TeamLogo CreateTeamLogo()
        {
            return new(TEAM_NAME, USER_ID) { ClanTags = CLAN_TAGS };
        }
    }
}
