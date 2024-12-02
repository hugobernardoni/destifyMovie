using DestifyMovie.Data.Entities;

namespace DestifyMovie.Repositories.Interfaces;

public interface IMovieRepository : IRepository<Movie>
{
    Task<IEnumerable<Movie>> SearchByNameAsync(string title);
    Task AddRatingAsync(MovieRating rating);
}
