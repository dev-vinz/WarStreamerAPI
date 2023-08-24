using WarStreamer.ViewModels;

namespace WarStreamer.Interfaces.Maps
{
    public interface ILanguageMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public List<LanguageViewModel> GetAll();

        public LanguageViewModel? GetById(int id);
    }
}
