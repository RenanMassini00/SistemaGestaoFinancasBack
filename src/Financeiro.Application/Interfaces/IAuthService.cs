using Financeiro.Application.Common;
using Financeiro.Application.DTOs.Auth;

namespace Financeiro.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
}
