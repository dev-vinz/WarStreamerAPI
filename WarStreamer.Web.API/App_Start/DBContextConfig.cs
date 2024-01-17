using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WarStreamer.Models.Context;

namespace WarStreamer.Web.API.App_Start
{
    public class DBContextConfig
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private const string STAGING_DATABASE_NAME = "WarStreamer";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        public static void Initialize(
            IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment env
        )
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                .LogTo(Console.WriteLine, LogLevel.Information)
                .ConfigureWarnings(w => w.Ignore(SqlServerEventId.DecimalTypeKeyWarning))
                .EnableSensitiveDataLogging();

            if (env.IsDevelopment())
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DevConnection"));
            }
            else if (env.IsProduction())
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("ProdConnection"));
            }
            else if (env.IsStaging())
            {
                optionsBuilder.UseInMemoryDatabase(STAGING_DATABASE_NAME);
            }
            else
            {
                throw new Exception($"Environment '{env.EnvironmentName}' is unknown");
            }

            WarStreamerContext context = new(optionsBuilder.Options);

            services.AddDbContext<WarStreamerContext>(options =>
            {
                if (env.IsDevelopment())
                {
                    options.UseSqlServer(configuration.GetConnectionString("DevConnection"));
                }
                else if (env.IsProduction())
                {
                    options.UseSqlServer(configuration.GetConnectionString("ProdConnection"));
                }
                else if (env.IsStaging())
                {
                    options.UseInMemoryDatabase(STAGING_DATABASE_NAME);
                }
            });

            // Create database and migrate any modification
            context.Database.Migrate();
        }
    }
}
