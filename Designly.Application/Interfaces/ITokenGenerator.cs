using Designly.Domain;

namespace Designly.Application.Interfaces;

public interface ITokenGenerator
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <returns>A signed JWT token string.</returns>
    string GenerateToken(User user);
}
