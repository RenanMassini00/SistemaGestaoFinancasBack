using Financeiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Infrastructure.Persistence.Configurations;

public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("transacoes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(200).IsRequired();
        builder.Property(x => x.Valor).HasColumnName("valor").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.DataLancamento).HasColumnName("data_lancamento").IsRequired();
        builder.Property(x => x.Tipo).HasColumnName("tipo").HasConversion<int>().IsRequired();
        builder.Property(x => x.Categoria).HasColumnName("categoria").HasMaxLength(120).IsRequired();
        builder.Property(x => x.Status).HasColumnName("status").HasMaxLength(80).IsRequired();
        builder.Property(x => x.Conciliado).HasColumnName("conciliado").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
    }
}
