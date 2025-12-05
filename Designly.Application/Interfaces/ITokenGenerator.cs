using Designly.Domain;

namespace Designly.Application.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}
