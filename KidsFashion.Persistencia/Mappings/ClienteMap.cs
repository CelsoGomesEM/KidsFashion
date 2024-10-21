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
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd(); // Auto-generated ID

            builder.Property(p => p.Nome)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(p => p.CPF)
                .HasColumnType("nvarchar(14)")
                .IsRequired();

            builder.Property(p => p.Contato)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            // Relacionamento 1:N com Endereco
            builder.HasOne(p => p.Endereco)
                .WithMany()
                .HasForeignKey(p => p.Endereco_Id)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
