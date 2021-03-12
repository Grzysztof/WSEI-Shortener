using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortener.Web.Migrations
{
    public partial class validation_string_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortPath",
                table: "Links");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortPath",
                table: "Links",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
