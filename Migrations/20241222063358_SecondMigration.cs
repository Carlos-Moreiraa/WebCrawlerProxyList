using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCrawlerProxyList.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JsonFilePath",
                table: "CrawlerExecutions",
                newName: "JsonData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JsonData",
                table: "CrawlerExecutions",
                newName: "JsonFilePath");
        }
    }
}
