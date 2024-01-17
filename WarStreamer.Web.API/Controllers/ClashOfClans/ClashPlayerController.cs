using ClashOfClans;
using ClashOfClans.Core;
using ClashOfClans.Models;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Web.API.Extensions;

namespace WarStreamer.Web.API.Controllers.ClashOfClans
{
    [Route("coc/players/")]
    public class ClashPlayerController : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly ClashOfClansClient _client;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public ClashPlayerController(IConfiguration configuration, IWebHostEnvironment environment)
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

        [HttpGet]
        [Route("{playerTag}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<Player?>> GetByTagAsync(string playerTag)
        {
            if (!playerTag.StartsWith('#'))
            {
                playerTag = $"#{playerTag}";
            }

            try
            {
                return Ok(await _client.Players.GetPlayerAsync(playerTag));
            }
            catch (ClashOfClansException e)
            {
                return e.SendError();
            }
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPost]
        [Route("{playerTag}/verifytoken")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<VerifyTokenResponse?>> VerifyTokenAsync(
            string playerTag,
            [FromBody] string token
        )
        {
            if (!playerTag.StartsWith('#'))
            {
                playerTag = $"#{playerTag}";
            }

            try
            {
                return Ok(await _client.Players.VerifyTokenAsync(playerTag, token));
            }
            catch (ClashOfClansException e)
            {
                return e.SendError();
            }
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */
    }
}
