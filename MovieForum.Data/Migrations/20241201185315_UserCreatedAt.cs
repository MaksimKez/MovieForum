using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieForum.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsConsonant",
                table: "Comments",
                newName: "IsPositive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPositive",
                table: "Comments",
                newName: "IsConsonant");
        }
    }
}
