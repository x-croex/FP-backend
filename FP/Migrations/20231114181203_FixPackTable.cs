using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FP.Migrations
{
    /// <inheritdoc />
    public partial class FixPackTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pack_Users_UserId",
                table: "Pack");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pack",
                table: "Pack");

            migrationBuilder.DropIndex(
                name: "IX_Pack_UserId",
                table: "Pack");

            migrationBuilder.RenameTable(
                name: "Pack",
                newName: "Packs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Packs",
                table: "Packs",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Packs_UserId",
                table: "Packs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packs_Users_UserId",
                table: "Packs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packs_Users_UserId",
                table: "Packs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Packs",
                table: "Packs");

            migrationBuilder.DropIndex(
                name: "IX_Packs_UserId",
                table: "Packs");

            migrationBuilder.RenameTable(
                name: "Packs",
                newName: "Pack");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pack",
                table: "Pack",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Pack_UserId",
                table: "Pack",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pack_Users_UserId",
                table: "Pack",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
