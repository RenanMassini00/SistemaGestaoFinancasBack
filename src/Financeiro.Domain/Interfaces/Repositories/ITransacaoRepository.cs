using Financeiro.Domain.Entities;

namespace Financeiro.Domain.Interfaces.Repositories;

public interface ITransacaoRepository
{
    Task<IReadOnlyList<Transacao>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Transacao?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Transacao transacao, CancellationToken cancellationToken = default);
    void Update(Transacao transacao);
    void Remove(Transacao transacao);
}
