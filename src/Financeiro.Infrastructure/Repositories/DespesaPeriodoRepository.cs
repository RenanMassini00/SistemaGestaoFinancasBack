using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces.Repositories;
using Financeiro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.Infrastructure.Repositories;

public class DespesaPeriodoRepository : IDespesaPeriodoRepository
{
    private readonly AppDbContext _ctx;

    public DespesaPeriodoRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task AddAsync(DespesaPeriodo entity, CancellationToken cancellationToken = default)
    {
        await _ctx.DespesasPeriodo.AddAsync(entity, cancellationToken);
    }

    public async Task<DespesaPeriodo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _ctx.DespesasPeriodo.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Update(DespesaPeriodo entity)
    {
        _ctx.DespesasPeriodo.Update(entity);
    }

    public void Remove(DespesaPeriodo entity)
    {
        _ctx.DespesasPeriodo.Remove(entity);
    }
}