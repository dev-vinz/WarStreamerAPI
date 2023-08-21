using WarStreamer.Models.Context;

namespace WarStreamer.Web.API.App_Start
{
    public class DependencyInjectionConfig
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        public static void AddScopes(IServiceCollection services)
        {
            // Global context
            services.AddScoped<IWarStreamerContext, WarStreamerContext>();
        }
    }
}
