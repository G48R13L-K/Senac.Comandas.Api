using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comandas.Api.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComandasItem_Comandas_ComandaId",
                table: "ComandasItem");

            migrationBuilder.DropForeignKey(
                name: "FK_pedidoCozinhaItems_ComandasItem_ComandaItemId",
                table: "pedidoCozinhaItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComandasItem",
                table: "ComandasItem");

            migrationBuilder.RenameTable(
                name: "ComandasItem",
                newName: "ComandaItens");

            migrationBuilder.RenameIndex(
                name: "IX_ComandasItem_ComandaId",
                table: "ComandaItens",
                newName: "IX_ComandaItens_ComandaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComandaItens",
                table: "ComandaItens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComandaItens_Comandas_ComandaId",
                table: "ComandaItens",
                column: "ComandaId",
                principalTable: "Comandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedidoCozinhaItems_ComandaItens_ComandaItemId",
                table: "pedidoCozinhaItems",
                column: "ComandaItemId",
                principalTable: "ComandaItens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComandaItens_Comandas_ComandaId",
                table: "ComandaItens");

            migrationBuilder.DropForeignKey(
                name: "FK_pedidoCozinhaItems_ComandaItens_ComandaItemId",
                table: "pedidoCozinhaItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComandaItens",
                table: "ComandaItens");

            migrationBuilder.RenameTable(
                name: "ComandaItens",
                newName: "ComandasItem");

            migrationBuilder.RenameIndex(
                name: "IX_ComandaItens_ComandaId",
                table: "ComandasItem",
                newName: "IX_ComandasItem_ComandaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComandasItem",
                table: "ComandasItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComandasItem_Comandas_ComandaId",
                table: "ComandasItem",
                column: "ComandaId",
                principalTable: "Comandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedidoCozinhaItems_ComandasItem_ComandaItemId",
                table: "pedidoCozinhaItems",
                column: "ComandaItemId",
                principalTable: "ComandasItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
