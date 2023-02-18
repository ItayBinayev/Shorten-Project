using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShortenProject.Migrations
{
    /// <inheritdoc />
    public partial class LinkingIdentityUserToURLModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlUserId",
                table: "URLs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_URLs_UrlUserId",
                table: "URLs",
                column: "UrlUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_URLs_AspNetUsers_UrlUserId",
                table: "URLs",
                column: "UrlUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_URLs_AspNetUsers_UrlUserId",
                table: "URLs");

            migrationBuilder.DropIndex(
                name: "IX_URLs_UrlUserId",
                table: "URLs");

            migrationBuilder.DropColumn(
                name: "UrlUserId",
                table: "URLs");
        }
    }
}
