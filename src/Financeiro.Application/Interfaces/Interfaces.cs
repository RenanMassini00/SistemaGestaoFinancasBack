using Financeiro.Application.Common;
using Financeiro.Application.DTOs;

namespace Financeiro.Application.Interfaces;

public interface IJwtTokenGenerator
{
    AuthResponseDto Generate(Guid userId, string name, string email, string role);
}

public interface IAuthService
{
    Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
}

public interface IPeriodoService
{
    Task<Result<IReadOnlyList<PeriodoResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<PeriodoResponseDto>> CreateAsync(PeriodoRequestDto request, CancellationToken cancellationToken = default);
    Task<Result<PeriodoResponseDto>> UpdateAsync(Guid id, PeriodoRequestDto request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<PeriodoResponseDto>> AddDespesaAsync(Guid periodoId, DespesaRequestDto request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteDespesaAsync(Guid periodoId, Guid despesaId, CancellationToken cancellationToken = default);
}