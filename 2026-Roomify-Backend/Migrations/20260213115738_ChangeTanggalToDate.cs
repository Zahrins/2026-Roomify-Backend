using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2026_Roomify_Backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTanggalToDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "role",
                value: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "role",
                value: "User");
        }
    }
}
