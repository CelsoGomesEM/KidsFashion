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
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        void IEntityTypeConfiguration<Pedido>.Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DataPedido)
                .IsRequired();

            builder.HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.Cliente_Id)
                .OnDelete(DeleteBehavior.NoAction);

            // Configura o relacionamento N:N com PedidoProduto
            builder.HasMany(p => p.PedidoProdutos)
                .WithOne(pp => pp.Pedido)
                .HasForeignKey(pp => pp.Pedido_Id);
        }
    }
}
