using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class LanguageMap(ILanguageService service) : ILanguageMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly ILanguageService _service = service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public List<LanguageViewModel> GetAll()
        {
            return DomainToViewModel(_service.GetAll());
        }

        public LanguageViewModel? GetById(int id)
        {
            Language? language = _service.GetById(id);
            return language != null ? DomainToViewModel(language) : null;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static LanguageViewModel DomainToViewModel(Language domain)
        {
            return new(
                domain.Id,
                domain.CultureInfo,
                domain.DisplayValue,
                domain.ShortcutValue,
                domain.FlagEmoji
            );
        }

        private static List<LanguageViewModel> DomainToViewModel(List<Language> domain)
        {
            return domain.Select(DomainToViewModel).ToList();
        }
    }
}
