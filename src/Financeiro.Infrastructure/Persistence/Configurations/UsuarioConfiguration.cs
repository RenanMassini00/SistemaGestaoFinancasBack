using Financeiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Infrastructure.Persistence.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(150).IsRequired();
        builder.Property(x => x.PasswordHash).HasColumnName("password_hash").HasMaxLength(128).IsRequired();
        builder.Property(x => x.Role).HasColumnName("role").HasMaxLength(50).IsRequired();
        builder.Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        builder.HasIndex(x => x.Email).IsUnique();
    }
}
