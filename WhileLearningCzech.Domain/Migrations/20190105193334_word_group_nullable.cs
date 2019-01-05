using Microsoft.EntityFrameworkCore.Migrations;

namespace WhileLearningCzech.Domain.Migrations
{
    public partial class word_group_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_WordGroups_WordGroupId",
                table: "Words");

            migrationBuilder.AlterColumn<int>(
                name: "WordGroupId",
                table: "Words",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Words_WordGroups_WordGroupId",
                table: "Words",
                column: "WordGroupId",
                principalTable: "WordGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_WordGroups_WordGroupId",
                table: "Words");

            migrationBuilder.AlterColumn<int>(
                name: "WordGroupId",
                table: "Words",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_WordGroups_WordGroupId",
                table: "Words",
                column: "WordGroupId",
                principalTable: "WordGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
