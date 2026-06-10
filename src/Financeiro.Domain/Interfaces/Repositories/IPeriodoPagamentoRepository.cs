using Financeiro.Domain.Entities;
namespace Financeiro.Domain.Interfaces.Repositories;
public interface IPeriodoPagamentoRepository {
    Task<IReadOnlyList<PeriodoPagamento>> GetAllAsync(CancellationToken cancellationToken=default);
    Task<PeriodoPagamento?> GetByIdAsync(Guid id,CancellationToken cancellationToken=default);
    Task AddAsync(PeriodoPagamento entity,CancellationToken cancellationToken=default);
    void Update(PeriodoPagamento entity);
    void Remove(PeriodoPagamento entity);
}
