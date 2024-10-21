using Application.Abstractions;
using Application.ApplicationModels;
using Application.Domain;
using Application.Services;
using Application.Validation;
using MapsterMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Tests.UnitTests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly NullLogger<UserService> _loggerMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new NullLogger<UserService>();
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object, _loggerMock);
    }

    [Fact]
    public void AddUserToChat_ValidUser_ShouldAddUser()
    {
        // Arrange
        var userDto = new UserDto { Name = "ValidUser", ConnectionId = "123" };
        var user = new User { Name = "ValidUser" };
        user.SetConnectionId(userDto.ConnectionId);

        _mapperMock.Setup(m => m.Map<User>(userDto)).Returns(user);
        _userRepositoryMock.Setup(r => r.IsExisting(user.Name)).Returns(false);
        _userRepositoryMock.Setup(r => r.AddUser(user));

        // Act
        var result = _userService.AddUserToChat(userDto);

        // Assert
        Assert.True(result);
        _userRepositoryMock.Verify(r => r.AddUser(user), Times.Once);
    }

    [Fact]
    public void AddUserToChat_InvalidUser_ShouldReturnFalse()
    {
        // Arrange
        var userDto = new UserDto { Name = "" }; // Invalid user
        var user = new User { Name = "" };

        var userValidator = new UserValidator();
        _mapperMock.Setup(m => m.Map<User>(userDto)).Returns(user);
        _userRepositoryMock.Setup(r => r.IsExisting(user.Name)).Returns(false);

        // Act
        var result = _userService.AddUserToChat(userDto);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUserToChat_ExistingUserActive_ShouldReturnFalse()
    {
        // Arrange
        var userDto = new UserDto { Name = "ExistingUser" };

        var existingUser = new User { Name = "ExistingUser" };
        
        existingUser.SetIsActive(false);

        _mapperMock.Setup(m => m.Map<User>(userDto)).Returns(existingUser);
        _userRepositoryMock.Setup(r => r.IsExisting(existingUser.Name)).Returns(true);
        _userRepositoryMock.Setup(r => r.GetUserByName(existingUser.Name)).Returns(existingUser);

        // Act
        var result = _userService.AddUserToChat(userDto);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUserToChat_ExistingUserInactive_ShouldActivateUser()
    {
        // Arrange
        var userDto = new UserDto { Name = "InactiveUser", ConnectionId = "123", Id = Guid.Empty };
        var existingUser = new User { Name = "InactiveUser" };

        existingUser.SetConnectionId("123");
        existingUser.SetIsActive(false);

        _mapperMock.Setup(m => m.Map<User>(userDto)).Returns(existingUser);
        _userRepositoryMock.Setup(r => r.IsExisting(existingUser.Name)).Returns(true);
        _userRepositoryMock.Setup(r => r.GetUserByName(existingUser.Name)).Returns(existingUser);

        // Act
        var result = _userService.AddUserToChat(userDto);

        // Assert
        Assert.True(result);
        _userRepositoryMock.Verify(r => r.Activate(existingUser), Times.Once);
        Assert.Equal("123", existingUser.ConnectionId);
    }
}
