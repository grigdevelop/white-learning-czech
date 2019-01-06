using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WhileLearningCzech.Domain.Migrations
{
    public partial class article_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatePublished",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatePublished",
                table: "Articles");
        }
    }
}
