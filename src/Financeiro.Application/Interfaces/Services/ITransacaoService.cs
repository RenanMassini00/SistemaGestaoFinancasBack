using Financeiro.Application.Common;
using Financeiro.Application.DTOs.Transacoes;

namespace Financeiro.Application.Interfaces.Services;

public interface ITransacaoService
{
    Task<Result<IReadOnlyList<TransacaoResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<TransacaoResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<TransacaoResponseDto>> CreateAsync(TransacaoRequestDto request, CancellationToken cancellationToken = default);
    Task<Result<TransacaoResponseDto>> UpdateAsync(Guid id, TransacaoRequestDto request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
