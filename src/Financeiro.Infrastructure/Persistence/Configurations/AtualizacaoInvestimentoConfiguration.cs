using Financeiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Infrastructure.Persistence.Configurations;

public class AtualizacaoInvestimentoConfiguration : IEntityTypeConfiguration<AtualizacaoInvestimento>
{
    public void Configure(EntityTypeBuilder<AtualizacaoInvestimento> b)
    {
        b.ToTable("atualizacoes_investimento");
        b.HasKey(x => x.Id);

        b.Property(x => x.InvestimentoId).HasColumnName("investimento_id").IsRequired();
        b.Property(x => x.Saldo).HasColumnName("saldo").HasPrecision(18, 2).IsRequired();
        b.Property(x => x.Data).HasColumnName("data_atualizacao").IsRequired();
        b.Property(x => x.Observacao).HasColumnName("observacao").HasMaxLength(500);
        b.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at");
    }
}
