using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _2026_Roomify_Backend.Migrations
{
    /// <inheritdoc />
    public partial class SafeSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "buildings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nama = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buildings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nama = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    tipe = table.Column<string>(type: "text", nullable: false),
                    kapasitas = table.Column<int>(type: "integer", nullable: false),
                    buildingid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.id);
                    table.ForeignKey(
                        name: "FK_rooms_buildings_buildingid",
                        column: x => x.buildingid,
                        principalTable: "buildings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nama_peminjam = table.Column<string>(type: "text", nullable: false),
                    no_kontak = table.Column<string>(type: "text", nullable: false),
                    tanggal = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    jam_mulai = table.Column<string>(type: "text", nullable: false),
                    jam_selesai = table.Column<string>(type: "text", nullable: false),
                    keperluan = table.Column<string>(type: "text", nullable: false),
                    building_id = table.Column<int>(type: "integer", nullable: false),
                    room_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.id);
                    table.ForeignKey(
                        name: "FK_bookings_buildings_building_id",
                        column: x => x.building_id,
                        principalTable: "buildings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookings_rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookings_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                table: "users",
                columns: new[] { "id", "password_hash", "role", "username" },
                values: new object[] { 1, "password123", "User", "zahrin" });

            migrationBuilder.InsertData(
                table: "rooms",
                columns: new[] { "id", "buildingid", "kapasitas", "nama", "status", "tipe" },
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

            migrationBuilder.CreateIndex(
                name: "IX_bookings_building_id",
                table: "bookings",
                column: "building_id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_room_id",
                table: "bookings",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_user_id",
                table: "bookings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_buildingid",
                table: "rooms",
                column: "buildingid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "buildings");
        }
    }
}
