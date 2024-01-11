using WarStreamer.Models;

namespace WarStreamer.Interfaces.Services
{
    public interface IFontService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public List<Font> GetAll();

        public Font? GetById(int id);
    }
}
