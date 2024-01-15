using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class LanguageRepository(IWarStreamerContext context)
        : Repository(context),
            ILanguageRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public List<Language> GetAll()
        {
            try
            {
                return [.. Context.Set<Language>()];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Language? GetById(Guid id)
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
