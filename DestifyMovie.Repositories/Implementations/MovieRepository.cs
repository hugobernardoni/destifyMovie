using DestifyMovie.Data;
using DestifyMovie.Data.Entities;
using DestifyMovie.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DestifyMovie.Repositories.Implementations;

public class MovieRepository : Repository<Movie>, IMovieRepository
{
    public MovieRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Movie>> SearchByNameAsync(string title)
    {
        return await FindByNameAsync(m => m.Title.Contains(title));
    }

}
