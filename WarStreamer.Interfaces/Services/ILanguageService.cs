using WarStreamer.Models;

namespace WarStreamer.Interfaces.Services
{
    public interface ILanguageService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public List<Language> GetAll();

        public Language? GetById(Guid id);
    }
}
