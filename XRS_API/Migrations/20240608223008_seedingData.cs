using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XRS_API.Migrations
{
    /// <inheritdoc />
    public partial class seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "Email", "IsActive", "Name", "Phone" },
                values: new object[] { 1, "Azaz", "AK-07Hotels@hotel.com", true, "AK-07Hotels", "12332131233" });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "IsActive", "NumOfBeds", "TypeName" },
                values: new object[,]
                {
                    { 1, true, 1, "P-01" },
                    { 2, true, 2, "P-02" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DOB", "Email", "FirstName", "HotelId", "IsActive", "LastName", "StratDate", "Title" },
                values: new object[] { 1, new DateTime(2000, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alilllll@gmail.com", "Ali", 1, true, "AlAli", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receptionist" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BookingId", "FloorNumber", "HotelId", "IsActive", "Number", "RoomTypeId", "Status" },
                values: new object[] { 1, 0, 1, 1, true, 1, 1, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
