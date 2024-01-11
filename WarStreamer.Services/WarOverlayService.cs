using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;

namespace WarStreamer.Services
{
    public class WarOverlayService(IWarOverlayRepository repository) : IWarOverlayService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWarOverlayRepository _repository = repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public WarOverlay Create(WarOverlay domain)
        {
            return _repository.Save(domain);
        }

        public bool Delete(WarOverlay domain)
        {
            return _repository.Delete(domain);
        }

        public List<WarOverlay> GetAll()
        {
            return _repository.GetAll();
        }

        public List<WarOverlay> GetByUserId(decimal userId)
        {
            return _repository.GetByUserId(userId);
        }

        public WarOverlay? GetByUserIdAndId(decimal userId, int id)
        {
            return _repository.GetByUserIdAndId(userId, id);
        }

        public bool Update(WarOverlay domain)
        {
            return _repository.Update(domain);
        }
    }
}
