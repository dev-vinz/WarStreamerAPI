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
        private const decimal USER_ID = 1;
        private const int WIDTH = 100;
        private const int WIDTH_UPDATED = 200;
        private const int HEIGHT = 200;
        private const int HEIGHT_UPDATED = 300;

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

            Assert.Equal(TEAM_NAME, logo.TeamName);
            Assert.Equal(USER_ID, logo.UserId);
            Assert.Equal(WIDTH, logo.Width);
            Assert.Equal(HEIGHT, logo.Height);
            Assert.NotEqual(DateTimeOffset.MinValue, logo.CreatedAt);
            Assert.Equal(logo.CreatedAt, logo.UpdatedAt);
        }

        [Fact]
        [TestOrder(2)]
        public void WhenGetAllTeamLogos_ThenReturnsTeamLogos()
        {
            List<TeamLogo> logos = _repository.GetAll();

            TeamLogo logo = Assert.Single(logos);

            Assert.Equal(TEAM_NAME, logo.TeamName);
            Assert.Equal(USER_ID, logo.UserId);
            Assert.Equal(WIDTH, logo.Width);
            Assert.Equal(HEIGHT, logo.Height);
            Assert.NotEqual(DateTimeOffset.MinValue, logo.CreatedAt);
            Assert.Equal(logo.CreatedAt, logo.UpdatedAt);
        }

        [Fact]
        [TestOrder(3)]
        public void WhenGetTeamLogosByUserId_ThenReturnsTeamLogos()
        {
            List<TeamLogo> logos = _repository.GetByUserId(USER_ID);

            TeamLogo logo = Assert.Single(logos);

            Assert.Equal(TEAM_NAME, logo.TeamName);
            Assert.Equal(USER_ID, logo.UserId);
            Assert.Equal(WIDTH, logo.Width);
            Assert.Equal(HEIGHT, logo.Height);
            Assert.NotEqual(DateTimeOffset.MinValue, logo.CreatedAt);
            Assert.Equal(logo.CreatedAt, logo.UpdatedAt);
        }

        [Fact]
        [TestOrder(4)]
        public void WhenGetTeamLogosByUserId_ThenReturnsEmpty()
        {
            Assert.Empty(_repository.GetByUserId(USER_ID + 1));
        }

        [Fact]
        [TestOrder(5)]
        public void WhenUpdateTeamLogo_ThenReturnsTrue()
        {
            List<TeamLogo> logos = _repository.GetByUserId(USER_ID);

            TeamLogo logo = Assert.Single(logos);

            logo.Width = WIDTH_UPDATED;
            logo.Height = HEIGHT_UPDATED;

            Assert.True(_repository.Update(logo));

            TeamLogo updatedLogo = Assert.Single(_repository.GetAll());

            Assert.Equal(WIDTH_UPDATED, updatedLogo.Width);
            Assert.Equal(HEIGHT_UPDATED, updatedLogo.Height);
            Assert.NotEqual(updatedLogo.CreatedAt, updatedLogo.UpdatedAt);
        }

        [Fact]
        [TestOrder(6)]
        public void WhenDeleteTeamLogo_ThenReturnsTrue()
        {
            TeamLogo logo = Assert.Single(_repository.GetAll());

            Assert.True(_repository.Delete(logo));
            Assert.Empty(_repository.GetAll());
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static TeamLogo CreateTeamLogo()
        {
            return new TeamLogo
            {
                TeamName = TEAM_NAME,
                UserId = USER_ID,
                Width = WIDTH,
                Height = HEIGHT,
            };
        }
    }
}
