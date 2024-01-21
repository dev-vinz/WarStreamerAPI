using ClashOfClans;
using ClashOfClans.Core;
using ClashOfClans.Models;
using ClashOfClans.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Web.API.Extensions;

namespace WarStreamer.Web.API.Controllers.ClashOfClans
{
    [Authorize]
    [Route("coc/clans/")]
    public class ClashClanController : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly ClashOfClansClient _client;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public ClashClanController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            string? token = configuration.GetValue<string>(
                environment.IsDevelopment() ? "DevCocApiToken" : "ProdCocApiToken"
            );

            if (token == null)
            {
                throw new(nameof(token));
            }

            _client = new(token);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*               GET               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpGet("tag/{clanTag}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<Clan?>> GetByTag(string clanTag)
        {
            if (!clanTag.StartsWith('#'))
            {
                clanTag = $"#{clanTag}";
            }

            try
            {
                Clan clan = await _client.Clans.GetClanAsync(clanTag);

                if (clan.Members == 0)
                {
                    throw new ClashOfClansException(new ClientError { Reason = "notFound" });
                }

                return Ok(clan);
            }
            catch (ClashOfClansException e)
            {
                return e.SendError();
            }
        }

        [HttpGet("name/{clanName}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<Clan[]>> GetByName(string clanName)
        {
            try
            {
                QueryClans query = new() { Name = clanName, Limit = 10, };

                QueryResult<ClanList> clans = await _client.Clans.SearchClansAsync(query);

                return Ok(clans.Items.ToArray());
            }
            catch (ClashOfClansException e)
            {
                return e.SendError();
            }
        }

        [HttpGet("{nameOrTag}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<Clan[]>> GetByNameOrTag(string nameOrTag)
        {
            ActionResult<Clan[]> clansByNameActionResults = await GetByName(nameOrTag);
            ActionResult<Clan?> clanByTagActionResult = await GetByTag(nameOrTag);

            OkObjectResult? clansByNameObject = clansByNameActionResults.Result as OkObjectResult;
            OkObjectResult? clanByTagObject = clanByTagActionResult.Result as OkObjectResult;

            Clan[] clansByName = clansByNameObject?.Value as Clan[] ?? [];
            List<Clan> clans = new(clansByName);

            if (clanByTagObject?.Value is Clan clanByTag)
            {
                clans.Add(clanByTag);
            }

            return Ok(clans);
        }

        [HttpGet("{clanTag}/war")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ClanWar>> GetCurrentWar(string clanTag)
        {
            if (!clanTag.StartsWith('#'))
            {
                clanTag = $"#{clanTag}";
            }

            try
            {
                ClanWar war = await _client.Clans.GetCurrentWarAsync(clanTag);

                if (war.State == State.NotInWar)
                {
                    throw new ClashOfClansException(new ClientError { Reason = "notFound" });
                }

                return Ok(war);
            }
            catch (ClashOfClansException e)
            {
                return e.SendError();
            }
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */
    }
}
