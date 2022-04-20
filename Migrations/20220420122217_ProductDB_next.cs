using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_ASP.NET6.Migrations
{
    public partial class ProductDB_next : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Test",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "Products");
        }
    }
}
