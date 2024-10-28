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
    public class EstoqueMap : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.ToTable("Estoque");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd(); // Auto-generated ID

            builder.Property(p => p.Quantidade)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.DataEntrada)
                .HasColumnType("datetime")
                .IsRequired();

            // Relacionamento 1:N com Produto
            builder.HasOne(p => p.Produto)
                .WithMany()
                .HasForeignKey(p => p.Produto_Id)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
