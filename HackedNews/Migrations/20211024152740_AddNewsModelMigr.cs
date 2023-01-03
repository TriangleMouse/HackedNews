using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackedNews.Migrations
{
    public partial class AddNewsModelMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ShorttextCard = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    NewsIntroduction = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    Img_Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Switch_Load_Img = table.Column<bool>(type: "bit", nullable: false),
                    Img_Load = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorNews = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subtitle = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Img_Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Switch_Load_Img = table.Column<bool>(type: "bit", nullable: false),
                    Img_Load = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Txt = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    NewsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsData_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_CategoryID",
                table: "News",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsData_NewsId",
                table: "NewsData",
                column: "NewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsData");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
