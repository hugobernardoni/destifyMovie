using DestifyMovie.Data.Entities;
using DestifyMovie.Repositories.Interfaces;
using DestifyMovie.Services.Interfaces;
using DestifyMovie.Services;
using DestifyMovie.Repositories.Implementations;

public class MovieService : GenericService<Movie>, IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository) : base(movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<Movie>> SearchByNameAsync(string title)
    {
        return await _movieRepository.SearchByNameAsync(title);
    }

    public async Task AddRatingAsync(MovieRating rating)
    {
        if (rating.Rating < 1 || rating.Rating > 5)
        {
            throw new ArgumentException("Rating must be between 1 and 5.");
        }

        await _movieRepository.AddRatingAsync(rating);
    }
}