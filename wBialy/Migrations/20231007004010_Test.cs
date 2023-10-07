using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wBialy.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreatedByUserUserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CreatedByUserUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatedByUserUserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Posts",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Posts",
                newName: "CreatedById");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserUserId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedByUserUserId",
                table: "Posts",
                column: "CreatedByUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreatedByUserUserId",
                table: "Posts",
                column: "CreatedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
