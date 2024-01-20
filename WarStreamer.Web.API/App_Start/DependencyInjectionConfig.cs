using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Maps;
using WarStreamer.Models.Context;
using WarStreamer.Repositories;
using WarStreamer.Services;

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

            // Account
            services.AddScoped<IAccountMap, AccountMap>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            // AuthRefreshToken
            services.AddScoped<IAuthRefreshTokenMap, AuthRefreshTokenMap>();
            services.AddScoped<IAuthRefreshTokenService, AuthRefreshTokenService>();
            services.AddScoped<IAuthRefreshTokenRepository, AuthRefreshTokenRepository>();

            // Font
            services.AddScoped<IFontMap, FontMap>();
            services.AddScoped<IFontService, FontService>();
            services.AddScoped<IFontRepository, FontRepository>();

            // Image
            services.AddScoped<IImageMap, ImageMap>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IImageRepository, ImageRepository>();

            // Language
            services.AddScoped<ILanguageMap, LanguageMap>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();

            // Overlay setting
            services.AddScoped<IOverlaySettingMap, OverlaySettingMap>();
            services.AddScoped<IOverlaySettingService, OverlaySettingService>();
            services.AddScoped<IOverlaySettingRepository, OverlaySettingRepository>();

            // Team logo
            services.AddScoped<ITeamLogoMap, TeamLogoMap>();
            services.AddScoped<ITeamLogoService, TeamLogoService>();
            services.AddScoped<ITeamLogoRepository, TeamLogoRepository>();

            // User
            services.AddScoped<IUserMap, UserMap>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            // War overlay
            services.AddScoped<IWarOverlayMap, WarOverlayMap>();
            services.AddScoped<IWarOverlayService, WarOverlayService>();
            services.AddScoped<IWarOverlayRepository, WarOverlayRepository>();
        }
    }
}
