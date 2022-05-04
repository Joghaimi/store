using Microsoft.EntityFrameworkCore.Migrations;

namespace Project1.Migrations
{
    public partial class ahmads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "shourtDescription",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shourtDescription",
                table: "Product");
        }
    }
}
