using Financeiro.Application.Common;
using Financeiro.Application.DTOs.Auth;
using Financeiro.Application.Interfaces;
using Financeiro.Application.Interfaces.Security;
using Financeiro.Domain.Interfaces.Repositories;

namespace Financeiro.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUsuarioRepository usuarioRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        var user = await _usuarioRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null || !user.Ativo)
            return Result<AuthResponseDto>.Fail("Usuário ou senha inválidos.");

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            return Result<AuthResponseDto>.Fail("Usuário ou senha inválidos.");

        var auth = _jwtTokenGenerator.Generate(user.Id, user.Nome, user.Email, user.Role);
        return Result<AuthResponseDto>.Ok(auth, "Login realizado com sucesso.");
    }
}
