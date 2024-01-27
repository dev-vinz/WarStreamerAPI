namespace WarStreamer.Commons.Extensions
{
    public static class IConfigurationSectionExtensions
    {
        /* * * * * * * * * * * * * * * * * *\
        |*            GENERIC T            *|
        \* * * * * * * * * * * * * * * * * */

        public static T GetSection<T>(this IConfiguration configuration, string key)
            where T : class, new()
        {
            IConfigurationSection section =
                configuration.GetSection(key)
                ?? throw new ArgumentNullException(nameof(configuration));

            T modelSection = new();
            section.Bind(modelSection);

            return modelSection;
        }

        public static T GetSection<T>(this IConfiguration configuration)
            where T : class, new()
        {
            string key = typeof(T).Name;
            return configuration.GetSection<T>(key);
        }
    }
}
