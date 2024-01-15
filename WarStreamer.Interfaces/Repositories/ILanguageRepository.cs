using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface ILanguageRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public List<Language> GetAll();

        public Language? GetById(Guid id);
    }
}
