using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces.Repositories;
using Financeiro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}
