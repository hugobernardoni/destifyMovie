using DestifyMovie.Data.Entities;

namespace DestifyMovie.Services.Interfaces
{
    public interface IActorService : IGenericService<Actor>
    {
        Task<IEnumerable<Actor>> SearchByNameAsync(string name);
        Task<IEnumerable<Actor>> GetByIdsAsync(IEnumerable<int> ids);
    }
}
