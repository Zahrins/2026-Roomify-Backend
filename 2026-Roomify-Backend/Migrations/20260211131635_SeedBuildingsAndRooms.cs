using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _2026_Roomify_Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedBuildingsAndRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "buildings",
                columns: new[] { "id", "nama" },
                values: new object[,]
                {
                    { 1, "Gedung D4" },
                    { 2, "Gedung D3" },
                    { 3, "Gedung SAW" },
                    { 4, "Gedung Pasca" }
                });

            migrationBuilder.InsertData(
                table: "rooms",
                columns: new[] { "id", "BuildingId", "kapasitas", "nama", "status", "tipe" },
                values: new object[,]
                {
                    { 1, 1, 30, "R.101", "kosong", "Kelas" },
                    { 2, 1, 20, "R.102", "kosong", "Kelas" },
                    { 3, 1, 20, "R.103", "kosong", "Kelas" },
                    { 4, 1, 20, "R.104", "kosong", "Kelas" },
                    { 5, 1, 20, "Lab.IT", "kosong", "Laboratorium" },
                    { 6, 2, 20, "R.201", "kosong", "Kelas" },
                    { 7, 2, 25, "R.202", "kosong", "Kelas" },
                    { 8, 2, 15, "Lab IoT", "kosong", "Laboratorium" },
                    { 9, 2, 30, "R.203", "kosong", "Kelas" },
                    { 10, 3, 40, "Ruang 301", "kosong", "Kelas" },
                    { 11, 3, 35, "Ruang 302", "kosong", "Kelas" },
                    { 12, 3, 20, "Lab Multimedia", "kosong", "Laboratorium" },
                    { 13, 4, 25, "Ruang 401", "kosong", "Kelas" },
                    { 14, 4, 25, "Ruang 402", "kosong", "Kelas" },
                    { 15, 4, 15, "Lab Research", "kosong", "Laboratorium" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "rooms",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "buildings",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "buildings",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "buildings",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "buildings",
                keyColumn: "id",
                keyValue: 4);
        }
    }
}
