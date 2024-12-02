using DestifyMovie.Data;
using DestifyMovie.Data.Entities;
using DestifyMovie.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DestifyMovie.Repositories.Implementations;

public class MovieRepository : Repository<Movie>, IMovieRepository
{
    private readonly AppDbContext _context;
    public MovieRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movie>> SearchByNameAsync(string title)
    {
        return await FindByNameAsync(m => m.Title.ToLower().Contains(title.ToLower()));

    }

    public async Task AddRatingAsync(MovieRating rating)
    {
        await _context.Ratings.AddAsync(rating);
        await _context.SaveChangesAsync();
    }
}
