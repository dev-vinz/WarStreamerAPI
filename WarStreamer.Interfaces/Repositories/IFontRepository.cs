using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IFontRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public List<Font> GetAll();

        public Font? GetById(int id);
    }
}
