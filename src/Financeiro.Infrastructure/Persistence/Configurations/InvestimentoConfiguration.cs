using Financeiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Infrastructure.Persistence.Configurations;

public class InvestimentoConfiguration : IEntityTypeConfiguration<Investimento>
{
    public void Configure(EntityTypeBuilder<Investimento> b)
    {
        b.ToTable("investimentos");
        b.HasKey(x => x.Id);

        b.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(150).IsRequired();
        b.Property(x => x.Tipo).HasColumnName("tipo").HasMaxLength(80).IsRequired();
        b.Property(x => x.Instituicao).HasColumnName("instituicao").HasMaxLength(150).IsRequired();
        b.Property(x => x.ValorInvestido).HasColumnName("valor_investido").HasPrecision(18, 2).IsRequired();
        b.Property(x => x.SaldoAtual).HasColumnName("saldo_atual").HasPrecision(18, 2).IsRequired();
        b.Property(x => x.MetaValor).HasColumnName("meta_valor").HasPrecision(18, 2);
        b.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        b.HasMany(x => x.Atualizacoes)
            .WithOne()
            .HasForeignKey(x => x.InvestimentoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
