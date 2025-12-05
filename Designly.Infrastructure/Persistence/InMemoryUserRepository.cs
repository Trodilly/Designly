using Designly.Application.Interfaces;
using Designly.Domain;

namespace Designly.Infrastructure.Persistence;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new()
    {
        new User { Id = Guid.NewGuid(), Username = "admin", Password = "password123" },
        new User { Id = Guid.NewGuid(), Username = "test", Password = "password123" },
        new User { Id = Guid.NewGuid(), Username = "user", Password = "userpass123" }
    };

    /// <summary>
    /// Retrieves a user by their username.
    /// </summary>
    /// <param name="username">The username to search for.</param>
    /// <returns>The user if found, otherwise null.</returns>
    public User? GetByUsername(string username) => _users.FirstOrDefault(u => u.Username == username);

    /// <summary>
    /// Retrieves all users from the in-memory store.
    /// </summary>
    /// <returns>A collection of users.</returns>
    public IEnumerable<User> GetAll() => _users;
}
