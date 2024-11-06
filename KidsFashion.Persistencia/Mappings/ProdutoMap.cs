using KidsFashion.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Persistencia.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd(); // Auto-generated ID

            builder.Property(p => p.Nome)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnType("nvarchar(250)")
                .IsRequired();

            // Relacionamento 1:N com Fornecedor
            builder.HasOne(p => p.Fornecedor)
                .WithMany()
                .HasForeignKey(p => p.Fornecedor_Id)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            // Relacionamento 1:N com Categoria
            builder.HasOne(p => p.Categoria)
                .WithMany()
                .HasForeignKey(p => p.Categoria_Id)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
