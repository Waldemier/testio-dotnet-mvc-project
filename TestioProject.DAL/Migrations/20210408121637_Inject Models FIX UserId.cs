using Microsoft.EntityFrameworkCore.Migrations;

namespace TestioProject.DAL.Migrations
{
    public partial class InjectModelsFIXUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_AspNetUsers_UserId1",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_UserId1",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Statistics");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Statistics",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_AspNetUsers_UserId",
                table: "Statistics",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_AspNetUsers_UserId",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Statistics",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Statistics",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_UserId1",
                table: "Statistics",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_AspNetUsers_UserId1",
                table: "Statistics",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
