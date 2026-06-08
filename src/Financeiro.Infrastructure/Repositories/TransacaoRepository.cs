using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces.Repositories;
using Financeiro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.Infrastructure.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;

    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transacao transacao, CancellationToken cancellationToken = default)
    {
        await _context.Transacoes.AddAsync(transacao, cancellationToken);
    }

    public async Task<IReadOnlyList<Transacao>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Transacoes
            .AsNoTracking()
            .OrderByDescending(x => x.DataLancamento)
            .ToListAsync(cancellationToken);
    }

    public async Task<Transacao?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Transacoes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Remove(Transacao transacao)
    {
        _context.Transacoes.Remove(transacao);
    }

    public void Update(Transacao transacao)
    {
        _context.Transacoes.Update(transacao);
    }
}
