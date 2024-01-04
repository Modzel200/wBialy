using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wBialy.Migrations
{
    /// <inheritdoc />
    public partial class proszeDzialaj4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostUser_Posts_LikedPostsPostId",
                table: "PostUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostUser",
                table: "PostUser");

            migrationBuilder.DropIndex(
                name: "IX_PostUser_LikedPostsPostId",
                table: "PostUser");

            migrationBuilder.DropColumn(
                name: "LikedPostsPostId",
                table: "PostUser");

            migrationBuilder.DropColumn(
                name: "PostForeignKey",
                table: "PostUser");

            migrationBuilder.RenameColumn(
                name: "UserForeignKey",
                table: "PostUser",
                newName: "PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostUser",
                table: "PostUser",
                columns: new[] { "LikedByUserId", "PostId" });

            migrationBuilder.CreateIndex(
                name: "IX_PostUser_PostId",
                table: "PostUser",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostUser_Posts_PostId",
                table: "PostUser",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostUser_Posts_PostId",
                table: "PostUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostUser",
                table: "PostUser");

            migrationBuilder.DropIndex(
                name: "IX_PostUser_PostId",
                table: "PostUser");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostUser",
                newName: "UserForeignKey");

            migrationBuilder.AddColumn<int>(
                name: "LikedPostsPostId",
                table: "PostUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostForeignKey",
                table: "PostUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostUser",
                table: "PostUser",
                columns: new[] { "LikedByUserId", "LikedPostsPostId" });

            migrationBuilder.CreateIndex(
                name: "IX_PostUser_LikedPostsPostId",
                table: "PostUser",
                column: "LikedPostsPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostUser_Posts_LikedPostsPostId",
                table: "PostUser",
                column: "LikedPostsPostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
