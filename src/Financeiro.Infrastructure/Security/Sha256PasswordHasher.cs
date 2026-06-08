using System.Security.Cryptography;
using System.Text;
using Financeiro.Application.Interfaces.Security;

namespace Financeiro.Infrastructure.Security;

public class Sha256PasswordHasher : IPasswordHasher
{
    public string Hash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }

    public bool Verify(string input, string hash) => Hash(input) == hash.ToLowerInvariant();
}
