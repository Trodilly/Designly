using Designly.Application.DTOs;
using Designly.Application.Interfaces;

namespace Designly.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthService(IUserRepository userRepository, ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    public LoginResponse? Login(LoginRequest request)
    {
        var user = _userRepository.GetByUsername(request.Username);
        if (user == null) return null;

        // In a real app, use password hashing (e.g., BCrypt).
        // For this exercise, we do simple string comparison as per original scope.
        if (user.Password != request.Password) return null;

        var token = _tokenGenerator.GenerateToken(user);
        return new LoginResponse(token);
    }
}
