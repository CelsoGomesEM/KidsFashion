using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsFashion.Persistencia.Migrations
{
    public partial class AdicionaContextoPedidoEPedidoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cliente_Id = table.Column<int>(type: "int", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_Cliente_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PedidoProduto",
                columns: table => new
                {
                    Pedido_Id = table.Column<int>(type: "int", nullable: false),
                    Produto_Id = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoProduto", x => new { x.Pedido_Id, x.Produto_Id });
                    table.ForeignKey(
                        name: "FK_PedidoProduto_Pedido_Pedido_Id",
                        column: x => x.Pedido_Id,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoProduto_Produto_Produto_Id",
                        column: x => x.Produto_Id,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Cliente_Id",
                table: "Pedido",
                column: "Cliente_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProduto_Produto_Id",
                table: "PedidoProduto",
                column: "Produto_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoProduto");

            migrationBuilder.DropTable(
                name: "Pedido");
        }
    }
}
