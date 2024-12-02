using DestifyMovie.Data.Entities;

namespace DestifyMovie.Services.Interfaces
{
    public interface IMovieService : IGenericService<Movie>
    {
        Task<IEnumerable<Movie>> SearchByNameAsync(string title);
        Task AddRatingAsync(MovieRating rating);

    }
}
