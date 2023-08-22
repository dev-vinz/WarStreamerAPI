using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class LanguageRepository : Repository, ILanguageRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public LanguageRepository(IWarStreamerContext context) : base(context) { }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public List<Language> GetAll()
        {
            try
            {
                return Context.Set<Language>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Language? GetById(int id)
        {
            try
            {
                return Context.Set<Language>().FirstOrDefault(l => l.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
