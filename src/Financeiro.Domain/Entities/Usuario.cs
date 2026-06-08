using Financeiro.Domain.Common;

namespace Financeiro.Domain.Entities;

public class Usuario : BaseEntity
{
    private Usuario() { }

    public Usuario(string nome, string email, string passwordHash, string role)
    {
        Nome = nome;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Role { get; private set; } = "Admin";
    public bool Ativo { get; private set; } = true;
}
