using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Security.Interface;

public interface ITokenService
{
    string GenerateToken(User user);
}
