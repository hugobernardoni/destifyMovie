using DestifyMovie.Data;
using DestifyMovie.Data.Entities;
using DestifyMovie.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DestifyMovie.Repositories.Implementations;

public class ActorRepository : Repository<Actor>, IActorRepository
{
    private readonly AppDbContext _context;
    public ActorRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Actor>> SearchByNameAsync(string name)
    {
        return await FindByNameAsync(a => a.Name.Contains(name));
    }    

}
