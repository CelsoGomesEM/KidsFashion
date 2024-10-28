using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsFashion.Persistencia.Migrations
{
    public partial class AdicionaEstoqueProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Endereco_Endereco_Id",
                table: "Cliente");

            migrationBuilder.CreateTable(
                name: "Estoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataEntrada = table.Column<DateTime>(type: "datetime", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Produto_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estoque_Produto_Produto_Id",
                        column: x => x.Produto_Id,
                        principalTable: "Produto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_Produto_Id",
                table: "Estoque",
                column: "Produto_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Endereco_Endereco_Id",
                table: "Cliente",
                column: "Endereco_Id",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Endereco_Endereco_Id",
                table: "Cliente");

            migrationBuilder.DropTable(
                name: "Estoque");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Endereco_Endereco_Id",
                table: "Cliente",
                column: "Endereco_Id",
                principalTable: "Endereco",
                principalColumn: "Id");
        }
    }
}
