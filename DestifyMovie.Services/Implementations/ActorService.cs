using DestifyMovie.Data.Entities;
using DestifyMovie.Repositories.Implementations;
using DestifyMovie.Repositories.Interfaces;
using DestifyMovie.Services.Interfaces;

namespace DestifyMovie.Services
{
    public class ActorService : GenericService<Actor>, IActorService
    {
        private readonly IActorRepository _actorRepository;
        public ActorService(IActorRepository repository) : base(repository)
        {
            _actorRepository = repository;
        }

        public async Task<IEnumerable<Actor>> SearchByNameAsync(string title)
        {
            return await _actorRepository.FindByNameAsync(m => m.Name.Contains(title));
        }

        public async Task<IEnumerable<Actor>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _actorRepository.FindAsync(actor => ids.Contains(actor.Id));
        }

    }
}
