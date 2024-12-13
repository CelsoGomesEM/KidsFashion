﻿// <auto-generated />
using System;
using KidsFashion.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KidsFashion.Persistencia.Migrations
{
    [DbContext(typeof(PersistContext))]
    [Migration("20241213121550_AdicionaPrecoEstoque")]
    partial class AdicionaPrecoEstoque
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("KidsFashion.Dominio.Categoria", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categoria", (string)null);
                });

            modelBuilder.Entity("KidsFashion.Dominio.Cliente", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Contato")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Endereco_Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Endereco_Id");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("KidsFashion.Dominio.Endereco", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Municipio_Id")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("Municipio_Id");

                    b.ToTable("Endereco", (string)null);
                });

            modelBuilder.Entity("KidsFashion.Dominio.Estoque", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Produto_Id")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Produto_Id");

                    b.ToTable("Estoque", (string)null);
                });

            modelBuilder.Entity("KidsFashion.Dominio.Fornecedor", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<string>("CPF_CNPJ")
                        .IsRequired()
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Contato")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Endereco_Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Endereco_Id");

                    b.ToTable("Fornecedor", (string)null);
                });

            modelBuilder.Entity("KidsFashion.Dominio.Municipio", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Id");

                    b.ToTable("Municipio", (string)null);
                });

            modelBuilder.Entity("KidsFashion.Dominio.Pedido", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<int>("Cliente_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Cliente_Id");

                    b.ToTable("Pedido", (string)null);
                });

            modelBuilder.Entity("KidsFashion.Dominio.PedidoProduto", b =>
                {
                    b.Property<int>("Pedido_Id")
                        .HasColumnType("int");

                    b.Property<int>("Produto_Id")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Pedido_Id", "Produto_Id");

                    b.HasIndex("Produto_Id");

                    b.ToTable("PedidoProduto", (string)null);
                });

            modelBuilder.Entity("KidsFashion.Dominio.Produto", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<int>("Categoria_Id")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Fornecedor_Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Categoria_Id");

                    b.HasIndex("Fornecedor_Id");

                    b.ToTable("Produto", (string)null);
                });

            modelBuilder.Entity("KidsFashion.Dominio.Cliente", b =>
                {
                    b.HasOne("KidsFashion.Dominio.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("Endereco_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("KidsFashion.Dominio.Endereco", b =>
                {
                    b.HasOne("KidsFashion.Dominio.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("Municipio_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Municipio");
                });

            modelBuilder.Entity("KidsFashion.Dominio.Estoque", b =>
                {
                    b.HasOne("KidsFashion.Dominio.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("Produto_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("KidsFashion.Dominio.Fornecedor", b =>
                {
                    b.HasOne("KidsFashion.Dominio.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("Endereco_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("KidsFashion.Dominio.Pedido", b =>
                {
                    b.HasOne("KidsFashion.Dominio.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("Cliente_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("KidsFashion.Dominio.PedidoProduto", b =>
                {
                    b.HasOne("KidsFashion.Dominio.Pedido", "Pedido")
                        .WithMany("PedidoProdutos")
                        .HasForeignKey("Pedido_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KidsFashion.Dominio.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("Produto_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("KidsFashion.Dominio.Produto", b =>
                {
                    b.HasOne("KidsFashion.Dominio.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("Categoria_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("KidsFashion.Dominio.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("Fornecedor_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("KidsFashion.Dominio.Pedido", b =>
                {
                    b.Navigation("PedidoProdutos");
                });
#pragma warning restore 612, 618
        }
    }
}
