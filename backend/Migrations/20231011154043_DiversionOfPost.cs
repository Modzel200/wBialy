using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wBialy.Migrations
{
    /// <inheritdoc />
    public partial class DiversionOfPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDate",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "LFPosts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LFPosts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_LFPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnSitePosts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnSitePosts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_OnSitePosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "EventPosts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPosts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_EventPosts_OnSitePosts_PostId",
                        column: x => x.PostId,
                        principalTable: "OnSitePosts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GastroPosts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastroPosts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_GastroPosts_OnSitePosts_PostId",
                        column: x => x.PostId,
                        principalTable: "OnSitePosts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTags", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_EventTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GastroTags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastroTags", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_GastroTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LFTags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LFTags", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_LFTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventPostEventTag",
                columns: table => new
                {
                    PostsPostId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPostEventTag", x => new { x.PostsPostId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_EventPostEventTag_EventPosts_PostsPostId",
                        column: x => x.PostsPostId,
                        principalTable: "EventPosts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventPostEventTag_EventTags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "EventTags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GastroPostGastroTag",
                columns: table => new
                {
                    PostsPostId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastroPostGastroTag", x => new { x.PostsPostId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_GastroPostGastroTag_GastroPosts_PostsPostId",
                        column: x => x.PostsPostId,
                        principalTable: "GastroPosts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GastroPostGastroTag_GastroTags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "GastroTags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LFPostLFTag",
                columns: table => new
                {
                    PostsPostId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LFPostLFTag", x => new { x.PostsPostId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_LFPostLFTag_LFPosts_PostsPostId",
                        column: x => x.PostsPostId,
                        principalTable: "LFPosts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LFPostLFTag_LFTags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "LFTags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventPostEventTag_TagsTagId",
                table: "EventPostEventTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_GastroPostGastroTag_TagsTagId",
                table: "GastroPostGastroTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_LFPostLFTag_TagsTagId",
                table: "LFPostLFTag",
                column: "TagsTagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventPostEventTag");

            migrationBuilder.DropTable(
                name: "GastroPostGastroTag");

            migrationBuilder.DropTable(
                name: "LFPostLFTag");

            migrationBuilder.DropTable(
                name: "EventPosts");

            migrationBuilder.DropTable(
                name: "EventTags");

            migrationBuilder.DropTable(
                name: "GastroPosts");

            migrationBuilder.DropTable(
                name: "GastroTags");

            migrationBuilder.DropTable(
                name: "LFPosts");

            migrationBuilder.DropTable(
                name: "LFTags");

            migrationBuilder.DropTable(
                name: "OnSitePosts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDate",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
