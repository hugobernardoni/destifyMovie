using DestifyMovie.Data.Entities;

namespace DestifyMovie.Repositories.Interfaces;

public interface IActorRepository : IRepository<Actor>
{
    Task<IEnumerable<Actor>> SearchByNameAsync(string title);    
}
