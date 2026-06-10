using Financeiro.Domain.Entities;
namespace Financeiro.Domain.Interfaces.Repositories;
public interface IDespesaPeriodoRepository {
    Task AddAsync(DespesaPeriodo entity,CancellationToken cancellationToken=default);
    Task<DespesaPeriodo?> GetByIdAsync(Guid id,CancellationToken cancellationToken=default);
    void Remove(DespesaPeriodo entity);
}
