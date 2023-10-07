using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wBialy.Migrations
{
    /// <inheritdoc />
    public partial class IdDiversity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreatedByUserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Posts",
                newName: "CreatedByUserUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Posts",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CreatedByUserId",
                table: "Posts",
                newName: "IX_Posts_CreatedByUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreatedByUserUserId",
                table: "Posts",
                column: "CreatedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreatedByUserUserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserUserId",
                table: "Posts",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Posts",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CreatedByUserUserId",
                table: "Posts",
                newName: "IX_Posts_CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreatedByUserId",
                table: "Posts",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
