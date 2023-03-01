using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limupa.Migrations
{
    public partial class TeamMemberTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FacebookLink = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TwitterLink = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LinkedinLink = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    GooglePlus = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMembers");
        }
    }
}
