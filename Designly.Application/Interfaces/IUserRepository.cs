using Designly.Domain;

namespace Designly.Application.Interfaces;

public interface IUserRepository
{
    User? GetByUsername(string username);
    IEnumerable<User> GetAll();
}
