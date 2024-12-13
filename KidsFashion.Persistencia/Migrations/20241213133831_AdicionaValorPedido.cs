using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsFashion.Persistencia.Migrations
{
    public partial class AdicionaValorPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "PedidoProduto",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "PedidoProduto");
        }
    }
}
