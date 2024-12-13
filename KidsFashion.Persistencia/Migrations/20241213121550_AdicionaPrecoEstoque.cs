using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsFashion.Persistencia.Migrations
{
    public partial class AdicionaPrecoEstoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecoUnitario",
                table: "Estoque",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoUnitario",
                table: "Estoque");
        }
    }
}
