using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactsApi.Migrations
{
    public partial class newModelCityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Contact");
        }
    }
}
