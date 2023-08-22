using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models.Context;
using WarStreamer.Repositories;

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
            this.AddScoped<IAccountRepository, AccountRepository>();

            // Image
            this.AddScoped<IImageRepository, ImageRepository>();

            // Language
            this.AddScoped<ILanguageRepository, LanguageRepository>();

            // Overlay setting
            this.AddScoped<IOverlaySettingRepository, OverlaySettingRepository>();

            // Team logo
            this.AddScoped<ITeamLogoRepository, TeamLogoRepository>();

            // User
            this.AddScoped<IUserRepository, UserRepository>();

            // War overlay
            this.AddScoped<IWarOverlayRepository, WarOverlayRepository>();
        }
    }
}
