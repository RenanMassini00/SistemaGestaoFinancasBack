using Financeiro.Domain.Entities;

namespace Financeiro.Domain.Interfaces.Repositories;

public interface IInvestimentoRepository
{
    Task<IReadOnlyList<Investimento>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Investimento?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Investimento investimento, CancellationToken cancellationToken = default);
    void Update(Investimento investimento);
    void Remove(Investimento investimento);
}
