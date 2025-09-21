using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSlot.Migrations
{
    /// <inheritdoc />
    public partial class meets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "EndTime", "RoomId", "StartTime", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 16, 11, 30, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 9, 16, 10, 30, 0, 0, DateTimeKind.Unspecified), "Vejledning m. Jens", "test@test.dk" },
                    { 2, new DateTime(2025, 9, 15, 15, 30, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 9, 15, 13, 30, 0, 0, DateTimeKind.Unspecified), "Møde - Team 3", "aba@test.dk" },
                    { 3, new DateTime(2025, 9, 19, 10, 30, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 9, 19, 8, 30, 0, 0, DateTimeKind.Unspecified), "Ledermøde", "test@test.dk" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 3);
        }
    }
}
