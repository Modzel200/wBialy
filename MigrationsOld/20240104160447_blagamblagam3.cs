using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wBialy.Migrations
{
    /// <inheritdoc />
    public partial class blagamblagam3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedPost_Posts_PostId1",
                table: "UserLikedPost");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedPost_Users_UserId1",
                table: "UserLikedPost");

            migrationBuilder.DropIndex(
                name: "IX_UserLikedPost_PostId1",
                table: "UserLikedPost");

            migrationBuilder.DropIndex(
                name: "IX_UserLikedPost_UserId1",
                table: "UserLikedPost");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "UserLikedPost");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserLikedPost");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId1",
                table: "UserLikedPost",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UserLikedPost",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLikedPost_PostId1",
                table: "UserLikedPost",
                column: "PostId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikedPost_UserId1",
                table: "UserLikedPost",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedPost_Posts_PostId1",
                table: "UserLikedPost",
                column: "PostId1",
                principalTable: "Posts",
                principalColumn: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedPost_Users_UserId1",
                table: "UserLikedPost",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
