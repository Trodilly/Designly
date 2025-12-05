using Designly.Application.DTOs;

namespace Designly.Application.Interfaces;

public interface IAuthService
{
    LoginResponse? Login(LoginRequest request);
}
