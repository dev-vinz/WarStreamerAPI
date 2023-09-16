using ClashOfClans.Core;
using Microsoft.AspNetCore.Mvc;

namespace WarStreamer.Web.API.Extensions
{
    public static class ClashOfClansExceptionExtensions
    {
        public static ActionResult SendError(this ClashOfClansException exception)
        {
            return exception.Error.Reason switch
            {
                "accessDenied" => new StatusCodeResult(StatusCodes.Status403Forbidden),
                "notFound" => new StatusCodeResult(StatusCodes.Status404NotFound),
                "inMaintenance" => new StatusCodeResult(StatusCodes.Status503ServiceUnavailable),
                _ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
            };
        }
    }
}
