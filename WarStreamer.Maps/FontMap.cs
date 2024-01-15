using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class FontMap(IFontService service) : IFontMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IFontService _service = service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public List<FontViewModel> GetAll()
        {
            return DomainToViewModel(_service.GetAll());
        }

        public FontViewModel? GetById(Guid id)
        {
            Font? font = _service.GetById(id);
            return font != null ? DomainToViewModel(font) : null;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static FontViewModel DomainToViewModel(Font domain)
        {
            return new($"{domain.Id}", domain.DisplayName, domain.FileName);
        }

        private static List<FontViewModel> DomainToViewModel(List<Font> domain)
        {
            return domain.Select(DomainToViewModel).ToList();
        }
    }
}
