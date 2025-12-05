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

    public User? GetByUsername(string username) => _users.FirstOrDefault(u => u.Username == username);

    public IEnumerable<User> GetAll() => _users;
}
