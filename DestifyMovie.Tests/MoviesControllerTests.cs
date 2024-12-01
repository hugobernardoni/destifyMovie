using Xunit;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DestifyMovie.API.Controllers;
using DestifyMovie.Services.Interfaces;
using DestifyMovie.API.ViewModels;
using DestifyMovie.Data.Entities;

public class MoviesControllerTests
{
    private readonly Mock<IMovieService> _movieServiceMock;
    private readonly Mock<IActorService> _actorServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly MoviesController _controller;

    public MoviesControllerTests()
    {
        _movieServiceMock = new Mock<IMovieService>();
        _actorServiceMock = new Mock<IActorService>();
        _mapperMock = new Mock<IMapper>();
        _controller = new MoviesController(_movieServiceMock.Object, _actorServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkWithMovies()
    {
        // Arrange
        var movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Movie 1", ReleaseYear = 2020 },
            new Movie { Id = 2, Title = "Movie 2", ReleaseYear = 2021 }
        };
        var movieViewModels = new List<MovieViewModel>
        {
            new MovieViewModel { Id = 1, Title = "Movie 1", ReleaseYear = 2020 },
            new MovieViewModel { Id = 2, Title = "Movie 2", ReleaseYear = 2021 }
        };

        _movieServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(movies);
        _mapperMock.Setup(m => m.Map<List<MovieViewModel>>(movies)).Returns(movieViewModels);

        // Act
        var result = await _controller.GetAll() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var returnedMovies = Assert.IsType<List<MovieViewModel>>(result.Value);
        Assert.Equal(2, returnedMovies.Count);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenMovieDoesNotExist()
    {
        // Arrange
        _movieServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Movie)null);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
