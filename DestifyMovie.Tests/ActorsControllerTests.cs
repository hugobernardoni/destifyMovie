using Xunit;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DestifyMovie.API.Controllers;
using DestifyMovie.Services.Interfaces;
using DestifyMovie.API.ViewModels;
using DestifyMovie.Data.Entities;

public class ActorsControllerTests
{
    private readonly Mock<IActorService> _actorServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ActorsController _controller;

    public ActorsControllerTests()
    {
        _actorServiceMock = new Mock<IActorService>();
        _mapperMock = new Mock<IMapper>();
        _controller = new ActorsController(_actorServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkWithActors()
    {
        // Arrange
        var actors = new List<Actor>
        {
            new Actor { Id = 1, Name = "Actor 1" },
            new Actor { Id = 2, Name = "Actor 2" }
        };
        var actorViewModels = new List<ActorViewModel>
        {
            new ActorViewModel { Id = 1, Name = "Actor 1" },
            new ActorViewModel { Id = 2, Name = "Actor 2" }
        };

        _actorServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(actors);
        _mapperMock.Setup(m => m.Map<List<ActorViewModel>>(actors)).Returns(actorViewModels);

        // Act
        var result = await _controller.GetAll() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var returnedActors = Assert.IsType<List<ActorViewModel>>(result.Value);
        Assert.Equal(2, returnedActors.Count);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenActorDoesNotExist()
    {
        // Arrange
        _actorServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Actor)null);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
