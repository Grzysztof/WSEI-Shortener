using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortener.Web.Migrations
{
    public partial class validation_string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "validationString",
                table: "Links",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "validationString",
                table: "Links");
        }
    }
}
