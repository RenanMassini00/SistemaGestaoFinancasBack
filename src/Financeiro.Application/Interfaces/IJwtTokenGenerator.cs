using Financeiro.Application.DTOs.Auth;

namespace Financeiro.Application.Interfaces;

public interface IJwtTokenGenerator
{
    AuthResponseDto Generate(Guid userId, string name, string email, string role);
}
