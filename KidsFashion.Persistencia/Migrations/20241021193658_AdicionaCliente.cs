using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsFashion.Persistencia.Migrations
{
    public partial class AdicionaCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    Contato = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Endereco_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_Endereco_Endereco_Id",
                        column: x => x.Endereco_Id,
                        principalTable: "Endereco",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Endereco_Id",
                table: "Cliente",
                column: "Endereco_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
