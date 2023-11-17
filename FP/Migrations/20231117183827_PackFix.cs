using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FP.Migrations
{
    /// <inheritdoc />
    public partial class PackFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Packs",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "BalanceWalletId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopUpWalletId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackTypeId",
                table: "Packs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PackTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WalletAddress = table.Column<string>(type: "text", nullable: false),
                    WalletSecretKey = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_BalanceWalletId",
                table: "Users",
                column: "BalanceWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TopUpWalletId",
                table: "Users",
                column: "TopUpWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Packs_PackTypeId",
                table: "Packs",
                column: "PackTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packs_PackTypes_PackTypeId",
                table: "Packs",
                column: "PackTypeId",
                principalTable: "PackTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wallets_BalanceWalletId",
                table: "Users",
                column: "BalanceWalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wallets_TopUpWalletId",
                table: "Users",
                column: "TopUpWalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packs_PackTypes_PackTypeId",
                table: "Packs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Wallets_BalanceWalletId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Wallets_TopUpWalletId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "PackTypes");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Users_BalanceWalletId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TopUpWalletId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Packs_PackTypeId",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "BalanceWalletId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TopUpWalletId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PackTypeId",
                table: "Packs");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Packs",
                newName: "ID");
        }
    }
}
