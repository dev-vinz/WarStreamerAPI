using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models.Context;
using WarStreamer.Repositories;
using WarStreamer.Services;

namespace WarStreamer.Tests
{
    public class ServiceTestCollection : ServiceCollection
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const string STAGING_DATABASE_NAME = "GitHub";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public ServiceTestCollection()
        {
            ConfigureDatabase();
            AddScopes();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private void ConfigureDatabase()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(STAGING_DATABASE_NAME)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            WarStreamerContext context = new(optionsBuilder.Options);

            this.AddDbContext<WarStreamerContext>(options =>
            {
                options.UseInMemoryDatabase(STAGING_DATABASE_NAME);
                options.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }

        private void AddScopes()
        {
            // Global context
            this.AddScoped<IWarStreamerContext, WarStreamerContext>();

            // Account
            this.AddScoped<IAccountService, AccountService>();
            this.AddScoped<IAccountRepository, AccountRepository>();

            // Image
            this.AddScoped<IImageService, ImageService>();
            this.AddScoped<IImageRepository, ImageRepository>();

            // Language
            this.AddScoped<ILanguageService, LanguageService>();
            this.AddScoped<ILanguageRepository, LanguageRepository>();

            // Overlay setting
            this.AddScoped<IOverlaySettingService, OverlaySettingService>();
            this.AddScoped<IOverlaySettingRepository, OverlaySettingRepository>();

            // Team logo
            this.AddScoped<ITeamLogoService, TeamLogoService>();
            this.AddScoped<ITeamLogoRepository, TeamLogoRepository>();

            // User
            this.AddScoped<IUserService, UserService>();
            this.AddScoped<IUserRepository, UserRepository>();

            // War overlay
            this.AddScoped<IWarOverlayService, WarOverlayService>();
            this.AddScoped<IWarOverlayRepository, WarOverlayRepository>();
        }
    }
}
