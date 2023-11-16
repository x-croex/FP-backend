using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FP.Migrations
{
    /// <inheritdoc />
    public partial class AddPackType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packs_PackTypes_PackTypeId",
                table: "Packs");

            migrationBuilder.DropTable(
                name: "PackTypes");

            migrationBuilder.DropIndex(
                name: "IX_Packs_PackTypeId",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "PackTypeId",
                table: "Packs");
        }
    }
}
