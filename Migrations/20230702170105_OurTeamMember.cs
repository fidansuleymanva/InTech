using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InTech_MVC.Migrations
{
    public partial class OurTeamMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OurTeamMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Fullname = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Profession = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    About = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurTeamMember", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OurTeamMember");
        }
    }
}
