using Designly.Application.DTOs;
using Designly.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Designly.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public UserController(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    /// <param name="loginRequest">The user credentials.</param>
    /// <returns>A JWT token if credentials are valid.</returns>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var result = _authService.Login(loginRequest);
        if (result == null)
        {
            return Unauthorized("Invalid credentials");
        }

        return Ok(result);
    }

    /// <summary>
    /// Retrieves a list of all users. Requires authentication.
    /// </summary>
    /// <returns>A list of users.</returns>
    [Authorize]
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userRepository.GetAll();
        var userDtos = users.Select(u => new UserDto(u.Id, u.Username));
        return Ok(userDtos);
    }
}