﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSlot.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_bookings_rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "rooms",
                columns: new[] { "RoomId", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, 10, "A1.01" },
                    { 2, 5, "A1.02" },
                    { 3, 4, "A1.03" },
                    { 4, 6, "A1.04" }
                });

            migrationBuilder.InsertData(
                table: "bookings",
                columns: new[] { "BookingId", "EndTime", "RoomId", "StartTime", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 16, 11, 30, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 9, 16, 10, 30, 0, 0, DateTimeKind.Unspecified), "Vejledning m. Jens" },
                    { 2, new DateTime(2025, 9, 15, 15, 30, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 9, 15, 13, 30, 0, 0, DateTimeKind.Unspecified), "Møde - Team 3" },
                    { 3, new DateTime(2025, 9, 19, 10, 30, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 9, 19, 8, 30, 0, 0, DateTimeKind.Unspecified), "Ledermøde" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_RoomId",
                table: "bookings",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "rooms");
        }
    }
}
