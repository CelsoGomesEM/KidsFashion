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
    public class PedidoProdutoMap : IEntityTypeConfiguration<PedidoProduto>
    {
        public void Configure(EntityTypeBuilder<PedidoProduto> builder)
        {
            builder.ToTable("PedidoProduto");

            // Definir a chave composta
            builder.HasKey(pp => new { pp.Pedido_Id, pp.Produto_Id });

            builder.Property(pp => pp.Quantidade)
                   .HasColumnType("int")
                   .IsRequired();

            builder.HasOne(pp => pp.Pedido)
                .WithMany(p => p.PedidoProdutos)
                .HasForeignKey(pp => pp.Pedido_Id);

            builder.HasOne(pp => pp.Produto)
                .WithMany() // Produto não conhece PedidoProduto
                .HasForeignKey(pp => pp.Produto_Id);
        }
    }
}
