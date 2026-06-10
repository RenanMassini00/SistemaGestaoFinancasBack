using Financeiro.Application.Common; using Financeiro.Application.DTOs; using Financeiro.Application.Interfaces; using Financeiro.Application.Interfaces.Security; using Financeiro.Domain.Interfaces.Repositories;
namespace Financeiro.Application.Services;
public class AuthService : IAuthService {
  private readonly IUsuarioRepository _repo; private readonly IPasswordHasher _hasher; private readonly IJwtTokenGenerator _jwt;
  public AuthService(IUsuarioRepository repo, IPasswordHasher hasher, IJwtTokenGenerator jwt){_repo=repo;_hasher=hasher;_jwt=jwt;}
  public async Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request,CancellationToken cancellationToken=default){ var u=await _repo.GetByEmailAsync(request.Email,cancellationToken); if(u is null || !u.Ativo || !_hasher.Verify(request.Password,u.PasswordHash)) return Result<AuthResponseDto>.Fail("Usuário ou senha inválidos."); return Result<AuthResponseDto>.Ok(_jwt.Generate(u.Id,u.Nome,u.Email,u.Role),"Login realizado com sucesso."); }
}
