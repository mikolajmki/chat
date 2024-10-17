namespace Tests.UnitTests.Services;

using Moq;
using Xunit;
using Application.ApplicationModels;
using Application.Domain;
using Application.Services;
using Application.Abstractions;
using MapsterMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<UserService>> _loggerMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void AddUserToChat_UserExistsAndIsActive_ReturnsFalse()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var userDto = new UserDto { Name = "Alice" };

        _userRepositoryMock.Setup(repo => repo.IsExisting(userDto.Name)).Returns(true);
        _userRepositoryMock.Setup(repo => repo.GetUserIdByName(userDto.Name)).Returns(guid);
        _userRepositoryMock.Setup(repo => repo.IsActive(guid)).Returns(true);

        // Act
        var result = _userService.AddUserToChat(userDto);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUserToChat_UserExistsAndIsInactive_ActivatesUserReturnsTrue()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var userDto = new UserDto { Name = "Bob" };

        _userRepositoryMock.Setup(repo => repo.IsExisting(userDto.Name)).Returns(true);
        _userRepositoryMock.Setup(repo => repo.GetUserIdByName(userDto.Name)).Returns(guid);
        _userRepositoryMock.Setup(repo => repo.IsActive(guid)).Returns(false);

        // Act
        var result = _userService.AddUserToChat(userDto);

        // Assert
        Assert.True(result);
        _userRepositoryMock.Verify(repo => repo.Activate(guid), Times.Once);
    }

    [Fact]
    public void AddUserToChat_UserDoesNotExist_AddsUserReturnsTrue()
    {
        // Arrange
        var userDto = new UserDto { Name = "Charlie" };

        _userRepositoryMock.Setup(repo => repo.IsExisting(userDto.Name)).Returns(false);

        var user = new User { Name = "Charlie" };
        user.GenerateId();

        _mapperMock.Setup(m => m.Map<User>(userDto)).Returns(user);

        _userRepositoryMock.Setup(repo => repo.AddUser(user)).Verifiable();

        // Act
        var result = _userService.AddUserToChat(userDto);

        // Assert
        Assert.True(result);
        _userRepositoryMock.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public void DeactivateUserByConnectionId_CallsDeactivateByConnectionId()
    {
        // Arrange
        var connectionId = "some-connection-id";

        // Act
        _userService.DeactivateByConnectionId(connectionId);

        // Assert
        _userRepositoryMock.Verify(repo => repo.DeactivateByConnectionId(connectionId), Times.Once);
    }
}
