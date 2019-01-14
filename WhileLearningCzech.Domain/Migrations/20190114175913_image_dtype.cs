using Microsoft.EntityFrameworkCore.Migrations;

namespace WhileLearningCzech.Domain.Migrations
{
    public partial class image_dtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataType",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataType",
                table: "Images");
        }
    }
}
