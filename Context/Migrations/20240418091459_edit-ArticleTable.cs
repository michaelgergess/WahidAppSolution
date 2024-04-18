using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class editArticleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AdminName",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "AdminName",
                table: "Articles",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AdminName",
                table: "Articles",
                newName: "IX_Articles_AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AdminId",
                table: "Articles",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AdminId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Articles",
                newName: "AdminName");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AdminId",
                table: "Articles",
                newName: "IX_Articles_AdminName");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AdminName",
                table: "Articles",
                column: "AdminName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
