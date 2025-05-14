using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Auth
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
