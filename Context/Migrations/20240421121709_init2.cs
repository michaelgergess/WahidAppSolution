using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportArticle_AspNetUsers_UserName",
                table: "ReportArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_WorldChat_AspNetUsers_UserName",
                table: "WorldChat");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "WorldChat",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorldChat_UserName",
                table: "WorldChat",
                newName: "IX_WorldChat_UserId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "ReportArticle",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReportArticle_UserName",
                table: "ReportArticle",
                newName: "IX_ReportArticle_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportArticle_AspNetUsers_UserId",
                table: "ReportArticle",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WorldChat_AspNetUsers_UserId",
                table: "WorldChat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportArticle_AspNetUsers_UserId",
                table: "ReportArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_WorldChat_AspNetUsers_UserId",
                table: "WorldChat");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WorldChat",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_WorldChat_UserId",
                table: "WorldChat",
                newName: "IX_WorldChat_UserName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ReportArticle",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_ReportArticle_UserId",
                table: "ReportArticle",
                newName: "IX_ReportArticle_UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportArticle_AspNetUsers_UserName",
                table: "ReportArticle",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorldChat_AspNetUsers_UserName",
                table: "WorldChat",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
