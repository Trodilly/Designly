using Designly.Application.DTOs;

namespace Designly.Application.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Authenticates a user based on the provided credentials.
    /// </summary>
    /// <param name="request">The login request containing username and password.</param>
    /// <returns>A login response with a token if successful, otherwise null.</returns>
    LoginResponse? Login(LoginRequest request);
}
