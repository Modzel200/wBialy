using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wBialy.Migrations
{
    /// <inheritdoc />
    public partial class likedPosts6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLikedPost");

            migrationBuilder.CreateTable(
                name: "PostUser",
                columns: table => new
                {
                    LikedByUserId = table.Column<int>(type: "int", nullable: false),
                    LikedPostsPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUser", x => new { x.LikedByUserId, x.LikedPostsPostId });
                    table.ForeignKey(
                        name: "FK_PostUser_Posts_LikedPostsPostId",
                        column: x => x.LikedPostsPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostUser_Users_LikedByUserId",
                        column: x => x.LikedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostUser_LikedPostsPostId",
                table: "PostUser",
                column: "LikedPostsPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostUser");

            migrationBuilder.CreateTable(
                name: "UserLikedPost",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikedPost", x => new { x.PostId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserLikedPost_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLikedPost_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLikedPost_UserId",
                table: "UserLikedPost",
                column: "UserId");
        }
    }
}
