using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Comandas.Api.Migrations
{
    /// <inheritdoc />
    public partial class categorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaCardapioId",
                table: "cardapioItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoriaCardapio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaCardapio", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CategoriaCardapio",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, null, "Lanche" },
                    { 2, null, "Bebidas" },
                    { 3, null, "Acompanhamentos" }
                });

            migrationBuilder.UpdateData(
                table: "cardapioItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoriaCardapioId",
                value: null);

            migrationBuilder.UpdateData(
                table: "cardapioItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoriaCardapioId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_cardapioItems_CategoriaCardapioId",
                table: "cardapioItems",
                column: "CategoriaCardapioId");

            migrationBuilder.AddForeignKey(
                name: "FK_cardapioItems_CategoriaCardapio_CategoriaCardapioId",
                table: "cardapioItems",
                column: "CategoriaCardapioId",
                principalTable: "CategoriaCardapio",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cardapioItems_CategoriaCardapio_CategoriaCardapioId",
                table: "cardapioItems");

            migrationBuilder.DropTable(
                name: "CategoriaCardapio");

            migrationBuilder.DropIndex(
                name: "IX_cardapioItems_CategoriaCardapioId",
                table: "cardapioItems");

            migrationBuilder.DropColumn(
                name: "CategoriaCardapioId",
                table: "cardapioItems");
        }
    }
}
