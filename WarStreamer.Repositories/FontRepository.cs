using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class FontRepository(IWarStreamerContext context) : Repository(context), IFontRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public List<Font> GetAll()
        {
            try
            {
                return [.. Context.Set<Font>()];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Font? GetById(Guid id)
        {
            try
            {
                return Context.Set<Font>().FirstOrDefault(l => l.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
