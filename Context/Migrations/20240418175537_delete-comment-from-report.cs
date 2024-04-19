using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class deletecommentfromreport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentOfUser",
                table: "ReportArticle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentOfUser",
                table: "ReportArticle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
