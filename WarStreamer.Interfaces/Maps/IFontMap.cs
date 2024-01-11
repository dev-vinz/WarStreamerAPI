using WarStreamer.ViewModels;

namespace WarStreamer.Interfaces.Maps
{
    public interface IFontMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public List<FontViewModel> GetAll();

        public FontViewModel? GetById(int id);
    }
}
