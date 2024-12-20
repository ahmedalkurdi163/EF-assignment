using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XRS_API.Migrations
{
    /// <inheritdoc />
    public partial class updateSomeOfData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Employees_EmployeeId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Hotels_HotelId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Hotels_HotelId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Bookings_BookingId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Hotels_HotelId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Guests_GuestId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Guests_BookingId",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_EmployeeId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_HotelId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Hotels_HotelId",
                table: "Employees",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Hotels_HotelId",
                table: "Guests",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Guests_GuestId",
                table: "Payments",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Hotels_HotelId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Hotels_HotelId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Guests_GuestId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_BookingId",
                table: "Guests",
                column: "BookingId",
                unique: true,
                filter: "[BookingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_EmployeeId",
                table: "Bookings",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HotelId",
                table: "Bookings",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Employees_EmployeeId",
                table: "Bookings",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Hotels_HotelId",
                table: "Bookings",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Hotels_HotelId",
                table: "Employees",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Bookings_BookingId",
                table: "Guests",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Hotels_HotelId",
                table: "Guests",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Guests_GuestId",
                table: "Payments",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
