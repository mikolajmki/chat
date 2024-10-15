namespace UnitTests.Services;

using Moq;
using Xunit;
using Application.Abstractions;
using Application.ApplicationModels;
using Application.Domain;
using Application.Services;
using MapsterMapper;

public class StatsServiceTests
{
    private readonly Mock<IStatsRepository> _statsRepositoryMock;
    private readonly Mock<IMapper> _mapperMock; // Assume this is set up properly
    private readonly StatsService _statsService;

    public StatsServiceTests()
    {
        _statsRepositoryMock = new Mock<IStatsRepository>();
        _mapperMock = new Mock<IMapper>();
        _statsService = new StatsService(_statsRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void GetStats_ReturnsMappedStatsDto()
    {
        // Arrange
        var stats = new Stats
        {
            ActiveUsers = [new User { Name = "Charlie" }],
            InActiveUsers = [new User { Name = "Charlie" }],
            IsActiveCount = 1
        };
        _statsRepositoryMock.Setup(repo => repo.GetStats()).Returns(stats);

        var expectedStatsDto = new StatsDto
        {
            ActiveUsers = [new UserDto { Name = "Charlie" }],
            InActiveUsers = [new UserDto { Name = "Charlie" }],
            IsActiveCount = 1
        };

        _mapperMock.Setup(m => m.Map<StatsDto>(stats)).Returns(expectedStatsDto);

        // Act
        var result = _statsService.GetStats();

        // Assert
        Assert.Equal(expectedStatsDto, result);
        _statsRepositoryMock.Verify(repo => repo.GetStats(), Times.Once);
    }
}
