using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limupa.Migrations
{
    public partial class TeamUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GooglePlus",
                table: "TeamMembers",
                newName: "GooglePlusLink");

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_PositionId",
                table: "TeamMembers",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Positions_PositionId",
                table: "TeamMembers",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Positions_PositionId",
                table: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_PositionId",
                table: "TeamMembers");

            migrationBuilder.RenameColumn(
                name: "GooglePlusLink",
                table: "TeamMembers",
                newName: "GooglePlus");
        }
    }
}
