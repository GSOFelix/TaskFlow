using TaskFlow.Domain.Interfaces.Services;

namespace TaskFlow.Services
{
    public class PassWordService : IPassWordService
    {
        public string Hash(string passWord)
        {
            return BCrypt.Net.BCrypt.HashPassword(passWord);
        }

        public bool Verify(string passWord, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(passWord, hash);
        }
    }
}
