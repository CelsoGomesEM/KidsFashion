using KidsFashion.Dominio;
using KidsFashion.Persistencia.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Persistencia
{
    public class PersistContext : DbContext
    {
        public PersistContext() { }

        public PersistContext(DbContextOptions<PersistContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //HOMOLOGAÇÃO
            optionsBuilder.UseSqlServer("Data Source=MSSQLHMLG;Initial Catalog=Teste2;Persist Security Info=True;User Id=sa;Password=A$$!nf998");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());
            modelBuilder.ApplyConfiguration(new MunicipioMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());  
            modelBuilder.ApplyConfiguration(new ClienteMap());  
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Produto> Produto { get; set; }

        public DbSet<Cliente> Cliente { get; set; }



    }
}
