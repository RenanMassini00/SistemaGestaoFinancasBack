using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces.Repositories;
using Financeiro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.Infrastructure.Repositories;

public class InvestimentoRepository : IInvestimentoRepository
{
    private readonly AppDbContext _context;

    public InvestimentoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Investimento>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Investimentos
            .Include(x => x.Atualizacoes)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public Task<Investimento?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _context.Investimentos
            .Include(x => x.Atualizacoes)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddAsync(Investimento investimento, CancellationToken cancellationToken = default) =>
        await _context.Investimentos.AddAsync(investimento, cancellationToken);

    public void Update(Investimento investimento) =>
        _context.Entry(investimento).State = EntityState.Modified;

    public void Remove(Investimento investimento) => _context.Investimentos.Remove(investimento);
}
