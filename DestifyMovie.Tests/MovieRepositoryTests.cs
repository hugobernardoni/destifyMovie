using Xunit;
using Microsoft.EntityFrameworkCore;
using DestifyMovie.Data;
using DestifyMovie.Repositories.Implementations;
using DestifyMovie.Data.Entities;

public class MovieRepositoryTests
{
    [Fact]
    public async Task AddAsync_ShouldAddMovieToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        using var context = new AppDbContext(options);
        var repository = new MovieRepository(context);

        var movie = new Movie { Title = "Test Movie", ReleaseYear = 2022 };

        // Act
        await repository.AddAsync(movie);
        var savedMovie = await context.Movies.FirstOrDefaultAsync(m => m.Title == "Test Movie");

        // Assert
        Assert.NotNull(savedMovie);
        Assert.Equal("Test Movie", savedMovie.Title);
    }
}
