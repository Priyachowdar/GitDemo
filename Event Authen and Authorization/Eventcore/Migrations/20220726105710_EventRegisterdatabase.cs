using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventcore.Migrations
{
    public partial class EventRegisterdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventRegister",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    StudentName = table.Column<string>(nullable: true),
                    EventId = table.Column<int>(nullable: false),
                    EventName = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<string>(nullable: true),
                    RegisteredDate = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    Contact = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegister", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventRegister");
        }
    }
}
