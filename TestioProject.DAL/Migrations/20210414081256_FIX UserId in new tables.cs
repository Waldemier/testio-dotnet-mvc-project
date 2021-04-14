using Microsoft.EntityFrameworkCore.Migrations;

namespace TestioProject.DAL.Migrations
{
    public partial class FIXUserIdinnewtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restricteds_AspNetUsers_UserId1",
                table: "Restricteds");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAvatars_AspNetUsers_UserId1",
                table: "UserAvatars");

            migrationBuilder.DropForeignKey(
                name: "FK_WrittenLetters_AspNetUsers_UserId1",
                table: "WrittenLetters");

            migrationBuilder.DropIndex(
                name: "IX_WrittenLetters_UserId1",
                table: "WrittenLetters");

            migrationBuilder.DropIndex(
                name: "IX_UserAvatars_UserId1",
                table: "UserAvatars");

            migrationBuilder.DropIndex(
                name: "IX_Restricteds_UserId1",
                table: "Restricteds");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "WrittenLetters");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserAvatars");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Restricteds");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WrittenLetters",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAvatars",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Restricteds",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_WrittenLetters_UserId",
                table: "WrittenLetters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvatars_UserId",
                table: "UserAvatars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Restricteds_UserId",
                table: "Restricteds",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restricteds_AspNetUsers_UserId",
                table: "Restricteds",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAvatars_AspNetUsers_UserId",
                table: "UserAvatars",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WrittenLetters_AspNetUsers_UserId",
                table: "WrittenLetters",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restricteds_AspNetUsers_UserId",
                table: "Restricteds");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAvatars_AspNetUsers_UserId",
                table: "UserAvatars");

            migrationBuilder.DropForeignKey(
                name: "FK_WrittenLetters_AspNetUsers_UserId",
                table: "WrittenLetters");

            migrationBuilder.DropIndex(
                name: "IX_WrittenLetters_UserId",
                table: "WrittenLetters");

            migrationBuilder.DropIndex(
                name: "IX_UserAvatars_UserId",
                table: "UserAvatars");

            migrationBuilder.DropIndex(
                name: "IX_Restricteds_UserId",
                table: "Restricteds");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WrittenLetters",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "WrittenLetters",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserAvatars",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserAvatars",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Restricteds",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Restricteds",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WrittenLetters_UserId1",
                table: "WrittenLetters",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvatars_UserId1",
                table: "UserAvatars",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Restricteds_UserId1",
                table: "Restricteds",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Restricteds_AspNetUsers_UserId1",
                table: "Restricteds",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAvatars_AspNetUsers_UserId1",
                table: "UserAvatars",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WrittenLetters_AspNetUsers_UserId1",
                table: "WrittenLetters",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
