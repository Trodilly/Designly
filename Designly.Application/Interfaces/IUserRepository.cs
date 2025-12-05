using Designly.Domain;

namespace Designly.Application.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Retrieves a user by their username.
    /// </summary>
    /// <param name="username">The username to search for.</param>
    /// <returns>The user if found, otherwise null.</returns>
    User? GetByUsername(string username);

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A collection of users.</returns>
    IEnumerable<User> GetAll();
}
