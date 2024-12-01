using DestifyMovie.Data.Entities;
using DestifyMovie.Repositories.Interfaces;
using DestifyMovie.Services.Interfaces;
using DestifyMovie.Services;

public class MovieService : GenericService<Movie>, IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository) : base(movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<Movie>> SearchByNameAsync(string title)
    {
        return await _movieRepository.FindByNameAsync(m => m.Title.Contains(title));
    }
}