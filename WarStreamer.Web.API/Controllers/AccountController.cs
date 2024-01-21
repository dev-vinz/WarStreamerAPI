using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;

namespace WarStreamer.Web.API.Controllers
{
    [Authorize]
    [Route("accounts/")]
    public class AccountController(IAccountMap accountMap) : Controller
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IAccountMap _accountMap = accountMap;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*               GET               *|
        \* * * * * * * * * * * * * * * * * */

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<AccountViewModel>> GetAll()
        {
            return Ok(_accountMap.GetAll());
        }

        [HttpGet]
        [Route("{userTag}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AccountViewModel?> GetByTag(string userTag)
        {
            // Ensure that the tag begins with '#'
            if (!userTag.StartsWith('#'))
            {
                userTag = $"#{userTag}";
            }

            AccountViewModel? account = _accountMap.GetByTag(userTag);

            // Verify if the account exists
            if (account == null)
            {
                return NotFound(new { error = $"Account with tag '{userTag}' not found" });
            }

            return Ok(account);
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               POST              *|
        \* * * * * * * * * * * * * * * * * */

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<AccountViewModel> Create([FromBody] AccountViewModel account)
        {
            // Verify if the account already exists
            if (_accountMap.GetByTag(account.Tag) != null)
            {
                return Conflict(new { error = $"Account with tag '{account.Tag}' already exists" });
            }

            return Created($"~/accounts/{account.Tag}", _accountMap.Create(account));
        }

        /* * * * * * * * * * * * * * * * * *\
        |*               PUT               *|
        \* * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              DELETE             *|
        \* * * * * * * * * * * * * * * * * */

        [HttpDelete]
        [Route("{userTag}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Delete(string userTag)
        {
            // Ensure that the tag begins with '#'
            if (!userTag.StartsWith('#'))
            {
                userTag = $"#{userTag}";
            }

            AccountViewModel? account = _accountMap.GetByTag(userTag);

            // Verify if the account exists
            if (account == null)
            {
                return NotFound(new { error = $"Account with tag '{userTag}' not found" });
            }

            return Ok(_accountMap.Delete(account));
        }
    }
}
