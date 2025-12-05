using Designly.Application.DTOs;
using Designly.Application.Interfaces;
using Designly.Application.Services;
using Designly.Domain;
using Moq;
using Xunit;

namespace Designly.Tests;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ITokenGenerator> _tokenGeneratorMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _tokenGeneratorMock = new Mock<ITokenGenerator>();
        _authService = new AuthService(_userRepositoryMock.Object, _tokenGeneratorMock.Object);
    }

    [Fact]
    public void Login_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.GetByUsername("unknown")).Returns((User?)null);
        var request = new LoginRequest("unknown", "password");

        // Act
        var result = _authService.Login(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Login_ShouldReturnNull_WhenPasswordIsInvalid()
    {
        // Arrange
        var user = new User { Username = "test", Password = "correct_password" };
        _userRepositoryMock.Setup(x => x.GetByUsername("test")).Returns(user);
        var request = new LoginRequest("test", "wrong_password");

        // Act
        var result = _authService.Login(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Login_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var user = new User { Username = "test", Password = "correct_password" };
        _userRepositoryMock.Setup(x => x.GetByUsername("test")).Returns(user);
        _tokenGeneratorMock.Setup(x => x.GenerateToken(user)).Returns("generated_token");
        var request = new LoginRequest("test", "correct_password");

        // Act
        var result = _authService.Login(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("generated_token", result.Token);
    }
}
