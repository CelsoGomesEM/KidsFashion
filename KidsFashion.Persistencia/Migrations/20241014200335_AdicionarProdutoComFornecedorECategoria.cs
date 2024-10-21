using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsFashion.Persistencia.Migrations
{
    public partial class AdicionarProdutoComFornecedorECategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Categoria_Id = table.Column<int>(type: "int", nullable: false),
                    Fornecedor_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_Categoria_Id",
                        column: x => x.Categoria_Id,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produto_Fornecedor_Fornecedor_Id",
                        column: x => x.Fornecedor_Id,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_Categoria_Id",
                table: "Produto",
                column: "Categoria_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_Fornecedor_Id",
                table: "Produto",
                column: "Fornecedor_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");
        }
    }
}
