using Microsoft.EntityFrameworkCore.Migrations;

namespace WhileLearningCzech.Domain.Migrations
{
    public partial class word_group_desc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WordGroups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "WordGroups");
        }
    }
}
