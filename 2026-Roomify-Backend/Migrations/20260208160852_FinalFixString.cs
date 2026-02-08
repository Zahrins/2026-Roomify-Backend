using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace _2026_Roomify_Backend.Migrations
{
    /// <inheritdoc />
    public partial class FinalFixString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    room_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.id);
                });

            //migrationBuilder.CreateTable(
            //    name: "buildings",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        nama = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_buildings", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "rooms",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        nama = table.Column<string>(type: "text", nullable: false),
            //        status = table.Column<string>(type: "text", nullable: false),
            //        tipe = table.Column<string>(type: "text", nullable: false),
            //        kapasitas = table.Column<int>(type: "integer", nullable: false),
            //        building_id = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_rooms", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_rooms_buildings_building_id",
            //            column: x => x.building_id,
            //            principalTable: "buildings",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_rooms_building_id",
            //    table: "rooms",
            //    column: "building_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "buildings");
        }
    }
}
